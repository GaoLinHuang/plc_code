using PipettingCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipetitngCode.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    // 参数窗口类
    public class MySettingWindow
    {
        #region 各种配置、日志文件
        public static readonly object _locker = new();
        public static readonly string resultFileName = ".\\result.json";                // 结果
        public static readonly string userFileName = ".\\User.json";                    // 用户
        public static readonly string configFileName = ".\\MyConfig.json";              // 上一轮的配置 
        public static readonly string PLCConfigName = ".\\PLCConfig.ini";
        public static readonly string DemoConfigName = ".\\demoConfig.ini";
        public static readonly string ErrorLog = ".\\error.log";                         // 错误日志
        public static readonly string RunningLog = ".\\runing.log";                      // 运行日志
        public static readonly string LogDirName = ".\\日志";                               // 日志目录
        public static string COM = "COM3";                                                 // 默认COM3
        public static readonly int SAVEDAYS = 7;                                         // 日志保存时间
        public static readonly int SAVERESULTDAYS = 3;                                   // 实验结果保存天数
        public static readonly string SCANDIR = "扫码结果";                              // 扫码结果目录
        public static readonly string PRESSDIR = "气压结果";                             // 气压结果目录
        #endregion

        #region PLC连接错误弹窗
        //public static void ShowPLCErrorWindow()
        //{
        //    if (MainWindowViewModel.GetInstance().PLCErrorWindowIsPrompt == false)
        //    {
        //        MainWindowViewModel.GetInstance().PLCErrorWindowIsPrompt = true;
        //        new PLCErrorWindow().ShowDialog();
        //    }
        //}
        #endregion

        #region 夹瓶底错误弹窗
        //public static void ShowJiaPingErrorWindow()
        //{
        //    if (MainWindowViewModel.GetInstance().JiaPingErrorWindowIsPrompt == false)
        //    {
        //        MainWindowViewModel.GetInstance().JiaPingErrorWindowIsPrompt = true;
        //        new JiaPingMsgWindow().ShowDialog();
        //    }
        //}
        #endregion

        #region 保存日志，一个是错误日志，一个是运行日志
        public static void SaveLog(string fileName, string msg)
        {
            //lock (_locker)
            //{
            //    #region 不存在目录，创建目录
            //    if (!Directory.Exists(LogDirName))
            //    {
            //        Directory.CreateDirectory(LogDirName);
            //    }
            //    #endregion

            //    fileName = LogDirName + fileName;
            //    if (File.Exists(fileName))
            //    {
            //        DateTime createTime = new FileInfo(fileName).CreationTime;
            //        DateTime now = DateTime.Now;
            //        int totalTime = SAVEDAYS * 24 * 60 * 60;
            //        #region 7天删除日志
            //        if (now.Subtract(createTime).TotalSeconds > totalTime)
            //        {
            //            File.SetCreationTime(fileName, DateTime.Now);            // 修改文件的创建时间
            //            File.Delete(fileName);
            //        }
            //        #endregion
            //    }
            //    StreamWriter file = new(fileName, append: true);     // 添加到后面
            //    file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\n===\n" + msg + "\n===\n");
            //    file.Close();
            //}
        }
        #endregion
    }
}
