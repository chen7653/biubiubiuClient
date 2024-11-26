using OfficeOpenXml;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelToCSVAndUploadTool
{
    public class Tool
    {
        private static Tool mInstance;
        public static Tool Instance
        {
            get
            {
                if (mInstance == null) mInstance = new Tool();
                return mInstance;
            }
        }

        /// <summary>
        /// 是否显示MessageBox
        /// 命令行下调用不需要显示messagebox
        /// </summary>
        public static bool IsShowMessageBox = true;

        /// <summary>
        /// 工具缓存记录
        /// </summary>
        private static Cache mCache;
        /// <summary>
        /// 工具缓存记录
        /// </summary>
        public static Cache Cache
        {
            get
            {
                if (mCache == null) mCache = new Cache();
                return mCache;
            }
        }

        /// <summary>
        /// excel目录下所有的文件
        /// </summary>
        public List<string> AllExcelFiles = new List<string>();


        /// <summary>
        /// 记录需要处理的excel文件
        /// </summary>
        public void RecordNeedDealExcelFiles(string inputExcelFilesName)
        {
            //用逗号分隔
            if (!string.IsNullOrEmpty(inputExcelFilesName) && inputExcelFilesName.Contains('#'))
            {
                Tool.Cache.CacheInfo.NeedDealExcelFiles.Clear();

                string[] splitStrs = inputExcelFilesName.Split('#');

                Tool.Cache.CacheInfo.NeedDealExcelFiles.AddRange(splitStrs);

                Tool.Cache.SaveCacheInfo();
            }
        }

        /// <summary>
        /// 读取Excel目录下的所有文件
        /// </summary>
        public void LoadAllExcelFiles()
        {
            var tableDir = Tool.Cache.CacheInfo.ExcelFileDir;
            if (!Directory.Exists(tableDir))
            {
                MessageBox.Show($"错误：{tableDir} 表格目录不存在", "", MessageBoxButtons.OK);
                return;
            }

            DirectoryInfo _dir = new DirectoryInfo(tableDir);
            var _totalFile = _dir.GetFiles("*.xlsx");
            for (int i = 0; i < _totalFile.Length; i++)
            {
                AllExcelFiles.Add(_totalFile[i].FullName);
            }
        }

        /// <summary>
        /// 把excel文件转换成csv
        /// </summary>
        public void ExcetToCSV()
        {
            if (string.IsNullOrEmpty(Tool.Cache.CacheInfo.OutputCSVFileDir))
            {
                return;
            }

            if (!Directory.Exists(Tool.Cache.CacheInfo.OutputCSVFileDir))
            {
                Directory.CreateDirectory(Tool.Cache.CacheInfo.OutputCSVFileDir);
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string loadCSVConfig = "";

            Tool.Cache.CacheInfo.NeedUploadFiles.Clear();

            for (int i = 0, count = Tool.Cache.CacheInfo.NeedDealExcelFiles.Count; i < count; i++)
            {
                var fileName = Tool.Cache.CacheInfo.NeedDealExcelFiles[i];

                var filePath = $"{Tool.Cache.CacheInfo.ExcelFileDir}/{fileName}.xlsx";

                if (!File.Exists(filePath))
                {
                    continue;
                }

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    // 获取第一个工作表
                    var worksheet = package.Workbook.Worksheets[0];

                    var csvFilePath = $"{Tool.Cache.CacheInfo.OutputCSVFileDir}/{Path.GetFileNameWithoutExtension(fileName)}.csv";
                    Tool.Cache.CacheInfo.NeedUploadFiles.Add(csvFilePath);

                    if (File.Exists(csvFilePath))
                    {
                        File.Delete(csvFilePath);
                    }

                    // 创建CSV文件
                    using (StreamWriter writer = new StreamWriter(csvFilePath))
                    {
                        // 循环遍历工作表的行
                        for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                        {
                            // 循环遍历行中的列
                            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                            {
                                // 在CSV文件中写入单元格的值，英文逗号换成中文逗号
                                var value = worksheet.Cells[row, col].Value;
                                var str = (value == null) ? "" : value.ToString().Replace(',', '，').Replace("\r\n", "\n");
                                if (str.Contains("\n"))
                                {
                                    str = $"\"{str}\"";
                                }
                                //添加逗号分隔符
                                str = str + ",";
                                writer.Write(str);
                            }

                            // 在行的最后添加换行符
                            writer.Write("\r\n");
                        }
                    }

                    loadCSVConfig += $"{fileName}{(i < count - 1 ? "\n" : "")}";
                }
            }

            string configFilePath = $"{Tool.Cache.CacheInfo.OutputCSVFileDir}/{Tool.Cache.CacheInfo.UploadCSVConfigFile}";
            if (File.Exists(configFilePath))
            {
                File.Delete(configFilePath);
            }
            File.WriteAllText(configFilePath, loadCSVConfig);

            Tool.Cache.CacheInfo.NeedUploadFiles.Add(configFilePath);

            Tool.Cache.SaveCacheInfo();

            MessageBox.Show($"导出完成", "", MessageBoxButtons.OK);
        }

        /// <summary>
        /// 上传csv文件到服务器
        /// </summary>
        public void UploadCSV()
        {
            // FTP服务器的相关信息
            string sftpHost = Tool.Cache.CacheInfo.FTP_Host;
            int sftpPort = Tool.Cache.CacheInfo.FTP_Port;
            string sftpUsername = Tool.Cache.CacheInfo.FTP_Username;
            string sftpPassword = Tool.Cache.CacheInfo.FTP_Password;
            string remotePath = Tool.Cache.CacheInfo.FTP_RemotePath;

            // 创建一个SFTP客户端对象
            using (var client = (sftpPort == 0)
                                ?
                                new SftpClient(sftpHost, sftpUsername, sftpPassword)
                                :
                                new SftpClient(sftpHost, sftpPort, sftpUsername, sftpPassword))
            {
                // 连接到SFTP服务器
                client.Connect();

                for (int i = 0, count = Tool.Cache.CacheInfo.NeedUploadFiles.Count; i < count; i++)
                {
                    var uploadFilePath = Tool.Cache.CacheInfo.NeedUploadFiles[i];

                    if (!File.Exists(uploadFilePath))
                    {
                        continue;
                    }

                    // 打开本地文件
                    using (var fileStream = File.OpenRead(uploadFilePath))
                    {
                        var remoteFilePath = $"{remotePath}/{Path.GetFileName(uploadFilePath)}";

                        // 上传文件到SFTP服务器
                        client.UploadFile(fileStream, remoteFilePath, true);
                    }
                }

                // 关闭SFTP连接
                client.Disconnect();
            }

            MessageBox.Show($"上传完成", "", MessageBoxButtons.OK);
        }
    }
}
