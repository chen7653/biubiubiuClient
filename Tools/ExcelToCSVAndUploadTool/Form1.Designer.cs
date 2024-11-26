namespace ExcelToCSVAndUploadTool
{
    partial class ExcelToCSVAndUploadTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_ExcelFileDir = new System.Windows.Forms.TextBox();
            this.label_ExcelFileDir = new System.Windows.Forms.Label();
            this.button_ExcelFileDir = new System.Windows.Forms.Button();
            this.button_OutputCSVFileDir = new System.Windows.Forms.Button();
            this.label_OutputCSVFileDir = new System.Windows.Forms.Label();
            this.textBox_OutputCSVFileDir = new System.Windows.Forms.TextBox();
            this.label_UploadHost = new System.Windows.Forms.Label();
            this.textBox_FTP_Host = new System.Windows.Forms.TextBox();
            this.button_Upload = new System.Windows.Forms.Button();
            this.textBox_NeedDealExcelFiles = new System.Windows.Forms.TextBox();
            this.label_NeedDealExcelFiles = new System.Windows.Forms.Label();
            this.listView_NeedDealExcelFiles = new System.Windows.Forms.ListView();
            this.groupBox_SelcetNeedDealExcelFiles = new System.Windows.Forms.GroupBox();
            this.button_ResetSelectAll = new System.Windows.Forms.Button();
            this.label_GenTP_ProgressTip = new System.Windows.Forms.Label();
            this.button_SelectAllNeedDealExcelFiles = new System.Windows.Forms.Button();
            this.button_OutputCSV = new System.Windows.Forms.Button();
            this.button_OpenCSVDir = new System.Windows.Forms.Button();
            this.label_FTP_Username = new System.Windows.Forms.Label();
            this.textBox_FTP_Username = new System.Windows.Forms.TextBox();
            this.label_FTP_Password = new System.Windows.Forms.Label();
            this.textBox_FTP_Password = new System.Windows.Forms.TextBox();
            this.label_FTP_RemotPath = new System.Windows.Forms.Label();
            this.textBox_FTP_RemotPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_FTP_Port = new System.Windows.Forms.TextBox();
            this.groupBox_SelcetNeedDealExcelFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_ExcelFileDir
            // 
            this.textBox_ExcelFileDir.Enabled = false;
            this.textBox_ExcelFileDir.Location = new System.Drawing.Point(101, 22);
            this.textBox_ExcelFileDir.Name = "textBox_ExcelFileDir";
            this.textBox_ExcelFileDir.Size = new System.Drawing.Size(606, 21);
            this.textBox_ExcelFileDir.TabIndex = 0;
            // 
            // label_ExcelFileDir
            // 
            this.label_ExcelFileDir.AutoSize = true;
            this.label_ExcelFileDir.Location = new System.Drawing.Point(12, 25);
            this.label_ExcelFileDir.Name = "label_ExcelFileDir";
            this.label_ExcelFileDir.Size = new System.Drawing.Size(83, 12);
            this.label_ExcelFileDir.TabIndex = 1;
            this.label_ExcelFileDir.Text = "Excel文件目录";
            // 
            // button_ExcelFileDir
            // 
            this.button_ExcelFileDir.Location = new System.Drawing.Point(713, 20);
            this.button_ExcelFileDir.Name = "button_ExcelFileDir";
            this.button_ExcelFileDir.Size = new System.Drawing.Size(75, 23);
            this.button_ExcelFileDir.TabIndex = 2;
            this.button_ExcelFileDir.Text = "选择目录";
            this.button_ExcelFileDir.UseVisualStyleBackColor = true;
            this.button_ExcelFileDir.Click += new System.EventHandler(this.button_ExcelFileDir_Click);
            // 
            // button_OutputCSVFileDir
            // 
            this.button_OutputCSVFileDir.Location = new System.Drawing.Point(713, 370);
            this.button_OutputCSVFileDir.Name = "button_OutputCSVFileDir";
            this.button_OutputCSVFileDir.Size = new System.Drawing.Size(75, 23);
            this.button_OutputCSVFileDir.TabIndex = 5;
            this.button_OutputCSVFileDir.Text = "选择目录";
            this.button_OutputCSVFileDir.UseVisualStyleBackColor = true;
            this.button_OutputCSVFileDir.Click += new System.EventHandler(this.button_OutputCSVFileDir_Click);
            // 
            // label_OutputCSVFileDir
            // 
            this.label_OutputCSVFileDir.AutoSize = true;
            this.label_OutputCSVFileDir.Location = new System.Drawing.Point(12, 375);
            this.label_OutputCSVFileDir.Name = "label_OutputCSVFileDir";
            this.label_OutputCSVFileDir.Size = new System.Drawing.Size(95, 12);
            this.label_OutputCSVFileDir.TabIndex = 4;
            this.label_OutputCSVFileDir.Text = "CSV文件导出目录";
            // 
            // textBox_OutputCSVFileDir
            // 
            this.textBox_OutputCSVFileDir.Enabled = false;
            this.textBox_OutputCSVFileDir.Location = new System.Drawing.Point(113, 372);
            this.textBox_OutputCSVFileDir.Name = "textBox_OutputCSVFileDir";
            this.textBox_OutputCSVFileDir.Size = new System.Drawing.Size(594, 21);
            this.textBox_OutputCSVFileDir.TabIndex = 3;
            // 
            // label_UploadHost
            // 
            this.label_UploadHost.AutoSize = true;
            this.label_UploadHost.Location = new System.Drawing.Point(12, 431);
            this.label_UploadHost.Name = "label_UploadHost";
            this.label_UploadHost.Size = new System.Drawing.Size(53, 12);
            this.label_UploadHost.TabIndex = 7;
            this.label_UploadHost.Text = "FTP_Host";
            // 
            // textBox_FTP_Host
            // 
            this.textBox_FTP_Host.Location = new System.Drawing.Point(112, 428);
            this.textBox_FTP_Host.Name = "textBox_FTP_Host";
            this.textBox_FTP_Host.Size = new System.Drawing.Size(257, 21);
            this.textBox_FTP_Host.TabIndex = 6;
            this.textBox_FTP_Host.TextChanged += new System.EventHandler(this.textBox_FTP_Host_TextChanged);
            // 
            // button_Upload
            // 
            this.button_Upload.Location = new System.Drawing.Point(713, 509);
            this.button_Upload.Name = "button_Upload";
            this.button_Upload.Size = new System.Drawing.Size(75, 23);
            this.button_Upload.TabIndex = 8;
            this.button_Upload.Text = "上传";
            this.button_Upload.UseVisualStyleBackColor = true;
            this.button_Upload.Click += new System.EventHandler(this.button_Upload_Click);
            // 
            // textBox_NeedDealExcelFiles
            // 
            this.textBox_NeedDealExcelFiles.Location = new System.Drawing.Point(12, 67);
            this.textBox_NeedDealExcelFiles.Name = "textBox_NeedDealExcelFiles";
            this.textBox_NeedDealExcelFiles.Size = new System.Drawing.Size(776, 21);
            this.textBox_NeedDealExcelFiles.TabIndex = 9;
            this.textBox_NeedDealExcelFiles.TextChanged += new System.EventHandler(this.textBox_NeedDealExcelFiles_TextChanged);
            // 
            // label_NeedDealExcelFiles
            // 
            this.label_NeedDealExcelFiles.AutoSize = true;
            this.label_NeedDealExcelFiles.Location = new System.Drawing.Point(12, 52);
            this.label_NeedDealExcelFiles.Name = "label_NeedDealExcelFiles";
            this.label_NeedDealExcelFiles.Size = new System.Drawing.Size(245, 12);
            this.label_NeedDealExcelFiles.TabIndex = 10;
            this.label_NeedDealExcelFiles.Text = "输入需要处理的Excel文件(多个文件用#分隔)";
            // 
            // listView_NeedDealExcelFiles
            // 
            this.listView_NeedDealExcelFiles.CheckBoxes = true;
            this.listView_NeedDealExcelFiles.HideSelection = false;
            this.listView_NeedDealExcelFiles.Location = new System.Drawing.Point(6, 20);
            this.listView_NeedDealExcelFiles.Name = "listView_NeedDealExcelFiles";
            this.listView_NeedDealExcelFiles.Size = new System.Drawing.Size(762, 212);
            this.listView_NeedDealExcelFiles.TabIndex = 37;
            this.listView_NeedDealExcelFiles.UseCompatibleStateImageBehavior = false;
            this.listView_NeedDealExcelFiles.View = System.Windows.Forms.View.List;
            this.listView_NeedDealExcelFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_NeedDealExcelFiles_ItemChecked);
            this.listView_NeedDealExcelFiles.SelectedIndexChanged += new System.EventHandler(this.listView_NeedDealExcelFiles_SelectedIndexChanged);
            // 
            // groupBox_SelcetNeedDealExcelFiles
            // 
            this.groupBox_SelcetNeedDealExcelFiles.Controls.Add(this.button_ResetSelectAll);
            this.groupBox_SelcetNeedDealExcelFiles.Controls.Add(this.label_GenTP_ProgressTip);
            this.groupBox_SelcetNeedDealExcelFiles.Controls.Add(this.listView_NeedDealExcelFiles);
            this.groupBox_SelcetNeedDealExcelFiles.Controls.Add(this.button_SelectAllNeedDealExcelFiles);
            this.groupBox_SelcetNeedDealExcelFiles.Location = new System.Drawing.Point(14, 94);
            this.groupBox_SelcetNeedDealExcelFiles.Name = "groupBox_SelcetNeedDealExcelFiles";
            this.groupBox_SelcetNeedDealExcelFiles.Size = new System.Drawing.Size(774, 270);
            this.groupBox_SelcetNeedDealExcelFiles.TabIndex = 43;
            this.groupBox_SelcetNeedDealExcelFiles.TabStop = false;
            this.groupBox_SelcetNeedDealExcelFiles.Text = "选择需要处理的excel文件";
            // 
            // button_ResetSelectAll
            // 
            this.button_ResetSelectAll.Location = new System.Drawing.Point(612, 238);
            this.button_ResetSelectAll.Name = "button_ResetSelectAll";
            this.button_ResetSelectAll.Size = new System.Drawing.Size(75, 23);
            this.button_ResetSelectAll.TabIndex = 45;
            this.button_ResetSelectAll.Text = "重置";
            this.button_ResetSelectAll.UseVisualStyleBackColor = true;
            this.button_ResetSelectAll.Click += new System.EventHandler(this.button_ResetSelectAll_Click);
            // 
            // label_GenTP_ProgressTip
            // 
            this.label_GenTP_ProgressTip.AutoSize = true;
            this.label_GenTP_ProgressTip.Location = new System.Drawing.Point(6, 287);
            this.label_GenTP_ProgressTip.Name = "label_GenTP_ProgressTip";
            this.label_GenTP_ProgressTip.Size = new System.Drawing.Size(0, 12);
            this.label_GenTP_ProgressTip.TabIndex = 44;
            // 
            // button_SelectAllNeedDealExcelFiles
            // 
            this.button_SelectAllNeedDealExcelFiles.Location = new System.Drawing.Point(693, 238);
            this.button_SelectAllNeedDealExcelFiles.Name = "button_SelectAllNeedDealExcelFiles";
            this.button_SelectAllNeedDealExcelFiles.Size = new System.Drawing.Size(75, 23);
            this.button_SelectAllNeedDealExcelFiles.TabIndex = 38;
            this.button_SelectAllNeedDealExcelFiles.Text = "全选";
            this.button_SelectAllNeedDealExcelFiles.UseVisualStyleBackColor = true;
            this.button_SelectAllNeedDealExcelFiles.Click += new System.EventHandler(this.button_SelectAllNeedDealExcelFiles_Click);
            // 
            // button_OutputCSV
            // 
            this.button_OutputCSV.Location = new System.Drawing.Point(713, 399);
            this.button_OutputCSV.Name = "button_OutputCSV";
            this.button_OutputCSV.Size = new System.Drawing.Size(75, 23);
            this.button_OutputCSV.TabIndex = 44;
            this.button_OutputCSV.Text = "导出csv";
            this.button_OutputCSV.UseVisualStyleBackColor = true;
            this.button_OutputCSV.Click += new System.EventHandler(this.button_OutputCSV_Click);
            // 
            // button_OpenCSVDir
            // 
            this.button_OpenCSVDir.Location = new System.Drawing.Point(632, 399);
            this.button_OpenCSVDir.Name = "button_OpenCSVDir";
            this.button_OpenCSVDir.Size = new System.Drawing.Size(75, 23);
            this.button_OpenCSVDir.TabIndex = 45;
            this.button_OpenCSVDir.Text = "打开目录";
            this.button_OpenCSVDir.UseVisualStyleBackColor = true;
            this.button_OpenCSVDir.Click += new System.EventHandler(this.button_OpenCSVDir_Click);
            // 
            // label_FTP_Username
            // 
            this.label_FTP_Username.AutoSize = true;
            this.label_FTP_Username.Location = new System.Drawing.Point(12, 458);
            this.label_FTP_Username.Name = "label_FTP_Username";
            this.label_FTP_Username.Size = new System.Drawing.Size(77, 12);
            this.label_FTP_Username.TabIndex = 47;
            this.label_FTP_Username.Text = "FTP_Username";
            // 
            // textBox_FTP_Username
            // 
            this.textBox_FTP_Username.Location = new System.Drawing.Point(113, 455);
            this.textBox_FTP_Username.Name = "textBox_FTP_Username";
            this.textBox_FTP_Username.Size = new System.Drawing.Size(594, 21);
            this.textBox_FTP_Username.TabIndex = 46;
            this.textBox_FTP_Username.TextChanged += new System.EventHandler(this.textBox_FTP_Username_TextChanged);
            // 
            // label_FTP_Password
            // 
            this.label_FTP_Password.AutoSize = true;
            this.label_FTP_Password.Location = new System.Drawing.Point(12, 485);
            this.label_FTP_Password.Name = "label_FTP_Password";
            this.label_FTP_Password.Size = new System.Drawing.Size(77, 12);
            this.label_FTP_Password.TabIndex = 48;
            this.label_FTP_Password.Text = "FTP_Password";
            // 
            // textBox_FTP_Password
            // 
            this.textBox_FTP_Password.Location = new System.Drawing.Point(113, 482);
            this.textBox_FTP_Password.Name = "textBox_FTP_Password";
            this.textBox_FTP_Password.PasswordChar = '*';
            this.textBox_FTP_Password.Size = new System.Drawing.Size(594, 21);
            this.textBox_FTP_Password.TabIndex = 49;
            this.textBox_FTP_Password.TextChanged += new System.EventHandler(this.textBox_FTP_Password_TextChanged);
            // 
            // label_FTP_RemotPath
            // 
            this.label_FTP_RemotPath.AutoSize = true;
            this.label_FTP_RemotPath.Location = new System.Drawing.Point(12, 514);
            this.label_FTP_RemotPath.Name = "label_FTP_RemotPath";
            this.label_FTP_RemotPath.Size = new System.Drawing.Size(83, 12);
            this.label_FTP_RemotPath.TabIndex = 50;
            this.label_FTP_RemotPath.Text = "FTP_RemotPath";
            // 
            // textBox_FTP_RemotPath
            // 
            this.textBox_FTP_RemotPath.Location = new System.Drawing.Point(113, 511);
            this.textBox_FTP_RemotPath.Name = "textBox_FTP_RemotPath";
            this.textBox_FTP_RemotPath.Size = new System.Drawing.Size(594, 21);
            this.textBox_FTP_RemotPath.TabIndex = 51;
            this.textBox_FTP_RemotPath.TextChanged += new System.EventHandler(this.textBox_FTP_RemotPath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(391, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 53;
            this.label1.Text = "FTP_Port";
            // 
            // textBox_FTP_Port
            // 
            this.textBox_FTP_Port.Location = new System.Drawing.Point(450, 428);
            this.textBox_FTP_Port.Name = "textBox_FTP_Port";
            this.textBox_FTP_Port.Size = new System.Drawing.Size(257, 21);
            this.textBox_FTP_Port.TabIndex = 52;
            this.textBox_FTP_Port.TextChanged += new System.EventHandler(this.textBox_FTP_Port_TextChanged);
            // 
            // ExcelToCSVAndUploadTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_FTP_Port);
            this.Controls.Add(this.textBox_FTP_RemotPath);
            this.Controls.Add(this.label_FTP_RemotPath);
            this.Controls.Add(this.textBox_FTP_Password);
            this.Controls.Add(this.label_FTP_Password);
            this.Controls.Add(this.label_FTP_Username);
            this.Controls.Add(this.textBox_FTP_Username);
            this.Controls.Add(this.button_OpenCSVDir);
            this.Controls.Add(this.button_OutputCSV);
            this.Controls.Add(this.groupBox_SelcetNeedDealExcelFiles);
            this.Controls.Add(this.label_NeedDealExcelFiles);
            this.Controls.Add(this.textBox_NeedDealExcelFiles);
            this.Controls.Add(this.button_Upload);
            this.Controls.Add(this.label_UploadHost);
            this.Controls.Add(this.textBox_FTP_Host);
            this.Controls.Add(this.button_OutputCSVFileDir);
            this.Controls.Add(this.label_OutputCSVFileDir);
            this.Controls.Add(this.textBox_OutputCSVFileDir);
            this.Controls.Add(this.button_ExcelFileDir);
            this.Controls.Add(this.label_ExcelFileDir);
            this.Controls.Add(this.textBox_ExcelFileDir);
            this.Name = "ExcelToCSVAndUploadTool";
            this.Text = "ExcelToCSVAndUploadTool";
            this.groupBox_SelcetNeedDealExcelFiles.ResumeLayout(false);
            this.groupBox_SelcetNeedDealExcelFiles.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ExcelFileDir;
        private System.Windows.Forms.Label label_ExcelFileDir;
        private System.Windows.Forms.Button button_ExcelFileDir;
        private System.Windows.Forms.Button button_OutputCSVFileDir;
        private System.Windows.Forms.Label label_OutputCSVFileDir;
        private System.Windows.Forms.TextBox textBox_OutputCSVFileDir;
        private System.Windows.Forms.Label label_UploadHost;
        private System.Windows.Forms.TextBox textBox_FTP_Host;
        private System.Windows.Forms.Button button_Upload;
        private System.Windows.Forms.TextBox textBox_NeedDealExcelFiles;
        private System.Windows.Forms.Label label_NeedDealExcelFiles;
        private System.Windows.Forms.ListView listView_NeedDealExcelFiles;
        private System.Windows.Forms.GroupBox groupBox_SelcetNeedDealExcelFiles;
        private System.Windows.Forms.Label label_GenTP_ProgressTip;
        private System.Windows.Forms.Button button_SelectAllNeedDealExcelFiles;
        private System.Windows.Forms.Button button_ResetSelectAll;
        private System.Windows.Forms.Button button_OutputCSV;
        private System.Windows.Forms.Button button_OpenCSVDir;
        private System.Windows.Forms.Label label_FTP_Username;
        private System.Windows.Forms.TextBox textBox_FTP_Username;
        private System.Windows.Forms.Label label_FTP_Password;
        private System.Windows.Forms.TextBox textBox_FTP_Password;
        private System.Windows.Forms.Label label_FTP_RemotPath;
        private System.Windows.Forms.TextBox textBox_FTP_RemotPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_FTP_Port;
    }
}

