using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Windows.Forms;

namespace ZartisLogs
{
    public partial class Main : Form
    {
        private readonly GoogleDriveUploaderService _googleDriveUploaderService;
        private AppSettingsModel _appSettingsModel;

        public Main(GoogleDriveUploaderService googleDriveUploaderService, AppSettingsModel appSettingsModel)
        {
            _googleDriveUploaderService = googleDriveUploaderService;
            _appSettingsModel = appSettingsModel;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                setInitialValues();
                setLatestsValues();
            }
            catch (Exception ex) { MessageBox.Show("Main_Load - " + ex.Message); }
        }

        private void rb_DateRange_CheckedChanged(object sender, EventArgs e) => setStep2DateSetVisibility(rb_DateUpToNow.Checked, rb_SingleMonth.Checked, rb_DateRange.Checked);

        private void rb_SingleMonth_CheckedChanged(object sender, EventArgs e) => setStep2DateSetVisibility(rb_DateUpToNow.Checked, rb_SingleMonth.Checked, rb_DateRange.Checked);

        private void rb_DateUpToNow_CheckedChanged(object sender, EventArgs e) => setStep2DateSetVisibility(rb_DateUpToNow.Checked, rb_SingleMonth.Checked, rb_DateRange.Checked);

        private void dtpDateFrom_ValueChanged(object sender, EventArgs e) => setStep2DateSetVisibility(rb_DateUpToNow.Checked, rb_SingleMonth.Checked, rb_DateRange.Checked);

