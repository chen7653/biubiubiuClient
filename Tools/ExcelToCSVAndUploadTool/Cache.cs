using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCSVAndUploadTool
{
    /// <summary>工具缓存记录信息</summary>
    [Serializable]
    public class CacheInfo
    {
        /// <summary>
        /// Excel文件目录
        /// </summary>
        public string ExcelFileDir = "";

        /// <summary>
        /// 需要处理的Excel文件列表
        /// </summary>        
        private List<string> mNeedDealExcelFiles;
        /// <summary>
        /// 需要处理的Excel文件列表
        /// </summary>   
        public List<string> NeedDealExcelFiles
        {
            get
            {
                if (mNeedDealExcelFiles == null)
                {
                    mNeedDealExcelFiles = new List<string>();
                }
                return mNeedDealExcelFiles;
            }
        }

        /// <summary>
        /// 导出的CSV文件目录
        /// </summary>
        public string OutputCSVFileDir = "";

        /// <summary>
        /// 需要上传的CSV文件列表
        /// </summary>
        private List<string> mNeedUploadFiles;
        /// <summary>
        /// 需要上传的CSV文件列表
        /// </summary>
        public List<string> NeedUploadFiles
        {
            get
            {
                if (mNeedUploadFiles == null)
                {
                    mNeedUploadFiles = new List<string>();
                }
                return mNeedUploadFiles;
            }
        }

        /// <summary>
        /// 上传到资源服务器的csv配置文件
        /// </summary>
        public string UploadCSVConfigFile = "LoadCSVConfigFile.txt";

        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl
        {
            get
            {
                return @"http://120.46.158.70:8081/download/WenZi2/Android/CSV";
            }
        }

        public string FTP_Host = "120.46.158.70";
        public int FTP_Port = 22;
        public string FTP_Username = "root";
        public string FTP_Password = "EgekU5pQfGAD";
        public string FTP_RemotePath = "/data/game/SiShenApks/WenZi2/Android/CSV";
    }

    public class Cache
    {
        public const string LOCAL_CACHE_FILE_Name = "LocalCache.json";

        /// <summary>工具 缓存记录文件 Cache.json</summary>
        private string toolsCacheFilePath;

        /// <summary>缓存记录</summary>
        private CacheInfo mCacheInfo;
        public CacheInfo CacheInfo
        {
            get
            {
                return mCacheInfo;
            }
        }

        public Cache()
        {
            toolsCacheFilePath = Environment.CurrentDirectory + "\\" + LOCAL_CACHE_FILE_Name;
            Init();
        }

        private void Init()
        {
            LoadCacheInfo();
        }

        private void RestCacheInfo()
        {
            if (mCacheInfo == null) mCacheInfo = new CacheInfo();

            SaveCacheInfo();
        }

        /// <summary>加载缓存记录</summary>
        private void LoadCacheInfo()
        {
            if (string.IsNullOrEmpty(toolsCacheFilePath) || !File.Exists(toolsCacheFilePath))//如果该地址为空或者 不存在则重置数据
            {
                RestCacheInfo();
                return;
            }

            using (FileStream stream = new FileStream(toolsCacheFilePath, FileMode.Open))//读取
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                string strData = Encoding.UTF8.GetString(bytes);
                PraseJsonCacheInfo(strData);
            }
        }

        /// <summary>解析缓存记录的json数据</summary>
        private void PraseJsonCacheInfo(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
            {
                RestCacheInfo();
                return;
            }

            try
            {
                mCacheInfo = JsonConvert.DeserializeObject<CacheInfo>(jsonStr);
            }
            catch (Exception)
            {
                RestCacheInfo();
                return;
            }
        }


        public void SaveCacheInfo()
        {
            //如果路径存在,删除 重建
            if (File.Exists(toolsCacheFilePath))
            {
                File.Delete(toolsCacheFilePath);
            }

            //将重置的数据保存为json格式
            string jsonStr = JsonConvert.SerializeObject(mCacheInfo, Formatting.Indented);
            using (FileStream stream = new FileStream(toolsCacheFilePath, FileMode.Create))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(jsonStr);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
            }
        }
    }
}
