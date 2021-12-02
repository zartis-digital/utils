
namespace ZartisLogs
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.gb_DateSettings = new System.Windows.Forms.GroupBox();
            this.gb_Step2DateSet = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.gb_Step1DateSelectionType = new System.Windows.Forms.GroupBox();
            this.rb_DateUpToNow = new System.Windows.Forms.RadioButton();
            this.rb_SingleMonth = new System.Windows.Forms.RadioButton();
            this.rb_DateRange = new System.Windows.Forms.RadioButton();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.gb_ConsoleLog = new System.Windows.Forms.GroupBox();
            this.pb_Process = new System.Windows.Forms.ProgressBar();
            this.txt_ConsoleLog = new System.Windows.Forms.TextBox();
            this.gb_FileProjectPathSettings = new System.Windows.Forms.GroupBox();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.lblAddedProjectFilePaths = new System.Windows.Forms.Label();
            this.lbAddedProjectFilePaths = new System.Windows.Forms.ListBox();
            this.chkRecursiveSearch = new System.Windows.Forms.CheckBox();
            this.txtProjectFilePath = new System.Windows.Forms.TextBox();
            this.btnSearchProjectFilePath = new System.Windows.Forms.Button();
            this.btnAddProjectFilePath = new System.Windows.Forms.Button();
            this.lblProjectFolder = new System.Windows.Forms.Label();
            this.fbdSearchProjectFilePath = new System.Windows.Forms.FolderBrowserDialog();
            this.bgw_AddedProjectFilePaths = new System.ComponentModel.BackgroundWorker();
            this.gb_FileGenerationSettings = new System.Windows.Forms.GroupBox();
            this.cbo_PrettierFormat = new System.Windows.Forms.ComboBox();
            this.txtFileNamePattern = new System.Windows.Forms.TextBox();
            this.gb_GroupBy = new System.Windows.Forms.GroupBox();
            this.rb_GroupByProjectAndMonth = new System.Windows.Forms.RadioButton();
            this.rb_SingleFileByMonthAndYear = new System.Windows.Forms.RadioButton();
            this.rb_SingleFileByProject = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFileNamePattern = new System.Windows.Forms.Label();
            this.gb_CloudSettings = new System.Windows.Forms.GroupBox();
            this.lbl_DriveFolderName = new System.Windows.Forms.Label();
            this.txt_DriveFolderName = new System.Windows.Forms.TextBox();
            this.chkTryToUpload = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.fbdGenerateBeautifulFile = new System.Windows.Forms.FolderBrowserDialog();
            this.bgw_RunGitCommand = new System.ComponentModel.BackgroundWorker();
            this.bgw_BuildOutputFile = new System.ComponentModel.BackgroundWorker();
            this.bgw_UploadToDrive = new System.ComponentModel.BackgroundWorker();
            this.gb_DateSettings.SuspendLayout();
            this.gb_Step2DateSet.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gb_Step1DateSelectionType.SuspendLayout();
            this.gb_ConsoleLog.SuspendLayout();
            this.gb_FileProjectPathSettings.SuspendLayout();
            this.gb_FileGenerationSettings.SuspendLayout();
            this.gb_GroupBy.SuspendLayout();
            this.gb_CloudSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(3, 18);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.PlaceholderText = "For E.G: manuel.dinucci@vizor...";
            this.txt_UserName.Size = new System.Drawing.Size(556, 23);
            this.txt_UserName.TabIndex = 1;
            // 
            // gb_DateSettings
            // 
            this.gb_DateSettings.Controls.Add(this.gb_Step2DateSet);
            this.gb_DateSettings.Controls.Add(this.gb_Step1DateSelectionType);
            this.gb_DateSettings.Location = new System.Drawing.Point(3, 47);
            this.gb_DateSettings.Name = "gb_DateSettings";
            this.gb_DateSettings.Size = new System.Drawing.Size(556, 109);
            this.gb_DateSettings.TabIndex = 2;
            this.gb_DateSettings.TabStop = false;
            this.gb_DateSettings.Text = "Date Settings";
            // 
            // gb_Step2DateSet
            // 
            this.gb_Step2DateSet.Controls.Add(this.tableLayoutPanel1);
            this.gb_Step2DateSet.Dock = System.Windows.Forms.DockStyle.Left;
            this.gb_Step2DateSet.Location = new System.Drawing.Point(172, 19);
            this.gb_Step2DateSet.Name = "gb_Step2DateSet";
            this.gb_Step2DateSet.Size = new System.Drawing.Size(369, 87);
            this.gb_Step2DateSet.TabIndex = 3;
            this.gb_Step2DateSet.TabStop = false;
            this.gb_Step2DateSet.Text = "Step 2 - Date Set";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.4876F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.5124F));
            this.tableLayoutPanel1.Controls.Add(this.lblDateTo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDateFrom, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDateFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDateTo, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 65);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(3, 32);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(49, 15);
            this.lblDateTo.TabIndex = 4;
            this.lblDateTo.Text = "Date To:";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(3, 0);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(65, 15);
            this.lblDateFrom.TabIndex = 3;
            this.lblDateFrom.Text = "Date From:";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateFrom.Location = new System.Drawing.Point(80, 3);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(280, 23);
            this.dtpDateFrom.TabIndex = 0;
            this.dtpDateFrom.ValueChanged += new System.EventHandler(this.dtpDateFrom_ValueChanged);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateTo.Location = new System.Drawing.Point(80, 35);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(280, 23);
            this.dtpDateTo.TabIndex = 0;
            // 
            // gb_Step1DateSelectionType
            // 
            this.gb_Step1DateSelectionType.Controls.Add(this.rb_DateUpToNow);
            this.gb_Step1DateSelectionType.Controls.Add(this.rb_SingleMonth);
            this.gb_Step1DateSelectionType.Controls.Add(this.rb_DateRange);
            this.gb_Step1DateSelectionType.Dock = System.Windows.Forms.DockStyle.Left;
            this.gb_Step1DateSelectionType.Location = new System.Drawing.Point(3, 19);
            this.gb_Step1DateSelectionType.Name = "gb_Step1DateSelectionType";
            this.gb_Step1DateSelectionType.Size = new System.Drawing.Size(169, 87);
            this.gb_Step1DateSelectionType.TabIndex = 2;
            this.gb_Step1DateSelectionType.TabStop = false;
            this.gb_Step1DateSelectionType.Text = "Step 1 - Date Selection Type";
            // 
            // rb_DateUpToNow
            // 
            this.rb_DateUpToNow.AutoSize = true;
            this.rb_DateUpToNow.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_DateUpToNow.Location = new System.Drawing.Point(3, 57);
            this.rb_DateUpToNow.Name = "rb_DateUpToNow";
            this.rb_DateUpToNow.Size = new System.Drawing.Size(163, 19);
            this.rb_DateUpToNow.TabIndex = 1;
            this.rb_DateUpToNow.TabStop = true;
            this.rb_DateUpToNow.Text = "Up To Now";
            this.rb_DateUpToNow.UseVisualStyleBackColor = true;
            this.rb_DateUpToNow.CheckedChanged += new System.EventHandler(this.rb_DateUpToNow_CheckedChanged);
            // 
            // rb_SingleMonth
            // 
            this.rb_SingleMonth.AutoSize = true;
            this.rb_SingleMonth.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_SingleMonth.Location = new System.Drawing.Point(3, 38);
            this.rb_SingleMonth.Name = "rb_SingleMonth";
            this.rb_SingleMonth.Size = new System.Drawing.Size(163, 19);
            this.rb_SingleMonth.TabIndex = 0;
            this.rb_SingleMonth.TabStop = true;
            this.rb_SingleMonth.Text = "Single";
            this.rb_SingleMonth.UseVisualStyleBackColor = true;
            this.rb_SingleMonth.CheckedChanged += new System.EventHandler(this.rb_SingleMonth_CheckedChanged);
            // 
            // rb_DateRange
            // 
            this.rb_DateRange.AutoSize = true;
            this.rb_DateRange.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_DateRange.Location = new System.Drawing.Point(3, 19);
            this.rb_DateRange.Name = "rb_DateRange";
            this.rb_DateRange.Size = new System.Drawing.Size(163, 19);
            this.rb_DateRange.TabIndex = 0;
            this.rb_DateRange.TabStop = true;
            this.rb_DateRange.Text = "Date Range";
            this.rb_DateRange.UseVisualStyleBackColor = true;
            this.rb_DateRange.CheckedChanged += new System.EventHandler(this.rb_DateRange_CheckedChanged);
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_UserName.Location = new System.Drawing.Point(0, 0);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(197, 15);
            this.lbl_UserName.TabIndex = 3;
            this.lbl_UserName.Text = "Version Control Account User Name";
            // 
            // gb_ConsoleLog
            // 
            this.gb_ConsoleLog.Controls.Add(this.pb_Process);
            this.gb_ConsoleLog.Controls.Add(this.txt_ConsoleLog);
            this.gb_ConsoleLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.gb_ConsoleLog.Location = new System.Drawing.Point(559, 15);
            this.gb_ConsoleLog.Name = "gb_ConsoleLog";
            this.gb_ConsoleLog.Size = new System.Drawing.Size(418, 801);
            this.gb_ConsoleLog.TabIndex = 4;
            this.gb_ConsoleLog.TabStop = false;
            this.gb_ConsoleLog.Text = "Log";
            // 
            // pb_Process
            // 
            this.pb_Process.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_Process.Location = new System.Drawing.Point(3, 735);
            this.pb_Process.Name = "pb_Process";
            this.pb_Process.Size = new System.Drawing.Size(412, 63);
            this.pb_Process.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_Process.TabIndex = 15;
            this.pb_Process.Visible = false;
            // 
            // txt_ConsoleLog
            // 
            this.txt_ConsoleLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_ConsoleLog.Location = new System.Drawing.Point(3, 19);
            this.txt_ConsoleLog.Multiline = true;
            this.txt_ConsoleLog.Name = "txt_ConsoleLog";
            this.txt_ConsoleLog.ReadOnly = true;
            this.txt_ConsoleLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_ConsoleLog.Size = new System.Drawing.Size(412, 715);
            this.txt_ConsoleLog.TabIndex = 0;
            // 
            // gb_FileProjectPathSettings
            // 
            this.gb_FileProjectPathSettings.Controls.Add(this.btnRemoveSelected);
            this.gb_FileProjectPathSettings.Controls.Add(this.lblAddedProjectFilePaths);
            this.gb_FileProjectPathSettings.Controls.Add(this.lbAddedProjectFilePaths);
            this.gb_FileProjectPathSettings.Controls.Add(this.chkRecursiveSearch);
            this.gb_FileProjectPathSettings.Controls.Add(this.txtProjectFilePath);
            this.gb_FileProjectPathSettings.Controls.Add(this.btnSearchProjectFilePath);
            this.gb_FileProjectPathSettings.Controls.Add(this.btnAddProjectFilePath);
            this.gb_FileProjectPathSettings.Controls.Add(this.lblProjectFolder);
            this.gb_FileProjectPathSettings.Location = new System.Drawing.Point(3, 160);
            this.gb_FileProjectPathSettings.Name = "gb_FileProjectPathSettings";
            this.gb_FileProjectPathSettings.Size = new System.Drawing.Size(556, 287);
            this.gb_FileProjectPathSettings.TabIndex = 5;
            this.gb_FileProjectPathSettings.TabStop = false;
            this.gb_FileProjectPathSettings.Text = "File Project Path Settings";
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(452, 79);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(92, 23);
            this.btnRemoveSelected.TabIndex = 14;
            this.btnRemoveSelected.Text = "Remove";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // lblAddedProjectFilePaths
            // 
            this.lblAddedProjectFilePaths.AutoSize = true;
            this.lblAddedProjectFilePaths.Location = new System.Drawing.Point(6, 63);
            this.lblAddedProjectFilePaths.Name = "lblAddedProjectFilePaths";
            this.lblAddedProjectFilePaths.Size = new System.Drawing.Size(170, 15);
            this.lblAddedProjectFilePaths.TabIndex = 13;
            this.lblAddedProjectFilePaths.Text = "Step 4 - Available Project Paths";
            // 
            // lbAddedProjectFilePaths
            // 
            this.lbAddedProjectFilePaths.AllowDrop = true;
            this.lbAddedProjectFilePaths.FormattingEnabled = true;
            this.lbAddedProjectFilePaths.HorizontalScrollbar = true;
            this.lbAddedProjectFilePaths.ItemHeight = 15;
            this.lbAddedProjectFilePaths.Location = new System.Drawing.Point(6, 79);
            this.lbAddedProjectFilePaths.Name = "lbAddedProjectFilePaths";
            this.lbAddedProjectFilePaths.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAddedProjectFilePaths.Size = new System.Drawing.Size(440, 199);
            this.lbAddedProjectFilePaths.TabIndex = 12;
            // 
            // chkRecursiveSearch
            // 
            this.chkRecursiveSearch.AutoSize = true;
            this.chkRecursiveSearch.Location = new System.Drawing.Point(332, 40);
            this.chkRecursiveSearch.Name = "chkRecursiveSearch";
            this.chkRecursiveSearch.Size = new System.Drawing.Size(114, 19);
            this.chkRecursiveSearch.TabIndex = 11;
            this.chkRecursiveSearch.Text = "Recursive Search";
            this.chkRecursiveSearch.UseVisualStyleBackColor = true;
            // 
            // txtProjectFilePath
            // 
            this.txtProjectFilePath.Location = new System.Drawing.Point(6, 37);
            this.txtProjectFilePath.Name = "txtProjectFilePath";
            this.txtProjectFilePath.Size = new System.Drawing.Size(288, 23);
            this.txtProjectFilePath.TabIndex = 8;
            // 
            // btnSearchProjectFilePath
            // 
            this.btnSearchProjectFilePath.Location = new System.Drawing.Point(300, 36);
            this.btnSearchProjectFilePath.Name = "btnSearchProjectFilePath";
            this.btnSearchProjectFilePath.Size = new System.Drawing.Size(26, 24);
            this.btnSearchProjectFilePath.TabIndex = 9;
            this.btnSearchProjectFilePath.Text = "...";
            this.btnSearchProjectFilePath.UseVisualStyleBackColor = true;
            this.btnSearchProjectFilePath.Click += new System.EventHandler(this.btnSearchProjectFilePath_Click);
            // 
            // btnAddProjectFilePath
            // 
            this.btnAddProjectFilePath.Location = new System.Drawing.Point(452, 37);
            this.btnAddProjectFilePath.Name = "btnAddProjectFilePath";
            this.btnAddProjectFilePath.Size = new System.Drawing.Size(92, 23);
            this.btnAddProjectFilePath.TabIndex = 10;
            this.btnAddProjectFilePath.Text = "Add File Path";
            this.btnAddProjectFilePath.UseVisualStyleBackColor = true;
            this.btnAddProjectFilePath.Click += new System.EventHandler(this.btnAddProjectFilePath_Click);
            // 
            // lblProjectFolder
            // 
            this.lblProjectFolder.AutoSize = true;
            this.lblProjectFolder.Location = new System.Drawing.Point(6, 19);
            this.lblProjectFolder.Name = "lblProjectFolder";
            this.lblProjectFolder.Size = new System.Drawing.Size(155, 15);
            this.lblProjectFolder.TabIndex = 0;
            this.lblProjectFolder.Text = "Step 3 - Project Folder Paths";
            // 
            // bgw_AddedProjectFilePaths
            // 
            this.bgw_AddedProjectFilePaths.WorkerReportsProgress = true;
            this.bgw_AddedProjectFilePaths.WorkerSupportsCancellation = true;
            this.bgw_AddedProjectFilePaths.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_AddedProjectFilePaths_DoWork);
            this.bgw_AddedProjectFilePaths.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
            this.bgw_AddedProjectFilePaths.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_AddedProjectFilePaths_RunWorkerCompleted);
            // 
            // gb_FileGenerationSettings
            // 
            this.gb_FileGenerationSettings.Controls.Add(this.cbo_PrettierFormat);
            this.gb_FileGenerationSettings.Controls.Add(this.txtFileNamePattern);
            this.gb_FileGenerationSettings.Controls.Add(this.gb_GroupBy);
            this.gb_FileGenerationSettings.Controls.Add(this.label1);
            this.gb_FileGenerationSettings.Controls.Add(this.lblFileNamePattern);
            this.gb_FileGenerationSettings.Location = new System.Drawing.Point(3, 453);
            this.gb_FileGenerationSettings.Name = "gb_FileGenerationSettings";
            this.gb_FileGenerationSettings.Size = new System.Drawing.Size(556, 193);
            this.gb_FileGenerationSettings.TabIndex = 6;
            this.gb_FileGenerationSettings.TabStop = false;
            this.gb_FileGenerationSettings.Text = "File Generation Settings";
            // 
            // cbo_PrettierFormat
            // 
            this.cbo_PrettierFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_PrettierFormat.FormattingEnabled = true;
            this.cbo_PrettierFormat.Items.AddRange(new object[] {
            "oneline",
            "short",
            "medium",
            "full",
            "fuller",
            "reference",
            "email"});
            this.cbo_PrettierFormat.Location = new System.Drawing.Point(3, 165);
            this.cbo_PrettierFormat.Name = "cbo_PrettierFormat";
            this.cbo_PrettierFormat.Size = new System.Drawing.Size(550, 23);
            this.cbo_PrettierFormat.TabIndex = 15;
            // 
            // txtFileNamePattern
            // 
            this.txtFileNamePattern.Enabled = false;
            this.txtFileNamePattern.Location = new System.Drawing.Point(3, 118);
            this.txtFileNamePattern.Name = "txtFileNamePattern";
            this.txtFileNamePattern.Size = new System.Drawing.Size(550, 23);
            this.txtFileNamePattern.TabIndex = 14;
            // 
            // gb_GroupBy
            // 
            this.gb_GroupBy.Controls.Add(this.rb_GroupByProjectAndMonth);
            this.gb_GroupBy.Controls.Add(this.rb_SingleFileByMonthAndYear);
            this.gb_GroupBy.Controls.Add(this.rb_SingleFileByProject);
            this.gb_GroupBy.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_GroupBy.Location = new System.Drawing.Point(3, 19);
            this.gb_GroupBy.Name = "gb_GroupBy";
            this.gb_GroupBy.Size = new System.Drawing.Size(550, 78);
            this.gb_GroupBy.TabIndex = 12;
            this.gb_GroupBy.TabStop = false;
            this.gb_GroupBy.Text = "Step  5 - Group By";
            // 
            // rb_GroupByProjectAndMonth
            // 
            this.rb_GroupByProjectAndMonth.AutoSize = true;
            this.rb_GroupByProjectAndMonth.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_GroupByProjectAndMonth.Location = new System.Drawing.Point(3, 57);
            this.rb_GroupByProjectAndMonth.Name = "rb_GroupByProjectAndMonth";
            this.rb_GroupByProjectAndMonth.Size = new System.Drawing.Size(544, 19);
            this.rb_GroupByProjectAndMonth.TabIndex = 1;
            this.rb_GroupByProjectAndMonth.Text = "Each Project/Month in a Single File ";
            this.rb_GroupByProjectAndMonth.UseVisualStyleBackColor = true;
            this.rb_GroupByProjectAndMonth.CheckedChanged += new System.EventHandler(this.rb_GroupByProjectAndMonth_CheckedChanged);
            // 
            // rb_SingleFileByMonthAndYear
            // 
            this.rb_SingleFileByMonthAndYear.AutoSize = true;
            this.rb_SingleFileByMonthAndYear.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_SingleFileByMonthAndYear.Location = new System.Drawing.Point(3, 38);
            this.rb_SingleFileByMonthAndYear.Name = "rb_SingleFileByMonthAndYear";
            this.rb_SingleFileByMonthAndYear.Size = new System.Drawing.Size(544, 19);
            this.rb_SingleFileByMonthAndYear.TabIndex = 1;
            this.rb_SingleFileByMonthAndYear.Text = "Each Month and Year in a Single File (All Projects Together)";
            this.rb_SingleFileByMonthAndYear.UseVisualStyleBackColor = true;
            this.rb_SingleFileByMonthAndYear.CheckedChanged += new System.EventHandler(this.rb_SingleFileByMonthAndYear_CheckedChanged);
            // 
            // rb_SingleFileByProject
            // 
            this.rb_SingleFileByProject.AutoSize = true;
            this.rb_SingleFileByProject.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_SingleFileByProject.Location = new System.Drawing.Point(3, 19);
            this.rb_SingleFileByProject.Name = "rb_SingleFileByProject";
            this.rb_SingleFileByProject.Size = new System.Drawing.Size(544, 19);
            this.rb_SingleFileByProject.TabIndex = 1;
            this.rb_SingleFileByProject.Text = "Each Project in a Single File (All Months Together)";
            this.rb_SingleFileByProject.UseVisualStyleBackColor = true;
            this.rb_SingleFileByProject.CheckedChanged += new System.EventHandler(this.rb_SingleFileByProject_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "(*) - Prettier Format";
            // 
            // lblFileNamePattern
            // 
            this.lblFileNamePattern.AutoSize = true;
            this.lblFileNamePattern.Location = new System.Drawing.Point(3, 100);
            this.lblFileNamePattern.Name = "lblFileNamePattern";
            this.lblFileNamePattern.Size = new System.Drawing.Size(201, 15);
            this.lblFileNamePattern.TabIndex = 13;
            this.lblFileNamePattern.Text = "Step 6 - Generated File Name Pattern";
            // 
            // gb_CloudSettings
            // 
            this.gb_CloudSettings.Controls.Add(this.lbl_DriveFolderName);
            this.gb_CloudSettings.Controls.Add(this.txt_DriveFolderName);
            this.gb_CloudSettings.Controls.Add(this.chkTryToUpload);
            this.gb_CloudSettings.Location = new System.Drawing.Point(3, 652);
            this.gb_CloudSettings.Name = "gb_CloudSettings";
            this.gb_CloudSettings.Size = new System.Drawing.Size(556, 97);
            this.gb_CloudSettings.TabIndex = 7;
            this.gb_CloudSettings.TabStop = false;
            this.gb_CloudSettings.Text = "Drive Settings";
            // 
            // lbl_DriveFolderName
            // 
            this.lbl_DriveFolderName.AutoSize = true;
            this.lbl_DriveFolderName.Location = new System.Drawing.Point(3, 44);
            this.lbl_DriveFolderName.Name = "lbl_DriveFolderName";
            this.lbl_DriveFolderName.Size = new System.Drawing.Size(192, 15);
            this.lbl_DriveFolderName.TabIndex = 12;
            this.lbl_DriveFolderName.Text = "Step - 6* - Drive Root Folder Name:";
            // 
            // txt_DriveFolderName
            // 
            this.txt_DriveFolderName.Enabled = false;
            this.txt_DriveFolderName.Location = new System.Drawing.Point(3, 62);
            this.txt_DriveFolderName.Name = "txt_DriveFolderName";
            this.txt_DriveFolderName.PlaceholderText = "{folderName}...";
            this.txt_DriveFolderName.Size = new System.Drawing.Size(550, 23);
            this.txt_DriveFolderName.TabIndex = 11;
            // 
            // chkTryToUpload
            // 
            this.chkTryToUpload.AutoSize = true;
            this.chkTryToUpload.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkTryToUpload.Location = new System.Drawing.Point(3, 19);
            this.chkTryToUpload.Name = "chkTryToUpload";
            this.chkTryToUpload.Size = new System.Drawing.Size(550, 19);
            this.chkTryToUpload.TabIndex = 10;
            this.chkTryToUpload.Text = "Try To Upload to Google Drive";
            this.chkTryToUpload.UseVisualStyleBackColor = true;
            this.chkTryToUpload.CheckedChanged += new System.EventHandler(this.chkTryToUpload_CheckedChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGenerate.Location = new System.Drawing.Point(0, 750);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(559, 66);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Lets the magic begin";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // bgw_RunGitCommand
            // 
            this.bgw_RunGitCommand.WorkerReportsProgress = true;
            this.bgw_RunGitCommand.WorkerSupportsCancellation = true;
            this.bgw_RunGitCommand.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_RunGitCommand_DoWork);
            this.bgw_RunGitCommand.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
            this.bgw_RunGitCommand.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunGitCommand_RunWorkerCompleted);
            // 
            // bgw_BuildOutputFile
            // 
            this.bgw_BuildOutputFile.WorkerReportsProgress = true;
            this.bgw_BuildOutputFile.WorkerSupportsCancellation = true;
            this.bgw_BuildOutputFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_BuildOutputFile_DoWork);
            this.bgw_BuildOutputFile.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
            this.bgw_BuildOutputFile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_BuildOutputFile_RunWorkerCompleted);
            // 
            // bgw_UploadToDrive
            // 
            this.bgw_UploadToDrive.WorkerReportsProgress = true;
            this.bgw_UploadToDrive.WorkerSupportsCancellation = true;
            this.bgw_UploadToDrive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_UploadToDrive_DoWork);
            this.bgw_UploadToDrive.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ProgressChanged);
            this.bgw_UploadToDrive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_UploadToDrive_RunWorkerCompleted);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 816);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.gb_CloudSettings);
            this.Controls.Add(this.gb_FileGenerationSettings);
            this.Controls.Add(this.gb_FileProjectPathSettings);
            this.Controls.Add(this.gb_ConsoleLog);
            this.Controls.Add(this.lbl_UserName);
            this.Controls.Add(this.gb_DateSettings);
            this.Controls.Add(this.txt_UserName);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.gb_DateSettings.ResumeLayout(false);
            this.gb_Step2DateSet.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gb_Step1DateSelectionType.ResumeLayout(false);
            this.gb_Step1DateSelectionType.PerformLayout();
            this.gb_ConsoleLog.ResumeLayout(false);
            this.gb_ConsoleLog.PerformLayout();
            this.gb_FileProjectPathSettings.ResumeLayout(false);
            this.gb_FileProjectPathSettings.PerformLayout();
            this.gb_FileGenerationSettings.ResumeLayout(false);
            this.gb_FileGenerationSettings.PerformLayout();
            this.gb_GroupBy.ResumeLayout(false);
            this.gb_GroupBy.PerformLayout();
            this.gb_CloudSettings.ResumeLayout(false);
            this.gb_CloudSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.GroupBox gb_DateSettings;
        private System.Windows.Forms.RadioButton rb_DateRange;
        private System.Windows.Forms.RadioButton rb_SingleMonth;
        private System.Windows.Forms.GroupBox gb_Step2DateSet;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.GroupBox gb_Step1DateSelectionType;
        private System.Windows.Forms.RadioButton rb_DateUpToNow;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label lbl_UserName;
        private System.Windows.Forms.GroupBox gb_ConsoleLog;
        private System.Windows.Forms.TextBox txt_ConsoleLog;
        private System.Windows.Forms.GroupBox gb_FileProjectPathSettings;
        private System.Windows.Forms.Label lblProjectFolder;
        private System.Windows.Forms.CheckBox chkRecursiveSearch;
        private System.Windows.Forms.TextBox txtProjectFilePath;
        private System.Windows.Forms.Button btnSearchProjectFilePath;
        private System.Windows.Forms.Button btnAddProjectFilePath;
        private System.Windows.Forms.FolderBrowserDialog fbdSearchProjectFilePath;
        private System.Windows.Forms.Label lblAddedProjectFilePaths;
        private System.Windows.Forms.ListBox lbAddedProjectFilePaths;
        private System.Windows.Forms.ProgressBar pb_Process;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.ComponentModel.BackgroundWorker bgw_AddedProjectFilePaths;
        private System.Windows.Forms.GroupBox gb_FileGenerationSettings;
        private System.Windows.Forms.GroupBox gb_GroupBy;
        private System.Windows.Forms.RadioButton rb_GroupByProjectAndMonth;
        private System.Windows.Forms.RadioButton rb_SingleFileByMonthAndYear;
        private System.Windows.Forms.RadioButton rb_SingleFileByProject;
        private System.Windows.Forms.TextBox txtFileNamePattern;
        private System.Windows.Forms.Label lblFileNamePattern;
        private System.Windows.Forms.GroupBox gb_CloudSettings;
        private System.Windows.Forms.Label lbl_DriveFolderName;
        private System.Windows.Forms.TextBox txt_DriveFolderName;
        private System.Windows.Forms.CheckBox chkTryToUpload;
        private System.Windows.Forms.Button btnGenerate;

        private System.Windows.Forms.FolderBrowserDialog fbdGenerateBeautifulFile;
        private System.ComponentModel.BackgroundWorker bgw_RunGitCommand;
        private System.ComponentModel.BackgroundWorker bgw_BuildOutputFile;
        private System.Windows.Forms.ComboBox cbo_PrettierFormat;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bgw_UploadToDrive;
    }
}