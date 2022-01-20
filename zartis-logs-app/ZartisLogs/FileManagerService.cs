using System.Text;

namespace ZartisLogs
{
    public class FileManagerService
    {
        public IEnumerable<(string parentPath, string projectPath, string projectName)> GetProjectsByExtensions(IEnumerable<(string parentPath, string projectPath, string projectName)> existingLBAddedProjectFilePaths, string projectsFilePath, string extension)
        {
            var availableProjectsPaths = Directory.GetFiles(projectsFilePath, extension, SearchOption.AllDirectories);

            var results = new List<(string parentPath, string projectPath, string projectName)>();

            var formattedExtensions = extension.Replace("*", "").Replace(".", "");

            foreach (var availableProjectsPath in availableProjectsPaths)
            {
                var _parentPath = Directory.GetParent(availableProjectsPath).FullName;
                string _projectPath = string.Empty;

                if (availableProjectsPath.Contains(formattedExtensions))
                    _projectPath = new DirectoryInfo(availableProjectsPath).Parent.FullName;
                else
                    _projectPath = availableProjectsPath;

                var _projectName = new DirectoryInfo(availableProjectsPath).Name.Contains(formattedExtensions) ?
                    Directory.GetParent(availableProjectsPath).Name :
                    new DirectoryInfo(availableProjectsPath).Name;

                if (!existingLBAddedProjectFilePaths.Any(y => y.projectPath == _projectPath))
                    results.Add(new(_parentPath, _projectPath, _projectName));
            }

            return results;
        }

        public async Task<IEnumerable<(string parentPath, string projectPath, string projectName)>> GetProjectsByExtensionsAsync(IEnumerable<(string parentPath, string projectPath, string projectName)> existingLBAddedProjectFilePaths, string projectsFilePath, string extension)
        {
            var availableProjectsPaths = Directory.GetFiles(projectsFilePath, extension, SearchOption.AllDirectories);

            var results = new List<(string parentPath, string projectPath, string projectName)>();

            var formattedExtensions = extension.Replace("*", "").Replace(".", "");

            foreach (var availableProjectsPath in availableProjectsPaths)
            {
                var _parentPath = Directory.GetParent(availableProjectsPath).FullName;
                string _projectPath = string.Empty;

                if (availableProjectsPath.Contains(formattedExtensions))
                    _projectPath = new DirectoryInfo(availableProjectsPath).Parent.FullName;
                else
                    _projectPath = availableProjectsPath;

                var _projectName = new DirectoryInfo(availableProjectsPath).Name.Contains(formattedExtensions) ?
                    Directory.GetParent(availableProjectsPath).Name :
                    new DirectoryInfo(availableProjectsPath).Name;

                if (!existingLBAddedProjectFilePaths.Any(y => y.projectPath == _projectPath))
                    results.Add(new(_parentPath, _projectPath, _projectName));
            }

            return results;
        }

        public string BuildOutputFile(IEnumerable<string> tempFilePaths, string outputFileName)
        {
            var outputStringBuilder = new StringBuilder();

            foreach (var tempFilePath in tempFilePaths)
            {
                if (!File.Exists(tempFilePath))
                    continue;

                using var sr = new StreamReader(tempFilePath);
                outputStringBuilder.Append(sr.ReadToEnd());
            }

            File.WriteAllText(outputFileName, outputStringBuilder.ToString());

            foreach (var tempFilePath in tempFilePaths)
            {
                if (!File.Exists(tempFilePath))
                    continue;

                File.Delete(tempFilePath);
            }

            return outputFileName;
        }

        public (string filePath, string fileName) BuildOutputFileName(string fileNamePattern, string userName, string saveSelectedPath, int month = int.MinValue, int year = int.MinValue, string projectName = null)
        {
            string outputFileName = fileNamePattern;

            if (outputFileName.Contains("{UserName}"))
                outputFileName = outputFileName.Replace("{UserName}", userName);

            if (outputFileName.Contains("{Month}_{Year}") && month != int.MinValue && year != int.MinValue)
                outputFileName = outputFileName.Replace("{Month}_{Year}", $"{month}_{year}");

            if (outputFileName.Contains("{ProjectName}") && projectName != null)
                outputFileName = outputFileName.Replace("{ProjectName}", projectName);

            return (saveSelectedPath + "\\" + outputFileName, outputFileName);
        }
    }
}
