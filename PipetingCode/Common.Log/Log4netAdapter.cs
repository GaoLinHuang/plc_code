using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace Common.Log
{
    // Token: 0x02000004 RID: 4
    public class Log4netAdapter : ILogger
    {
        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000011 RID: 17 RVA: 0x000020D7 File Offset: 0x000002D7
        public string LogFolder { get; }

        // Token: 0x06000012 RID: 18 RVA: 0x000022F0 File Offset: 0x000004F0
        public Log4netAdapter(string appName)
        {
            this._logPath = new LogPath(appName);
            this.SetGlobalProperties(appName);
            this.InitConfig();
            this._logInfo = LogManager.GetLogger("loginfo");
            this._logError = LogManager.GetLogger("logerror");
            this._logDebugview = LogManager.GetLogger("logdebugview");
        }

        // Token: 0x06000013 RID: 19 RVA: 0x000020DF File Offset: 0x000002DF
        private void SetGlobalProperties(string appName)
        {
            GlobalContext.Properties["AppName"] = appName;
            GlobalContext.Properties["LogFolder"] = this._logPath.GetLogFolder() + "\\";
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002350 File Offset: 0x00000550
        private void InitConfig()
        {
            Assembly assembly = base.GetType().Assembly;
            Stream manifestResourceStream = assembly.GetManifestResourceStream("Common.Log.log4net.config");
            XmlConfigurator.Configure(manifestResourceStream);
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002118 File Offset: 0x00000318
        public void Error(string error)
        {
            this._logDebugview.Error(error);
            this._logError.Error(error);
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002135 File Offset: 0x00000335
        public void Error(string error, Exception ex)
        {
            this._logDebugview.Error(error, ex);
            this._logError.Error(error, ex);
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00002154 File Offset: 0x00000354
        public void Error(Exception ex)
        {
            this._logDebugview.Error(ex.Message, ex);
            this._logError.Error(ex.Message, ex);
        }

        // Token: 0x06000018 RID: 24 RVA: 0x0000217D File Offset: 0x0000037D
        public void Info(string info)
        {
            this._logDebugview.Info(info);
            this._logInfo.Info(info);
        }

        // Token: 0x06000019 RID: 25 RVA: 0x0000217D File Offset: 0x0000037D
        public void Message(string message)
        {
            this._logDebugview.Info(message);
            this._logInfo.Info(message);
        }

        // Token: 0x04000005 RID: 5
        private readonly ILog _logInfo;

        // Token: 0x04000006 RID: 6
        private readonly ILog _logError;

        // Token: 0x04000007 RID: 7
        private readonly ILog _logDebugview;

        // Token: 0x04000008 RID: 8
        private readonly LogPath _logPath;
    }
}