        private void btnSearchProjectFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbdSearchProjectFilePath.ShowDialog() == DialogResult.OK)
                    txtProjectFilePath.Text = fbdSearchProjectFilePath.SelectedPath;
            }
            catch (Exception ex) { MessageBox.Show("btnSearchProjectFilePath_Click - " + ex.Message); }
        }

        private void btnAddProjectFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddProjectFilePath_Click_Validate();

                var _lbAddedProjectFilePaths = lbAddedProjectFilePaths.Items.Cast<(string parentPath, string projectPath, string projectName)>();

                if (chkRecursiveSearch.Checked)
                {
                    asyncTaskIsRunning(true);
                    var workerParam = (_lbAddedProjectFilePaths.ToList(), txtProjectFilePath.Text);
                    bgw_AddedProjectFilePaths.RunWorkerAsync(workerParam);
                }
                else if (!_lbAddedProjectFilePaths.Any(x => x.projectPath == txtProjectFilePath.Text))
                {
                    var newItem = (parentPath: Directory.GetParent(txtProjectFilePath.Text).FullName, projectPath: txtProjectFilePath.Text, projectName: new DirectoryInfo(txtProjectFilePath.Text).Name);
                    lbAddedProjectFilePaths.Items.Add(newItem);
                }
            }
            catch (Exception ex) { MessageBox.Show("btnAddProjectFilePath_Click - " + ex.Message); }
            finally
            {
                txtProjectFilePath.Clear();
                chkRecursiveSearch.Checked = false;
            }
        }

        private void bgw_AddedProjectFilePaths_DoWork(object sender, DoWorkEventArgs e)
        {
            bgw_AddedProjectFilePaths.ReportProgress(0);

            bgw_AddedProjectFilePaths.ReportProgress(1, "bgw_AddedProjectFilePaths_DoWork - Parsing Argument");
            var (listOfAvailableProjectFilePaths, projectFilePath) = ((List<(string parentPath, string projectPath, string projectName)> listOfAvailableProjectFilePaths, string projectFilePath))e.Argument;

            bgw_AddedProjectFilePaths.ReportProgress(2, "Searching folders with *.SLN extension");
            var _getProjectsByExtensionsAsync = getProjectsByExtensions(listOfAvailableProjectFilePaths, projectFilePath, "*.sln");

            bgw_AddedProjectFilePaths.ReportProgress(3, "Searching folders with *.SLN extension Job Done");
            listOfAvailableProjectFilePaths.AddRange(_getProjectsByExtensionsAsync);

            bgw_AddedProjectFilePaths.ReportProgress(4, "Searching folders with *.gitignore extension");
            var __getProjectsByExtensionsAsync = getProjectsByExtensions(listOfAvailableProjectFilePaths, projectFilePath, "*.gitignore");

            bgw_AddedProjectFilePaths.ReportProgress(5, "Searching folders with *.gitignore extension Job Done");
            listOfAvailableProjectFilePaths.AddRange(__getProjectsByExtensionsAsync);

            bgw_AddedProjectFilePaths.ReportProgress(100);
            e.Result = listOfAvailableProjectFilePaths;
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
                txt_ConsoleLog.AppendText("*******************Starting Job*******************");

            txt_ConsoleLog.AppendText(Environment.NewLine);

            if (e.UserState != null)
                txt_ConsoleLog.AppendText(e.UserState.ToString());

            if (e.ProgressPercentage == 100)
                txt_ConsoleLog.AppendText("*******************Job Done*******************");
        }

        private void bgw_AddedProjectFilePaths_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                var bgw_AddedProjectFilePaths_DoWork_Result = (List<(string parentPath, string projectPath, string projectName)>)e.Result;

                lbAddedProjectFilePaths.Items.Clear();

                foreach (var (parentPath, projectPath, projectName) in bgw_AddedProjectFilePaths_DoWork_Result)
                    lbAddedProjectFilePaths.Items.Add((parentPath, projectPath, projectName));

                if (e.Error != null)
                    MessageBox.Show("bgw_AddedProjectFilePaths - " + e.Error.Message);
            }
            finally { asyncTaskIsRunning(false); }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbAddedProjectFilePaths.Items.Count > 0 && lbAddedProjectFilePaths.SelectedItems != null)
                    for (int i = lbAddedProjectFilePaths.SelectedItems.Count - 1; i >= 0; i--)
                        lbAddedProjectFilePaths.Items.Remove(lbAddedProjectFilePaths.SelectedItems[i]);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void rb_SingleFileByProject_CheckedChanged(object sender, EventArgs e) => setStep6GeneratedFileNamePattern(rb_SingleFileByProject.Checked, rb_SingleFileByMonthAndYear.Checked, rb_GroupByProjectAndMonth.Checked);

        private void rb_SingleFileByMonthAndYear_CheckedChanged(object sender, EventArgs e) => setStep6GeneratedFileNamePattern(rb_SingleFileByProject.Checked, rb_SingleFileByMonthAndYear.Checked, rb_GroupByProjectAndMonth.Checked);

        private void rb_GroupByProjectAndMonth_CheckedChanged(object sender, EventArgs e) => setStep6GeneratedFileNamePattern(rb_SingleFileByProject.Checked, rb_SingleFileByMonthAndYear.Checked, rb_GroupByProjectAndMonth.Checked);

        private void chkTryToUpload_CheckedChanged(object sender, EventArgs e) => txt_DriveFolderName.Enabled = chkTryToUpload.Checked;

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                setStep2DateSetVisibility_Validate();
                setStep6GeneratedFileNamePattern_Validate();
                gb_CloudSettings_Validate();

                txt_ConsoleLog.Clear();

                if (fbdGenerateBeautifulFile.ShowDialog() == DialogResult.OK)
                {
                    asyncTaskIsRunning(true);

                    var lbAAddedProjectsAsList = lbAddedProjectFilePaths.Items.Cast<(string parentPath, string projectPath, string projectName)>().ToList();

                    bgw_RunGitCommand.RunWorkerAsync((lbAAddedProjectsAsList, dtpDateFrom.Value, dtpDateTo.Value, txt_UserName.Text, getExecutionType(), cbo_PrettierFormat.SelectedItem.ToString()));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); asyncTaskIsRunning(false); }
        }

        private void bgw_RunGitCommand_DoWork(object sender, DoWorkEventArgs e)
        {
            bgw_RunGitCommand.ReportProgress(0);

            bgw_RunGitCommand.ReportProgress(1, "bgw_RunGitCommand_DoWork - Parsing Argument");
            var bgw_RunGitCommand_Argument = ((List<(string parentPath, string projectPath, string projectName)>, DateTime dateFrom, DateTime dateTo, string username, ExecutionType executionType, string prettierFormat))e.Argument;

            var result = new List<(string tempFileName, string projectName, DateTime dateFrom, DateTime dateTo)>();

            switch (bgw_RunGitCommand_Argument.executionType)
            {
                case ExecutionType.rb_DateRange_rb_GroupByProjectAndMonth:
                case ExecutionType.rb_DateRange_rb_SingleFileByMonthAndYear:
                case ExecutionType.rb_SingleMonth_rb_SingleFileByMonthAndYear:
                case ExecutionType.rb_SingleMonth_rb_GroupByProjectAndMonth:
                case ExecutionType.rb_DateUpToNow_rb_SingleFileByMonthAndYear:
                case ExecutionType.rb_DateUpToNow_rb_GroupByProjectAndMonth:
                    bgw_RunGitCommand.ReportProgress(2, "For Each ProjectName/Project Path + Months");
                    foreach (var item in bgw_RunGitCommand_Argument.Item1.GroupBy(x => new { x.projectName, x.projectPath }).Select(y => new { y.Key.projectName, y.Key.projectPath }))
                        foreach (var (Month, Year) in getMonthsBetween(bgw_RunGitCommand_Argument.dateFrom, bgw_RunGitCommand_Argument.dateTo))
                        {
                            var newDateFrom = new DateTime(Year, Month, 1);
                            var newDateTo = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));

                            var resultItem = _bgw_RunGitCommand_DoWork(bgw_RunGitCommand, item.projectPath, newDateFrom, newDateTo, bgw_RunGitCommand_Argument.username, item.projectName, bgw_RunGitCommand_Argument.prettierFormat);

                            result.Add(resultItem);
                        }
                    break;
                case ExecutionType.rb_SingleMonth_rb_SingleFileByProject:
                case ExecutionType.rb_DateRange_rb_SingleFileByProject:
                case ExecutionType.rb_DateUpToNow_rb_SingleFileByProject:
                    bgw_RunGitCommand.ReportProgress(2, "For Each ProjectName/Project Path");
                    foreach (var item in bgw_RunGitCommand_Argument.Item1.GroupBy(x => new { x.projectName, x.projectPath }).Select(y => new { y.Key.projectName, y.Key.projectPath }))
                    {
                        var resultItem = _bgw_RunGitCommand_DoWork(bgw_RunGitCommand, item.projectPath, bgw_RunGitCommand_Argument.dateFrom, bgw_RunGitCommand_Argument.dateTo, bgw_RunGitCommand_Argument.username, item.projectName, bgw_RunGitCommand_Argument.prettierFormat);
                        result.Add(resultItem);
                    }
                    break;
            }

            bgw_RunGitCommand.ReportProgress(100);
            e.Result = result;
        }

        private void bgw_RunGitCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                var bgw_RunGitCommand_DoWork_Result = (List<(string tempFileName, string projectName, DateTime dateFrom, DateTime dateTo)>)e.Result;

                bgw_BuildOutputFile.RunWorkerAsync((bgw_RunGitCommand_DoWork_Result, getExecutionType(), txtFileNamePattern.Text, txt_UserName.Text, fbdGenerateBeautifulFile.SelectedPath));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (e.Error != null)
                {
                    MessageBox.Show("bgw_AddedProjectFilePaths - " + e.Error.Message);
                    asyncTaskIsRunning(false);
                }
            }
        }

        private void bgw_BuildOutputFile_DoWork(object sender, DoWorkEventArgs e)
        {
            bgw_BuildOutputFile.ReportProgress(0);

            bgw_BuildOutputFile.ReportProgress(1, "bgw_BuildOutputFile_DoWork - Parsing Argument");
            var bgw_BuildOutputFile_Argument = ((List<(string tempFileName, string projectName, DateTime dateFrom, DateTime dateTo)>, ExecutionType executionType, string fileNamePattern, string userName, string saveSelectedPath))e.Argument;

            var result = new List<(string filePath, string fileName)>();

            switch (bgw_BuildOutputFile_Argument.executionType)
            {
                case ExecutionType.rb_DateUpToNow_rb_SingleFileByProject:
                case ExecutionType.rb_SingleMonth_rb_SingleFileByProject:
                case ExecutionType.rb_DateRange_rb_SingleFileByProject:
                    foreach (var item in bgw_BuildOutputFile_Argument.Item1.GroupBy(x => x.projectName))
                    {
                        var _item = bgw_BuildOutputFile_Argument.Item1.Where(x => x.projectName == item.Key);

                        bgw_BuildOutputFile.ReportProgress(2, "Building Output file Name");
                        var _outputFileName = buildOutputFileName(
                                    bgw_BuildOutputFile_Argument.fileNamePattern,
                                    bgw_BuildOutputFile_Argument.userName,
                                    bgw_BuildOutputFile_Argument.saveSelectedPath,
                                    int.MinValue,
                                    int.MinValue,
                                    item.Key);

                        bgw_BuildOutputFile.ReportProgress(3, "Writting final file");
                        buildOutputFile(_item.Select(x => x.tempFileName),
                            _outputFileName.filePath);

                        result.Add(_outputFileName);
                    }

                    break;
                case ExecutionType.rb_DateUpToNow_rb_SingleFileByMonthAndYear:
                case ExecutionType.rb_SingleMonth_rb_SingleFileByMonthAndYear:
                case ExecutionType.rb_DateRange_rb_SingleFileByMonthAndYear:
                    foreach (var item in bgw_BuildOutputFile_Argument.Item1.GroupBy(x => x.dateTo))
                    {
                        var _item = bgw_BuildOutputFile_Argument.Item1.Where(x => x.dateTo == item.Key);

                        bgw_BuildOutputFile.ReportProgress(2, "Building Output file Name");
                        var _outputFileName = buildOutputFileName(
                                    bgw_BuildOutputFile_Argument.fileNamePattern,
                                    bgw_BuildOutputFile_Argument.userName,
                                    bgw_BuildOutputFile_Argument.saveSelectedPath,
                                    item.Key.Month,
                                    item.Key.Year);

                        bgw_BuildOutputFile.ReportProgress(3, "Writting final file");
                        buildOutputFile(_item.Select(x => x.tempFileName),
                            _outputFileName.filePath);

                        result.Add(_outputFileName);
                    }
                    break;
                case ExecutionType.rb_DateUpToNow_rb_GroupByProjectAndMonth:
                case ExecutionType.rb_SingleMonth_rb_GroupByProjectAndMonth:
                case ExecutionType.rb_DateRange_rb_GroupByProjectAndMonth:
                    foreach (var _item in bgw_BuildOutputFile_Argument.Item1.GroupBy(x => x.projectName))
                        foreach (var item in bgw_BuildOutputFile_Argument.Item1.GroupBy(x => x.dateTo))
                        {
                            var (tempFileName, projectName, dateFrom, dateTo) = bgw_BuildOutputFile_Argument.Item1.SingleOrDefault(x => x.projectName == _item.Key && x.dateTo == item.Key);

                            bgw_BuildOutputFile.ReportProgress(2, "Building Output file Name");
                            var _outputFileName = buildOutputFileName(
                                    bgw_BuildOutputFile_Argument.fileNamePattern,
                                    bgw_BuildOutputFile_Argument.userName,
                                    bgw_BuildOutputFile_Argument.saveSelectedPath,
                                    item.Key.Month,
                                    item.Key.Year,
                                    _item.Key);

                            bgw_BuildOutputFile.ReportProgress(3, "Writting final file");
                            buildOutputFile(
                                new List<string> { tempFileName },
                               _outputFileName.filePath);

                            result.Add(_outputFileName);
                        }
                    break;
            }
            e.Result = result;

            bgw_BuildOutputFile.ReportProgress(100);
        }

        private void bgw_BuildOutputFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                var bgw_BuildOutputFile_DoWork_Result = (List<(string filePath, string fileName)>)e.Result;

                if (chkTryToUpload.Checked)
                    bgw_UploadToDrive.RunWorkerAsync((bgw_BuildOutputFile_DoWork_Result, txt_DriveFolderName.Text));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (!chkTryToUpload.Checked)
                {
                    updateAppSettings();
                    asyncTaskIsRunning(false);
                }

                if (e.Error != null)
                {
                    MessageBox.Show("bgw_BuildOutputFile - " + e.Error.Message);
                    asyncTaskIsRunning(false);
                }
            }
        }

        private void bgw_UploadToDrive_DoWork(object sender, DoWorkEventArgs e)
        {
            bgw_UploadToDrive.ReportProgress(0);

            bgw_UploadToDrive.ReportProgress(1, "bgw_UploadToDrive - Parsing Argument");
            var bgw_UploadToDrive_Argument = ((List<(string filePath, string fileName)>, string driveFolderName))e.Argument;

            bgw_UploadToDrive.ReportProgress(2, "Uploading Files...");
            foreach (var (filePath, fileName) in bgw_UploadToDrive_Argument.Item1)
            {
                bgw_UploadToDrive.ReportProgress(3, $"Uploading File {fileName}");
                var partialUploadResult = _googleDriveUploaderService.UploadFile(filePath, fileName, bgw_UploadToDrive_Argument.driveFolderName);
                bgw_UploadToDrive.ReportProgress(4, $"Status: {partialUploadResult.uploadProgress.Status} - File {fileName}");
            }

            bgw_UploadToDrive.ReportProgress(100);
        }

        private void bgw_UploadToDrive_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try { updateAppSettings(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (e.Error != null) MessageBox.Show("bgw_BuildOutputFile - " + e.Error.Message);

                asyncTaskIsRunning(false);
            }
        }

        private void setInitialValues()
        {
            rb_DateRange.Checked = true;
            rb_SingleFileByProject.Checked = true;
            fbdSearchProjectFilePath.Description = "Select where are the git Projects";
            fbdSearchProjectFilePath.UseDescriptionForTitle = true;
            fbdGenerateBeautifulFile.Description = "Select where do you want to store the generated files";
            fbdGenerateBeautifulFile.UseDescriptionForTitle = true;
            cbo_PrettierFormat.SelectedItem = "email";
        }

        private void setLatestsValues()
        {
            if (!string.IsNullOrEmpty(_appSettingsModel.UserName))
                txt_UserName.Text = _appSettingsModel.UserName;

            if (DateTime.TryParse(_appSettingsModel.LastDateFromUsed.ToString(), out var _LastDateFromUsed))
                if (_LastDateFromUsed != DateTime.MinValue)
                    dtpDateFrom.Value = _LastDateFromUsed.Date;

            if (DateTime.TryParse(_appSettingsModel.LastDateToUsed.ToString(), out var _LastDateToUsed))
                if (_LastDateToUsed != DateTime.MinValue)
                    dtpDateTo.Value = _LastDateToUsed.Date;

            switch (_appSettingsModel.Step1DateSelectionType)
            {
                case 1:
                    rb_DateRange.Checked = true;
                    break;
                case 2:
                    rb_SingleMonth.Checked = true;
                    break;
                case 3:
                    rb_DateUpToNow.Checked = true;
                    break;
            }

            if (!string.IsNullOrEmpty(_appSettingsModel.ProjectFolderPath) && Directory.Exists(_appSettingsModel.ProjectFolderPath))
                fbdSearchProjectFilePath.SelectedPath = _appSettingsModel.ProjectFolderPath;
            else
                fbdSearchProjectFilePath.SelectedPath = null;

            if (_appSettingsModel.ProjectFilePaths != null && _appSettingsModel.ProjectFilePaths.Length > 0)
                foreach (var item in _appSettingsModel.ProjectFilePaths.Where(x => Directory.Exists(x.projectPath)))
                {
                    var newItem = (item.parentPath, item.projectPath, item.projectName);
                    lbAddedProjectFilePaths.Items.Add(newItem);
                }

            switch (_appSettingsModel.Step5GroupByType)
            {
                case 1:
                    rb_SingleFileByProject.Checked = true;
                    break;
                case 2:
                    rb_SingleFileByMonthAndYear.Checked = true;
                    break;
                case 3:
                    rb_GroupByProjectAndMonth.Checked = true;
                    break;
            }

            if (!string.IsNullOrEmpty(_appSettingsModel.Step6FileNamePattern))
                txtFileNamePattern.Text = _appSettingsModel.Step6FileNamePattern;

            if (_appSettingsModel.installed != null && !string.IsNullOrEmpty(_appSettingsModel.installed.client_id) && !string.IsNullOrEmpty(_appSettingsModel.installed.client_secret) && _appSettingsModel.TryToUpload)
            {
                chkTryToUpload.Checked = _appSettingsModel.TryToUpload;

                if (!string.IsNullOrEmpty(_appSettingsModel.DriveFolderName) && _appSettingsModel.TryToUpload)
                    txt_DriveFolderName.Text = _appSettingsModel.DriveFolderName;
            }
            else
                chkTryToUpload.Enabled = false;

            if (!string.IsNullOrEmpty(_appSettingsModel.SaveFilePath) && Directory.Exists(_appSettingsModel.SaveFilePath))
                fbdGenerateBeautifulFile.SelectedPath = _appSettingsModel.SaveFilePath;
            else
                fbdGenerateBeautifulFile.SelectedPath = null;
        }

        private void setStep2DateSetVisibility(bool dateUpToNow, bool singleMonth, bool dateRange)
        {
            try
            {
                if (dateUpToNow && !singleMonth && !dateRange)
                {
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = false;

                    dtpDateFrom.MaxDate = DateTime.UtcNow.AddDays(-1).Date;

                    dtpDateTo.Value = DateTime.UtcNow.Date;

                    dtpDateTo.Enabled = false;
                }
                else if (!dateUpToNow && singleMonth && !dateRange)
                {
                    dtpDateTo.Enabled = false;

                    dtpDateFrom.MaxDate = DateTime.UtcNow.Date;

                    dtpDateTo.Value = dtpDateFrom.Value.AddDays(1);

                    dtpDateTo.Enabled = false;
                }
                else if (!dateUpToNow && !singleMonth && dateRange)
                {
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;

                    dtpDateFrom.MaxDate = DateTime.UtcNow.AddDays(-1).Date;
                    dtpDateTo.MaxDate = DateTime.UtcNow.Date;

                    dtpDateTo.Value = DateTime.UtcNow.Date;

                    dtpDateTo.Enabled = true;
                }
                else { throw new InvalidOperationException("setStep2DateSetVisibility"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private IEnumerable<(string parentPath, string projectPath, string projectName)> getProjectsByExtensions(IEnumerable<(string parentPath, string projectPath, string projectName)> existingLBAddedProjectFilePaths, string projectsFilePath, string extension)
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

        private void asyncTaskIsRunning(bool asyncTaskIsRunning)
        {
            pb_Process.Visible = asyncTaskIsRunning;
            txt_UserName.Enabled = !asyncTaskIsRunning;
            gb_DateSettings.Enabled = !asyncTaskIsRunning;
            gb_FileProjectPathSettings.Enabled = !asyncTaskIsRunning;
            gb_FileGenerationSettings.Enabled = !asyncTaskIsRunning;
            gb_CloudSettings.Enabled = !asyncTaskIsRunning;
            btnGenerate.Enabled = !asyncTaskIsRunning;
        }

        private void setStep6GeneratedFileNamePattern(bool singleFileByProject, bool singleFileByMonthAndYear, bool groupByProjectAndMonth)
        {
            try
            {
                if (singleFileByProject && !singleFileByMonthAndYear && !groupByProjectAndMonth)
                    txtFileNamePattern.Text = "E.T {UserName} - {ProjectName}.txt";
                else if (!singleFileByProject && singleFileByMonthAndYear && !groupByProjectAndMonth)
                    txtFileNamePattern.Text = "E.T {UserName} - {Month}_{Year}.txt";
                else if (!singleFileByProject && !singleFileByMonthAndYear && groupByProjectAndMonth)
                    txtFileNamePattern.Text = "E.T {UserName} - {ProjectName} - {Month}_{Year}.txt";
                else { throw new InvalidOperationException("setStep6GeneratedFileNamePattern"); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void setStep2DateSetVisibility_Validate()
        {
            if (string.IsNullOrEmpty(txt_UserName.Text))
                throw new ArgumentNullException("Version Control Account User Name cannot be null");

            if (dtpDateTo.Value <= dtpDateFrom.Value)
                throw new InvalidDataException("Date From cannot be less or equal than Date To");
        }

        private void btnAddProjectFilePath_Click_Validate()
        {
            if (string.IsNullOrEmpty(txtProjectFilePath.Text))
                throw new ArgumentNullException("Step 3 - Project Folder Paths cannot be null");
        }

        private void setStep6GeneratedFileNamePattern_Validate()
        {
            if (lbAddedProjectFilePaths.Items.Count <= 0)
                throw new ArgumentNullException("Step 4 - Available Project Paths cannot be null");

            if (string.IsNullOrEmpty(txtFileNamePattern.Text))
                throw new ArgumentNullException("Step 6 - Generated File Name Pattern cannot be null");
        }

        private void gb_CloudSettings_Validate()
        {
            if (chkTryToUpload.Checked && string.IsNullOrEmpty(txt_DriveFolderName.Text))
                throw new ArgumentNullException("Step - 6* - Drive Root Folder Name: cannot be null");
        }

        private (string gitCommand, string tempFileName, string projectName, DateTime dateFrom, DateTime dateTo) buildGitCommand(DateTime dateFrom, DateTime dateTo, string userName, string projectName, string prettierFormat)
        {
            var tempFileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";

            var formattedDayDateFrom = dateFrom.Day < 10 ? "0" + dateFrom.Day.ToString() : dateFrom.Day.ToString();
            var formattedDayDateTo = dateTo.Day < 10 ? "0" + dateTo.Day.ToString() : dateTo.Day.ToString();

            var formattedMonthDateFrom = dateFrom.Month < 10 ? "0" + dateFrom.Month.ToString() : dateFrom.Month.ToString();
            var formattedMonthDateTo = dateTo.Month < 10 ? "0" + dateTo.Month.ToString() : dateTo.Month.ToString();
            var _prettierFormat = string.IsNullOrEmpty(prettierFormat) ? "email" : prettierFormat;

            var gitCommand = $@"git log --author='{userName}*' --pretty={_prettierFormat} --since='{dateFrom.Year}-{formattedMonthDateFrom}-{formattedDayDateFrom}' --until='{dateTo.Year}-{formattedMonthDateTo}-{formattedDayDateTo}' > {tempFileName}";

            return (gitCommand, tempFileName, projectName, dateFrom, dateTo);
        }

        private (string tempFileName, string projectName, DateTime dateFrom, DateTime dateTo) _bgw_RunGitCommand_DoWork(BackgroundWorker _bgw_RunGitCommand, string projectPath, DateTime dateFrom, DateTime dateTo, string username, string projectName, string prettierFormat)
        {
            _bgw_RunGitCommand.ReportProgress(3, "Creating PS Console");
            using PowerShell powershell = PowerShell.Create();

            _bgw_RunGitCommand.ReportProgress(4, $"Adding Script for Project Path: {projectPath}");
            powershell.AddScript($"cd {projectPath}");

            _bgw_RunGitCommand.ReportProgress(5, "Building Git Command");
            var (gitCommand, tempFileName, _projectName, _dateFrom, _dateTo) = buildGitCommand(dateFrom, dateTo, username, projectName, prettierFormat);

            _bgw_RunGitCommand.ReportProgress(6, $"Adding Script to PS Console. Git Command:{ gitCommand}");
            powershell.AddScript(gitCommand);

            _bgw_RunGitCommand.ReportProgress(7, "Executing Script");
            powershell.Invoke();

            var resultItem = (tempFileName, projectName, dateFrom, dateTo);
            _bgw_RunGitCommand.ReportProgress(8, "Getting Results." + "tempFileName:" + tempFileName + ".projectName:" + projectName + ".dateFrom:" + dateFrom.Date.ToString() + ".dateTo:" + dateTo.Date.ToString());

            return resultItem;
        }
        private ExecutionType getExecutionType()
        {
            if (rb_DateRange.Checked)
            {
                if (rb_SingleFileByProject.Checked)
                    return ExecutionType.rb_DateRange_rb_SingleFileByProject;
                else if (rb_SingleFileByMonthAndYear.Checked)
                    return ExecutionType.rb_DateRange_rb_SingleFileByMonthAndYear;
                else if (rb_GroupByProjectAndMonth.Checked)
                    return ExecutionType.rb_DateRange_rb_GroupByProjectAndMonth;
            }
            else if (rb_SingleMonth.Checked)
            {
                if (rb_SingleFileByProject.Checked)
                    return ExecutionType.rb_SingleMonth_rb_SingleFileByProject;
                else if (rb_SingleFileByMonthAndYear.Checked)
                    return ExecutionType.rb_SingleMonth_rb_SingleFileByMonthAndYear;
                else if (rb_GroupByProjectAndMonth.Checked)
                    return ExecutionType.rb_SingleMonth_rb_GroupByProjectAndMonth;
            }
            else if (rb_DateUpToNow.Checked)
            {
                if (rb_SingleFileByProject.Checked)
                    return ExecutionType.rb_DateUpToNow_rb_SingleFileByProject;
                else if (rb_SingleFileByMonthAndYear.Checked)
                    return ExecutionType.rb_DateUpToNow_rb_SingleFileByMonthAndYear;
                else if (rb_GroupByProjectAndMonth.Checked)
                    return ExecutionType.rb_DateUpToNow_rb_GroupByProjectAndMonth;
            }

            throw new InvalidCastException("Cannot Find a Valid Execution Type");
        }

        IEnumerable<(int Month, int Year)> getMonthsBetween(DateTime startDate, DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            while (iterator <= limit)
            {
                yield return (
                    iterator.Month,
                    iterator.Year
                );

                iterator = iterator.AddMonths(1);
            }
        }

        private string buildOutputFile(IEnumerable<string> tempFilePaths, string outputFileName)
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

        private (string filePath, string fileName) buildOutputFileName(string fileNamePattern, string userName, string saveSelectedPath, int month = int.MinValue, int year = int.MinValue, string projectName = null)
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

        private void updateAppSettings()
        {
            _appSettingsModel.UserName = txt_UserName.Text;

            if (rb_DateRange.Checked)
                _appSettingsModel.Step1DateSelectionType = 1;
            else if (rb_SingleMonth.Checked)
                _appSettingsModel.Step1DateSelectionType = 2;
            else if (rb_DateUpToNow.Checked)
                _appSettingsModel.Step1DateSelectionType = 3;

            _appSettingsModel.LastDateFromUsed = dtpDateFrom.Value;
            _appSettingsModel.LastDateToUsed = dtpDateTo.Value;

            _appSettingsModel.ProjectFolderPath = fbdSearchProjectFilePath.SelectedPath;

            if (_appSettingsModel.ProjectFilePaths == null)
                _appSettingsModel.ProjectFilePaths = new List<ProjectFilePath>().ToArray();

            _appSettingsModel.ProjectFilePaths = lbAddedProjectFilePaths.Items
                .Cast<(string parentPath, string projectPath, string projectName)>()
                .Select(x => new ProjectFilePath() { projectPath = x.projectPath, parentPath = x.parentPath, projectName = x.projectName })
                .ToArray();

            if (rb_SingleFileByProject.Checked)
                _appSettingsModel.Step5GroupByType = 1;
            else if (rb_SingleFileByMonthAndYear.Checked)
                _appSettingsModel.Step5GroupByType = 2;
            else if (rb_GroupByProjectAndMonth.Checked)
                _appSettingsModel.Step5GroupByType = 3;

            _appSettingsModel.Step6FileNamePattern = txtFileNamePattern.Text;
            _appSettingsModel.PrettierFormat = cbo_PrettierFormat.SelectedItem.ToString();

            _appSettingsModel.TryToUpload = chkTryToUpload.Checked;
            _appSettingsModel.DriveFolderName = txt_DriveFolderName.Text;

            _appSettingsModel.SaveFilePath = fbdGenerateBeautifulFile.SelectedPath;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appSettings.json");
            var output = Newtonsoft.Json.JsonConvert.SerializeObject(_appSettingsModel, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
        }
    }
}
