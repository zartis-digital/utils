using System;
using System.Collections.Generic;

namespace ZartisLogs
{
    class AppSettingsModel
    {
        public string UserName { get; set; }
        public int Step1DateSelectionType { get; set; } = 1;
        public DateTime LastDateFromUsed { get; set; }
        public DateTime LastDateToUsed { get; set; }
        public string ProjectFolderPath { get; set; }
        public ProjectFilePath[] ProjectFilePaths { get; set; }
        public int Step5GroupByType { get; set; } = 1;
        public string Step6FileNamePattern { get; set; }
        public string PrettierFormat { get; set; }
        public bool TryToUpload { get; set; } = true;
        public string DriveFolderName { get; set; }
        public string SaveFilePath { get; set; }

    }
}

public class ProjectFilePath
{
    public string parentPath { get; set; }
    public string projectPath { get; set; }
    public string projectName { get; set; }
}
