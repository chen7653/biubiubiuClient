using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelToCSVAndUploadTool
{
    public partial class ExcelToCSVAndUploadTool : Form
    {
        public ExcelToCSVAndUploadTool()
        {
            InitializeComponent();

            InitCacheData();
        }

        private void InitCacheData()
        {
            textBox_ExcelFileDir.Text = Tool.Cache.CacheInfo.ExcelFileDir;
            textBox_OutputCSVFileDir.Text = Tool.Cache.CacheInfo.OutputCSVFileDir;

            string needDealExcelFilesStr = "";
            for (int i = 0, count = Tool.Cache.CacheInfo.NeedDealExcelFiles.Count; i < count; i++)
            {
                needDealExcelFilesStr += $"{Tool.Cache.CacheInfo.NeedDealExcelFiles[i]}{(i < count - 1 ? "#" : "")}";
            }
            textBox_NeedDealExcelFiles.Text = needDealExcelFilesStr;

            textBox_FTP_Host.Text = Tool.Cache.CacheInfo.FTP_Host;
            textBox_FTP_Port.Text = Tool.Cache.CacheInfo.FTP_Port.ToString();
            textBox_FTP_Username.Text = Tool.Cache.CacheInfo.FTP_Username;
            textBox_FTP_Password.Text = Tool.Cache.CacheInfo.FTP_Password;
            textBox_FTP_RemotPath.Text = Tool.Cache.CacheInfo.FTP_RemotePath;

            WaitInited();
        }

        private async void WaitInited()
        {
            await Task.Delay(1000);

            //选择完目录后读取所有表格
            Tool.Instance.LoadAllExcelFiles();
            ShowAllExcelFilesInListView();
            RefreshSelectedExcelFilesInListView();
        }

        #region Utility
        /// <summary>
        /// 文件夹目录选择框
        /// </summary>
        /// <param name="title">提示框标题</param>
        /// <param name="prePath">文件夹路径（预设定的路径）</param>
        /// <param name="folderDirPath">返回选择的文件夹目录</param>
        private void SelectFolderDirDialog(string title, string prePath, ref string folderDirPath)
        {
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("What are you doing now ???");
                return;
            }

            using (FolderBrowserDialog folderDlg = new FolderBrowserDialog())
            {
                folderDlg.Description = title;
                folderDlg.SelectedPath = prePath;

                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    folderDirPath = folderDlg.SelectedPath.Replace("\\", "/");
                }
            }
        }

        /// <summary>
        /// 文件选择框
        /// </summary>
        /// <param name="title">提示框标题</param>
        /// <param name="filter">筛选的文件类型 ： "json file (*.json)|*.json" </param>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">返回所选文件的路径</param>
        private void SelectFileDialog(string title, string filter, string fileName, ref string filePath, string fileDir = "")
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(filter))
            {
                MessageBox.Show("What are you doing now ???");
                return;
            }

            using (OpenFileDialog fileDlg = new OpenFileDialog())
            {
                fileDlg.InitialDirectory = (string.IsNullOrEmpty(fileDir) ? Application.StartupPath : fileDir) + "\\" + fileName;
                fileDlg.Multiselect = false;
                fileDlg.Title = title;
                fileDlg.Filter = filter;

                if (fileDlg.ShowDialog() == DialogResult.OK)
                {
                    filePath = fileDlg.FileName;
                }
            }
        }

        void ShowProgress(ProgressBar proBar, int minimum, int maximum, int value, Label lableTip = null, string tipContent = "")
        {
            if (proBar == null) return;

            if (proBar.InvokeRequired)
            {
                Action<int> min = (x) =>
                {
                    proBar.Minimum = x;
                };
                proBar.Invoke(min, minimum);
                Action<int> max = (x) =>
                {
                    proBar.Maximum = x;
                };
                proBar.Invoke(max, maximum);
                Action<int> val = (x) =>
                {
                    proBar.Value = x;
                };
                proBar.Invoke(val, value);
            }
            else
            {
                proBar.Minimum = minimum;
                proBar.Maximum = maximum;
                proBar.Value = value;
            }

            if (lableTip != null && !string.IsNullOrEmpty(tipContent))
            {
                lableTip.Text = tipContent;
            }

            proBar.Refresh();
        }
        #endregion

        private void button_ExcelFileDir_Click(object sender, EventArgs e)
        {
            SelectFolderDirDialog("请选择Excel文件目录", textBox_ExcelFileDir.Text, ref Tool.Cache.CacheInfo.ExcelFileDir);
            textBox_ExcelFileDir.Text = Tool.Cache.CacheInfo.ExcelFileDir;
            Tool.Cache.SaveCacheInfo();

            //选择完目录后读取所有表格
            Tool.Instance.LoadAllExcelFiles();
            ShowAllExcelFilesInListView();
            RefreshSelectedExcelFilesInListView();
        }

        private void button_OutputCSVFileDir_Click(object sender, EventArgs e)
        {
            SelectFolderDirDialog("请选择CSV文件导出目录", textBox_OutputCSVFileDir.Text, ref Tool.Cache.CacheInfo.OutputCSVFileDir);
            textBox_OutputCSVFileDir.Text = Tool.Cache.CacheInfo.OutputCSVFileDir;
            Tool.Cache.SaveCacheInfo();
        }

        private void textBox_NeedDealExcelFiles_TextChanged(object sender, EventArgs e)
        {
            Tool.Instance.RecordNeedDealExcelFiles(textBox_NeedDealExcelFiles.Text);
            RefreshSelectedExcelFilesInListView();
        }

        private void button_SelectAllNeedDealExcelFiles_Click(object sender, EventArgs e)
        {
            Tool.Cache.CacheInfo.NeedDealExcelFiles.Clear();

            isRefresheAllExcelFilesListView = true;
            var items = listView_NeedDealExcelFiles.Items;
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Checked = true;
                Tool.Cache.CacheInfo.NeedDealExcelFiles.Add(items[i].Text);
            }
            isRefresheAllExcelFilesListView = false;

            string needDealExcelFilesStr = "";
            for (int i = 0, count = Tool.Cache.CacheInfo.NeedDealExcelFiles.Count; i < count; i++)
            {
                needDealExcelFilesStr += $"{Tool.Cache.CacheInfo.NeedDealExcelFiles[i]}{(i < count - 1 ? "#" : "")}";
            }

            textBox_NeedDealExcelFiles.Text = needDealExcelFilesStr;

            Tool.Cache.SaveCacheInfo();
        }

        private void button_ResetSelectAll_Click(object sender, EventArgs e)
        {
            Tool.Cache.CacheInfo.NeedDealExcelFiles.Clear();

            isRefresheAllExcelFilesListView = true;
            var items = listView_NeedDealExcelFiles.Items;
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Checked = false;
            }
            isRefresheAllExcelFilesListView = false;

            textBox_NeedDealExcelFiles.Text = "";

            Tool.Cache.SaveCacheInfo();
        }

        private void ShowAllExcelFilesInListView()
        {
            isRefresheAllExcelFilesListView = true;
            listView_NeedDealExcelFiles.Items.Clear();
            var fileList = Tool.Instance.AllExcelFiles;
            for (int i = 0; i < fileList.Count; i++)
            {
                ListViewItem item = new ListViewItem(Path.GetFileNameWithoutExtension(fileList[i]), 0);
                listView_NeedDealExcelFiles.Items.Add(item);
            }
            isRefresheAllExcelFilesListView = false;
        }

        private void RefreshSelectedExcelFilesInListView()
        {
            isRefresheAllExcelFilesListView = true;
            var items = listView_NeedDealExcelFiles.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Checked)
                {
                    items[i].Checked = false;
                }

                var fileName = items[i].Text;

                if (Tool.Cache.CacheInfo.NeedDealExcelFiles.Contains(fileName))
                {
                    items[i].Checked = true;
                }
            }
            isRefresheAllExcelFilesListView = false;
        }

        /// <summary>
        /// 是否正在刷新真个excel列表
        /// </summary>
        bool isRefresheAllExcelFilesListView = false;
        private void listView_NeedDealExcelFiles_ItemChecked(object sender, EventArgs e)
        {
            if (isRefresheAllExcelFilesListView)
            {
                return;
            }

            List<string> selectItems = new List<string>();
            var items = listView_NeedDealExcelFiles.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Checked)
                {
                    var fileName = items[i].Text;
                    selectItems.Add(fileName);
                }
            }

            Tool.Cache.CacheInfo.NeedDealExcelFiles.Clear();
            for (int i = 0; i < selectItems.Count; i++)
            {
                var fileName = selectItems[i];
                Tool.Cache.CacheInfo.NeedDealExcelFiles.Add(fileName);
            }

            string needDealExcelFilesStr = "";
            for (int i = 0, count = Tool.Cache.CacheInfo.NeedDealExcelFiles.Count; i < count; i++)
            {
                needDealExcelFilesStr += $"{Tool.Cache.CacheInfo.NeedDealExcelFiles[i]}{(i < count - 1 ? "#" : "")}";
            }

            textBox_NeedDealExcelFiles.Text = needDealExcelFilesStr;

            Tool.Cache.SaveCacheInfo();
        }

        private void listView_NeedDealExcelFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listview = sender as ListView;
            if (listview != null && listview.SelectedItems != null)
            {
                isRefresheAllExcelFilesListView = true;
                List<string> selectItems = new List<string>();
                for (int i = 0; i < listview.SelectedItems.Count; i++)
                {
                    if (!listview.SelectedItems[i].Checked)
                    {
                        listview.SelectedItems[i].Checked = true;
                        selectItems.Add(listview.SelectedItems[i].Text);
                    }
                }

                for (int i = 0; i < selectItems.Count; i++)
                {
                    var fileName = selectItems[i];
                    if (!Tool.Cache.CacheInfo.NeedDealExcelFiles.Contains(fileName))
                    {
                        Tool.Cache.CacheInfo.NeedDealExcelFiles.Add(fileName);
                    }
                }

                string needDealExcelFilesStr = "";
                for (int i = 0, count = Tool.Cache.CacheInfo.NeedDealExcelFiles.Count; i < count; i++)
                {
                    needDealExcelFilesStr += $"{Tool.Cache.CacheInfo.NeedDealExcelFiles[i]}{(i < count - 1 ? "#" : "")}";
                }

                textBox_NeedDealExcelFiles.Text = needDealExcelFilesStr;

                Tool.Cache.SaveCacheInfo();
                isRefresheAllExcelFilesListView = false;
            }
        }

        private void button_OutputCSV_Click(object sender, EventArgs e)
        {
            Tool.Instance.ExcetToCSV();
        }

        private void button_OpenCSVDir_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Tool.Cache.CacheInfo.OutputCSVFileDir))
            {
                Directory.CreateDirectory(Tool.Cache.CacheInfo.OutputCSVFileDir);
            }

            Process.Start(Tool.Cache.CacheInfo.OutputCSVFileDir);
        }

        #region FTP
        private void button_Upload_Click(object sender, EventArgs e)
        {
            Tool.Instance.UploadCSV();
        }

        private void textBox_FTP_Host_TextChanged(object sender, EventArgs e)
        {
            if (textBox_FTP_Host.Text != Tool.Cache.CacheInfo.FTP_Host)
            {
                Tool.Cache.CacheInfo.FTP_Host = textBox_FTP_Host.Text;
                Tool.Cache.SaveCacheInfo();
            }
        }

        private void textBox_FTP_Port_TextChanged(object sender, EventArgs e)
        {
            if (textBox_FTP_Port.Text != Tool.Cache.CacheInfo.FTP_Port.ToString())
            {
                Tool.Cache.CacheInfo.FTP_Port = int.TryParse(textBox_FTP_Port.Text, out int port) ? port : 0;
                Tool.Cache.SaveCacheInfo();
            }
        }

        private void textBox_FTP_Username_TextChanged(object sender, EventArgs e)
        {
            if (textBox_FTP_Username.Text != Tool.Cache.CacheInfo.FTP_Username)
            {
                Tool.Cache.CacheInfo.FTP_Username = textBox_FTP_Username.Text;
                Tool.Cache.SaveCacheInfo();
            }
        }

        private void textBox_FTP_Password_TextChanged(object sender, EventArgs e)
        {
            if (textBox_FTP_Password.Text != Tool.Cache.CacheInfo.FTP_Password)
            {
                Tool.Cache.CacheInfo.FTP_Password = textBox_FTP_Password.Text;
                Tool.Cache.SaveCacheInfo();
            }
        }

        private void textBox_FTP_RemotPath_TextChanged(object sender, EventArgs e)
        {
            if (textBox_FTP_RemotPath.Text != Tool.Cache.CacheInfo.FTP_RemotePath)
            {
                Tool.Cache.CacheInfo.FTP_RemotePath = textBox_FTP_RemotPath.Text;
                Tool.Cache.SaveCacheInfo();
            }
        }
        #endregion
    }
}
