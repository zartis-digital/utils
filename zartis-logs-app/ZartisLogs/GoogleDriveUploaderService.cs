using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZartisLogs
{
    public class GoogleDriveUploaderService
    {
        private DriveService driveService;
        private readonly AppSettingsModel _appSettingsModel;

        public GoogleDriveUploaderService(AppSettingsModel appSettingsModel)
        {
            _appSettingsModel = appSettingsModel;

            if (_appSettingsModel.installed != null && !string.IsNullOrEmpty(_appSettingsModel.installed.client_id) && !string.IsNullOrEmpty(_appSettingsModel.installed.client_secret))
                setDriveServiceAsync().GetAwaiter().GetResult();
        }

        async Task setDriveServiceAsync()
        {
            string credPath = "token.json";

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets() { ClientId = _appSettingsModel.installed.client_id, ClientSecret = _appSettingsModel.installed.client_secret },
                new string[] { DriveService.Scope.Drive },
                "ZartisLogs",
                CancellationToken.None,
                new FileDataStore(credPath, true));

            driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "ZartisLogs",
            });
        }

        public (IUploadProgress uploadProgress, string fileName) UploadFile(string filePath, string fileName, string folderName)
        {
            var folderListInsideRoot = getFolder(folderName);

            Google.Apis.Drive.v3.Data.File driveFolderName;
            if (folderListInsideRoot == null)
                driveFolderName = createFolder(folderName);
            else
                driveFolderName = getFolder(folderName);

            return (upload(filePath, fileName, driveFolderName.Id), fileName);
        }

        public List<(IUploadProgress uploadProgress, string fileName)> UploadFiles(List<(string filePath, string fileName)> files, string folderName)
        {
            var result = new List<(IUploadProgress uploadProgress, string fileName)>();

            var folderListInsideRoot = getFolder(folderName);

            Google.Apis.Drive.v3.Data.File driveFolderName;
            if (folderListInsideRoot == null)
                driveFolderName = createFolder(folderName);
            else
                driveFolderName = getFolder(folderName);

            foreach (var (filePath, fileName) in files)
                result.Add(new(upload(filePath, fileName, driveFolderName.Id), fileName));

            return result;
        }

        public async Task<(IUploadProgress uploadProgress, string fileName)> UploadFileAsync(string filePath, string fileName, string folderName)
        {
            var folderListInsideRoot = await getFolderAsync(folderName);

            Google.Apis.Drive.v3.Data.File driveFolderName;
            if (folderListInsideRoot == null)
                driveFolderName = await createFolderAsync(folderName);
            else
                driveFolderName = await getFolderAsync(folderName);

            return (await uploadAsync(filePath, fileName, driveFolderName.Id), fileName);
        }

        public async Task<List<(IUploadProgress uploadProgress, string fileName)>> UploadFilesAsync(List<(string filePath, string fileName)> files, string folderName)
        {
            var result = new List<(IUploadProgress uploadProgress, string fileName)>();

            var folderListInsideRoot = await getFolderAsync(folderName);

            Google.Apis.Drive.v3.Data.File driveFolderName;
            if (folderListInsideRoot == null)
                driveFolderName = await createFolderAsync(folderName);
            else
                driveFolderName = await getFolderAsync(folderName);

            foreach (var (filePath, fileName) in files)
                result.Add(new(await uploadAsync(filePath, fileName, driveFolderName.Id), fileName));

            return result;
        }

        Google.Apis.Drive.v3.Data.File getFolder(string folderName, string parentFolderID = null)
        {
            var getFoldersResponse = getFolders(parentFolderID);

            if (getFoldersResponse.Any(x => x.Name == folderName && (!x.Trashed.HasValue || !x.Trashed.Value)))
                return getFoldersResponse.FirstOrDefault(x => x.Name == folderName);
            else
                return null;
        }

        async Task<Google.Apis.Drive.v3.Data.File> getFolderAsync(string folderName, string parentFolderID = null)
        {
            var getFoldersResponse = await getFoldersAsync(parentFolderID);

            if (getFoldersResponse.Any(x => x.Name == folderName && (!x.Trashed.HasValue || !x.Trashed.Value)))
                return getFoldersResponse.FirstOrDefault(x => x.Name == folderName);
            else
                return null;
        }

        IList<Google.Apis.Drive.v3.Data.File> getFolders(string parentFolderID = null)
        {
            var listRequest = driveService.Files.List();
            if (parentFolderID == null)
                listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder'";
            else
                listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' AND '{parentFolderID}' in parents";
            listRequest.SupportsAllDrives = true;
            listRequest.SupportsTeamDrives = true;
            var cResult = listRequest.Execute().Files;
            return cResult;
        }

        async Task<IList<Google.Apis.Drive.v3.Data.File>> getFoldersAsync(string parentFolderID = null)
        {
            var listRequest = driveService.Files.List();
            if (parentFolderID == null)
                listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder'";
            else
                listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' AND '{parentFolderID}' in parents";
            listRequest.SupportsAllDrives = true;
            listRequest.SupportsTeamDrives = true;
            var cResult = (await listRequest.ExecuteAsync()).Files;
            return cResult;
        }

        Google.Apis.Drive.v3.Data.File createFolder(string name, string parentFolderID = "root")
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new[] { parentFolderID }
            };

            var request = driveService.Files.Create(fileMetadata);
            request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType";

            return request.Execute();
        }

        async Task<Google.Apis.Drive.v3.Data.File> createFolderAsync(string name, string parentFolderID = "root")
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new[] { parentFolderID }
            };

            var request = driveService.Files.Create(fileMetadata);
            request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType";

            return await request.ExecuteAsync();
        }

        IUploadProgress upload(string filePath, string fileName, string parentFolderID)
        {
            FilesResource.CreateMediaUpload request;
            var ms = new MemoryStream();

            using FileStream fs = File.Open(filePath, FileMode.Open);

            try
            {
                fs.CopyTo(ms);

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName,
                    MimeType = "text/plain",
                    Parents = new[] { parentFolderID }
                };

                request = driveService.Files.Create(fileMetadata, ms, "text/plain");
                request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType, thumbnailLink";
                return request.Upload();
            }
            finally { ms.Dispose(); }
        }

        async Task<IUploadProgress> uploadAsync(string filePath, string fileName, string parentFolderID)
        {
            FilesResource.CreateMediaUpload request;
            var ms = new MemoryStream();

            using FileStream fs = File.Open(filePath, FileMode.Open);

            try
            {
                await fs.CopyToAsync(ms);

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName,
                    MimeType = "text/plain",
                    Parents = new[] { parentFolderID }
                };

                request = driveService.Files.Create(fileMetadata, ms, "text/plain");
                request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType, thumbnailLink";
                return await request.UploadAsync();
            }
            finally { await ms.DisposeAsync(); }
        }
    }
}
