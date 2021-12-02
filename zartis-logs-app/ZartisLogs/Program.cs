
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ZartisLogs
{
    class Program
    {
        static IConfiguration Configuration;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            using var serviceProvider = services.BuildServiceProvider();
            var main = serviceProvider.GetRequiredService<Main>();
            Application.Run(main);
        }


        private static void ConfigureServices(ServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                               .AddUserSecrets("0902cec2-7a5c-415f-8178-e7e2470b035b")                                               
                                               .AddEnvironmentVariables();
            Configuration = builder.Build();

            AppSettingsModel appSettingsModel = new();

            try
            {
                Configuration.Bind(appSettingsModel);

                if (appSettingsModel.ProjectFilePaths != null)
                    appSettingsModel.ProjectFilePaths.ToList().RemoveAll(x => !Directory.Exists(x.projectPath) || string.IsNullOrEmpty(x.projectPath));

                services
                    .AddSingleton(appSettingsModel)
                    .AddScoped<GoogleDriveUploaderService>()
                    .AddScoped<Main>();

            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is InvalidCastException)
            {
                try
                {
                    setDefaultConfiguration();
                    ConfigureServices(services);
                }
                catch (Exception) { throw; }
            }
            catch (Exception) { throw; }
        }

        private static void setDefaultConfiguration()
        {
            var appSettingsModel = new AppSettingsModel
            {
                UserName = null,

                Step1DateSelectionType = 1,

                LastDateFromUsed = DateTime.UtcNow.AddDays(-1).Date,
                LastDateToUsed = DateTime.UtcNow.Date,

                ProjectFolderPath = null,

                ProjectFilePaths = new List<ProjectFilePath>().ToArray(),

                Step5GroupByType = 1,

                Step6FileNamePattern = "E.T {UserName} - {ProjectName}.txt",
                PrettierFormat = "email",

                TryToUpload = false,
                DriveFolderName = null,

                SaveFilePath = null
            };

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appSettings.json");
            var output = Newtonsoft.Json.JsonConvert.SerializeObject(appSettingsModel, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
        }
    }
}
