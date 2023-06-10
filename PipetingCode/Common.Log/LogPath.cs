using System;
using System.IO;

namespace Common.Log
{
    // Token: 0x02000007 RID: 7
    public class LogPath
    {
        // Token: 0x06000022 RID: 34 RVA: 0x000021BD File Offset: 0x000003BD
        public LogPath(string appName)
        {
            this._appName = appName;
        }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000023 RID: 35 RVA: 0x000021D9 File Offset: 0x000003D9
        public string InfoPath
        {
            get
            {
                return this.GetLogFilePath(string.Format("info_{0:yyyy_MM_dd}.txt", DateTime.Now));
            }
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000024 RID: 36 RVA: 0x000021F5 File Offset: 0x000003F5
        public string ErrorPath
        {
            get
            {
                return this.GetLogFilePath(string.Format("error_{0:yyyy_MM_dd}.txt", DateTime.Now));
            }
        }

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x06000025 RID: 37 RVA: 0x00002211 File Offset: 0x00000411
        public string MessagePath
        {
            get
            {
                return this.GetLogFilePath(string.Format("message_{0:yyyy_MM_dd}.txt", DateTime.Now));
            }
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00002714 File Offset: 0x00000914
        private string GetLogFilePath(string fileName)
        {
            string logFolder = this.GetLogFolder();
            string text = Path.Combine(logFolder, fileName);
            try
            {
                bool flag = !File.Exists(text);
                if (flag)
                {
                    FileStream fileStream = File.Create(text);
                    fileStream.Dispose();
                }
            }
            catch (Exception)
            {
            }
            return text;
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00002770 File Offset: 0x00000970
        public string GetLogFolder()
        {
            bool flag = !string.IsNullOrWhiteSpace(this._logFolder);
            string logFolder;
            if (flag)
            {
                logFolder = this._logFolder;
            }
            else
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string text = Path.Combine(folderPath, this._appName, "Log");
                this.CreateFolder(text);
                bool flag2 = Directory.Exists(text);
                if (flag2)
                {
                    this._logFolder = text;
                }
                logFolder = this._logFolder;
            }
            return logFolder;
        }

        // Token: 0x06000028 RID: 40 RVA: 0x000027DC File Offset: 0x000009DC
        public void CreateFolder(string folder)
        {
            bool flag = !Directory.Exists(folder);
            if (flag)
            {
                Directory.CreateDirectory(folder);
            }
        }

        // Token: 0x0400000E RID: 14
        private readonly string _appName;

        // Token: 0x0400000F RID: 15
        private string _logFolder = string.Empty;
    }
}