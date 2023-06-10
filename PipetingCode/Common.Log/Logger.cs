using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Common.Log
{
    // Token: 0x02000005 RID: 5
    public class Logger : ILogger
    {
        // Token: 0x0600001A RID: 26 RVA: 0x0000219A File Offset: 0x0000039A
        public Logger(string appName)
        {
            this._logPath = new LogPath(appName);
        }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x0600001B RID: 27 RVA: 0x000021B0 File Offset: 0x000003B0
        public string LogFolder
        {
            get
            {
                return this._logPath.GetLogFolder();
            }
        }

        // Token: 0x0600001C RID: 28 RVA: 0x00002380 File Offset: 0x00000580
        public void Info(string info)
        {
            try
            {
                List<string> contents = new List<string>
                {
                    string.Format("记录时间：{0:yyyy-MM-dd hh:mm:ss} {1}", DateTime.Now, info)
                };
                File.AppendAllLines(this._logPath.InfoPath, contents);
            }
            catch (Exception ex)
            {
                try
                {
                    File.AppendAllLines(this._logPath.InfoPath, new List<string>() { ex.Message.ToString() });
                }
                catch
                {
                }
            }
        }

        // Token: 0x0600001D RID: 29 RVA: 0x000023DC File Offset: 0x000005DC
        public void Message(string message)
        {
            try
            {
                List<string> list = new List<string>
                {
                    "***************************************************************\r\n",
                    string.Format("记录时间：{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now),
                    "描述：" + message
                };
                List<string> list2 = list;
                int index = list.Count - 1;
                list2[index] += "\r\n";
                File.AppendAllLines(this._logPath.MessagePath, list);
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00002478 File Offset: 0x00000678
        public void Error(string error)
        {
            try
            {
                List<string> list = new List<string>
                {
                    "***************************************************************\r\n",
                    string.Format("记录时间：{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now),
                    "线程ID  ：" + this.GetCurrentThreadId(),
                    "错误描述：" + error
                };
                List<string> list2 = list;
                int index = list.Count - 1;
                list2[index] += "\r\n";
                File.AppendAllLines(this._logPath.ErrorPath, list);
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x0600001F RID: 31 RVA: 0x0000252C File Offset: 0x0000072C
        public void Error(string error, Exception ex)
        {
            try
            {
                List<string> list = new List<string>
                {
                    "***************************************************************\r\n",
                    string.Format("记录时间：{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now),
                    "线程ID  ：" + this.GetCurrentThreadId(),
                    "错误描述：" + error + " " + ex.Message
                };
                bool flag = ex.StackTrace != null;
                if (flag)
                {
                    list.Add("异常堆栈：" + ex.StackTrace);
                }
                List<string> list2 = list;
                int index = list.Count - 1;
                list2[index] += "\r\n";
                File.AppendAllLines(this._logPath.ErrorPath, list);
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00002610 File Offset: 0x00000810
        public void Error(Exception ex)
        {
            try
            {
                List<string> list = new List<string>
                {
                    "***************************************************************\r\n",
                    string.Format("记录时间：{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now),
                    "线程ID  ：" + this.GetCurrentThreadId(),
                    "错误描述：" + ex.Message
                };
                bool flag = ex.StackTrace != null;
                if (flag)
                {
                    list.Add("异常堆栈：" + ex.StackTrace);
                }
                List<string> list2 = list;
                int index = list.Count - 1;
                list2[index] += "\r\n";
                File.AppendAllLines(this._logPath.ErrorPath, list);
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06000021 RID: 33 RVA: 0x000026F0 File Offset: 0x000008F0
        private string GetCurrentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }

        // Token: 0x0400000A RID: 10
        private readonly LogPath _logPath;
    }
}