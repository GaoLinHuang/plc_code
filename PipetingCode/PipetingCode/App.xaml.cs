using Newtonsoft.Json;
using PipettingCode.ViewModel;
using PipettingControl;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using Windows.Base;

namespace PipettingCode
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

        #endregion

        #region 夹瓶底错误弹窗

        #endregion

        #region 保存日志，一个是错误日志，一个是运行日志
        public static void SaveLog(string fileName, string msg)
        {
            lock (_locker)
            {
                #region 不存在目录，创建目录
                if (!Directory.Exists(LogDirName))
                {
                    Directory.CreateDirectory(LogDirName);
                }
                #endregion

                fileName = LogDirName + fileName;
                if (File.Exists(fileName))
                {
                    DateTime createTime = new FileInfo(fileName).CreationTime;
                    DateTime now = DateTime.Now;
                    int totalTime = SAVEDAYS * 24 * 60 * 60;
                    #region 7天删除日志
                    if (now.Subtract(createTime).TotalSeconds > totalTime)
                    {
                        File.SetCreationTime(fileName, DateTime.Now);            // 修改文件的创建时间
                        File.Delete(fileName);
                    }
                    #endregion
                }
                StreamWriter file = new(fileName, append: true);     // 添加到后面
                file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\n===\n" + msg + "\n===\n");
                file.Close();
            }
        }
        #endregion
    }

    #region 保存用户的配置
    public class MyConfig
    {
        // 样本预处理
        public ObservableCollection<CheckModel> PreDealCheck { get; set; }

        // 选择的是那一个试管类型，10混还是20混
        public int RealSelectedIndex { get; set; } = 0;
        // 选择的是哪一个试剂类型，16 还是 96
        public int ExperimentTypeIndex { get; set; } = 0;

        // 选择的是那一个试管类型，10混还是20混
        public int RealSelectedIndexBuild { get; set; } = 0;
        // 选择的是哪一个试剂类型，16 还是 96
        public int ExperimentTypeIndexBuild { get; set; } = 0;

        public MyConfig()
        {
            this.PreDealCheck = new();

        }
        public static void SaveConfigFile()
        {
            MyConfig my = new MyConfig()
            {
                PreDealCheck = CheckViewModel.GetInstance().MyCheck,


            };
            // 试剂

        }
    }
    #endregion

    public partial class App : Application
    {
        // 全局的调整参数窗口
        public App()
        {

        }

        #region 加载配置文件
        private void LoadConfigFile()
        {
            // 加载configFileName中的配置到配置中
            if (File.Exists(MySettingWindow.configFileName))
            {
                string result = File.ReadAllText(MySettingWindow.configFileName);
                MyConfig myConfig = result.ToObj<MyConfig>();
            }
        }
        #endregion
        protected override void OnStartup(StartupEventArgs e)
        {
            #region 避免重复打开
            bool OnlyOneInstance = false;
            Mutex mutex = new(true, "TopToolsApp", out OnlyOneInstance);

            if (OnlyOneInstance == false)
            {

                System.Environment.Exit(0);
                return;
            }

            #endregion

            base.OnStartup(e);

            //#region 激活程序
            //bool? b = null;
            //while (!ActivationManager.Instance.Verify())
            //{
            //    // 弹出激活窗口
            //    b = new ActivationWindow().ShowDialog();
            //    // 若点击“退出”，先跳出循环再退出程序，否则可能导致进程不能结束，产生内存泄漏
            //    // （可能由异步代码导致）
            //    if (b == false)
            //    {
            //        break;
            //    }
            //}
            //if (b == false)
            //{
            //    App.Current.Shutdown();
            //}
            //#endregion

            try
            {
                GetNode();

                PLCViewModel.GetInstance().PLC_SendCommand_Write("R180", 0);       // 复位

                #region 金域不需要登录界面
                //MainWindowViewModel.GetInstance().Login(new object());
                #endregion

                #region 朝杨需要登录界面
                #endregion
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            finally
            {
                #region 停止PLC流程
                PLCViewModel.GetInstance().PLC_SendCommand_Write("R11B", 1);
                PLCViewModel.GetInstance().PLC_SendCommand_Write("R101", 1);
                PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 0);         // 取消报警
                #endregion 
            }
        }

        #region 从配置文件中读取参数
        private void GetNode()
        {
            ReadNode();
            try
            {
                #region 1.系统参数
                Global_Parameter.SysXyxtkq = Convert.ToInt16(IniFile.SysXyxtkq);
                Global_Parameter.IPAddress = IniFile.IPAddress;                                             //仪器网卡IP地址
                Global_Parameter.IPPort = IniFile.IPPort;                                                   //仪器网卡使用的端口号
                Global_Parameter.Sys_SaveLog_Flag = IniFile.Sys_SaveLog_Flag;                               //写日日志标志  ON 写  OFF 不写

                Global_Parameter.Sys_SaveLog_Flag1 = IniFile.Sys_SaveLog_Flag1;                             //写收发数据日志标志  ON 写  OFF 不写


                Global_Parameter.AutoInitFlag = IniFile.AutoInitFlag;                                       //是否自动初始化标志
                #endregion
                #region 2.各模块节点值
                Global_Parameter.XCMDID = Get_StrToByte(IniFile.XCMDID);                                    //1.加样臂X轴地址
                Global_Parameter.YCMDID = Get_StrToByte(IniFile.YCMDID);                                    //2.加样臂Y轴地址
                Global_Parameter.ZCMDID = Get_StrToByte(IniFile.ZCMDID);                                    //3.加样臂Z轴地址
                Global_Parameter.PlungerCMDID = Get_StrToByte(IniFile.PlungerCMDID);                        //4.泵节点编号                     
                #endregion
                //txtNumberOfStitches.Text = "" + Global_Parameter.YCMDID.Length;
                #region  3.系统使用的所有节点，节点匹配使用
                Global_Parameter.Node_ID = IniFile.XCMDID;
                Global_Parameter.Node_ID += "," + IniFile.YCMDID;
                Global_Parameter.Node_ID += "," + IniFile.ZCMDID;
                Global_Parameter.Node_ID += "," + IniFile.PlungerCMDID;
                //去掉重复的编号
                string[] strPos = Global_Parameter.Node_ID.Split(',');
                string m_C1 = "";
                string m_OldC1 = "";
                string m_S1 = "";
                for (int i = 0; i < strPos.Length; i++)
                {
                    m_C1 = strPos[i];
                    if (m_C1 != m_OldC1)
                    {
                        m_OldC1 = m_C1;
                        if (m_S1 == "")
                        {
                            m_S1 = m_C1;
                        }
                        else
                        {
                            m_S1 += "," + m_C1;
                        }
                    }
                }
                Global_Parameter.Node_ID = m_S1;
                #endregion

                #region 4.加样臂使用的坐标
                //1.试管架
                Global_Parameter.TubersStartX = int.Parse(IniFile.TubersStartX);                            //1.试管架开始X轴坐标
                Global_Parameter.TubersStartY = int.Parse(IniFile.TubersStartY);                            //2.试管架开始Y轴坐标       
                Global_Parameter.TubersStartZ = int.Parse(IniFile.TubersStartZ);                            //3.试管架开始Z轴坐标
                Global_Parameter.TubersEndX = int.Parse(IniFile.TubersEndX);                                //4.试管架结束X轴坐标
                Global_Parameter.TubersEndY = int.Parse(IniFile.TubersEndY);                                //5.试管架结束Y轴坐标
                Global_Parameter.TubersEndZ = int.Parse(IniFile.TubersEndZ);                                //6.试管架结束Z轴坐标
                Global_Parameter.TubersMaxZ = int.Parse(IniFile.TubersMaxZ);                                //3.试管架开始Z轴坐标
                Global_Parameter.TubersMoveZ = int.Parse(IniFile.TubersMoveZ);                              //3.试管架开始Z轴坐标
                Global_Parameter.TubersX = int.Parse(IniFile.TubersX);                                      // 吸液位置不等分
                Global_Parameter.TubersY = Get_StrToInt(IniFile.TubersY);                                   // 吸液位置不等分
                Global_Parameter.TubersX1 = int.Parse(IniFile.TubersX1);                                      // 吸液位置不等分
                if (Global_Parameter.TubersX1 == 0)
                {
                    Global_Parameter.TubersX1 = Global_Parameter.TubersStartX;                              //如果没有设置，就用吸液位置
                    IniFile.TubersX1 = "" + Global_Parameter.TubersX1;
                }

                //3.脱针
                Global_Parameter.NeedleRemovalStartX = int.Parse(IniFile.NeedleRemovalStartX);              //13.脱针位置坐标
                Global_Parameter.NeedleRemovalStartY = int.Parse(IniFile.NeedleRemovalStartY);              //14.脱针位置坐标
                Global_Parameter.NeedleRemovalStartZ = int.Parse(IniFile.NeedleRemovalStartZ);              //15.脱针位置坐标
                Global_Parameter.NeedleRemovalEndX = int.Parse(IniFile.NeedleRemovalEndX);                  //16.脱针位置坐标
                Global_Parameter.NeedleRemovalEndY = int.Parse(IniFile.NeedleRemovalEndY);                  //17.脱针位置坐标
                Global_Parameter.NeedleRemovalEndZ = int.Parse(IniFile.NeedleRemovalEndZ);                  //18.脱针位置坐标
                Global_Parameter.NeedleRemovalMaxZ = int.Parse(IniFile.NeedleRemovalMaxZ);                  //15.脱针位置坐标
                Global_Parameter.NeedleRemovalMoveZ = int.Parse(IniFile.NeedleRemovalMoveZ);                //15.脱针位置坐标
                Global_Parameter.NeedleRemovalStartX1 = int.Parse(IniFile.NeedleRemovalStartX1);           //13.脱针位置缺口X轴坐标

                Global_Parameter.TakeNeedle_Judge_Distance = int.Parse(IniFile.TakeNeedle_Judge_Distance);                      //Z轴初始化位置坐标
                Global_Parameter.TakeNeedle_Judge_MinDistance = int.Parse(IniFile.TakeNeedle_Judge_MinDistance);                //取针失步判断最小值
                Global_Parameter.TakeNeedle_Judge_MaxDistance = int.Parse(IniFile.TakeNeedle_Judge_MaxDistance);                //取针失步判断最大值
                Global_Parameter.RemovealNeedle_Judge_MinDistance = int.Parse(IniFile.RemovealNeedle_Judge_MinDistance);        //脱针失步判断最小值
                Global_Parameter.RemovealNeedle_Judge_MaxDistance = int.Parse(IniFile.RemovealNeedle_Judge_MaxDistance);        //脱针失步判断最大值
                Global_Parameter.TakeNeedle_DelayTime = int.Parse(IniFile.TakeNeedle_DelayTime);                                //取针时候，两根针之间的时间间隔
                Global_Parameter.RemovealNeedle_DelayTime = int.Parse(IniFile.RemovealNeedle_DelayTime);                        //脱针时候，两根针之间的时间间隔


                //5.取针
                Global_Parameter.NeedleStartX = Get_StrToInt(IniFile.NeedleStartX);                         //25.针盒位开始X轴坐标
                Global_Parameter.NeedleStartY = Get_StrToInt(IniFile.NeedleStartY);                         //26.针盒位开始Y轴坐标
                Global_Parameter.NeedleStartZ = Get_StrToInt(IniFile.NeedleStartZ);                         //27.针盒位开始Z轴坐标
                Global_Parameter.NeedleEndX = Get_StrToInt(IniFile.NeedleEndX);                             //28.针盒位结束X轴坐标
                Global_Parameter.NeedleEndY = Get_StrToInt(IniFile.NeedleEndY);                             //29.针盒位结束Y轴坐标
                Global_Parameter.NeedleEndZ = Get_StrToInt(IniFile.NeedleEndZ);                             //30.针盒位结束Z轴坐标
                Global_Parameter.NeedleMaxZ = Get_StrToInt(IniFile.NeedleMaxZ);                             //27.针盒位开始Z轴坐标
                Global_Parameter.NeedleMoveZ = Get_StrToInt(IniFile.NeedleMoveZ);                           //27.针盒位开始Z轴坐标
                //6.加样位
                Global_Parameter.SampleStartX = Get_StrToInt(IniFile.SampleStartX);                         //31.加样位开始X轴坐标
                Global_Parameter.SampleStartY = Get_StrToInt(IniFile.SampleStartY);                         //32.加样位开始Y轴坐标
                Global_Parameter.SampleStartZ = Get_StrToInt(IniFile.SampleStartZ);                         //33.加样位开始Z轴坐标
                Global_Parameter.SampleEndX = Get_StrToInt(IniFile.SampleEndX);                             //34.加样位结束X轴坐标
                Global_Parameter.SampleEndY = Get_StrToInt(IniFile.SampleEndY);                             //35.加样位结束Y轴坐标
                Global_Parameter.SampleEndZ = Get_StrToInt(IniFile.SampleEndZ);                             //36.加样位结束Z轴坐标
                Global_Parameter.SampleMaxZ = Get_StrToInt(IniFile.SampleMaxZ);                             //33.加样位开始Z轴坐标
                Global_Parameter.SampleMoveZ = Get_StrToInt(IniFile.SampleMoveZ);                           //33.加样位开始Z轴坐标
                #endregion

                #region 5.加样臂、夹手臂、泵的速度和加速度
                #region （1）.加样臂的速度和加速度
                //1.加样臂 刚接电时速度和加速度
                Global_Parameter.XSlowSpeed = Get_StrToInt(IniFile.XSlowSpeed);
                Global_Parameter.XSlowAccSpeed = Get_StrToInt(IniFile.XSlowAccSpeed);
                Global_Parameter.YSlowSpeed = Get_StrToInt(IniFile.YSlowSpeed);
                Global_Parameter.YSlowAccSpeed = Get_StrToInt(IniFile.YSlowAccSpeed);
                Global_Parameter.ZSlowSpeed = Get_StrToInt(IniFile.ZSlowSpeed);
                Global_Parameter.ZSlowAccSpeed = Get_StrToInt(IniFile.ZSlowAccSpeed);

                Global_Parameter.YAccSpeed = int.Parse(IniFile.YAccSpeed);
                //2.加样臂 初始化速度和加速度
                Global_Parameter.XInitSpeed = Get_StrToInt(IniFile.XInitSpeed);
                Global_Parameter.XInitAccSpeed = Get_StrToInt(IniFile.XInitAccSpeed);
                Global_Parameter.YInitSpeed = Get_StrToInt(IniFile.YInitSpeed);
                Global_Parameter.YInitAccSpeed = Get_StrToInt(IniFile.YInitAccSpeed);
                Global_Parameter.ZInitSpeed = Get_StrToInt(IniFile.ZInitSpeed);
                Global_Parameter.ZInitAccSpeed = Get_StrToInt(IniFile.ZInitAccSpeed);
                //3.加样臂 运行时速度和加速度
                Global_Parameter.XMoveSpeed = Get_StrToInt(IniFile.XMoveSpeed);
                Global_Parameter.XMoveAccSpeed = Get_StrToInt(IniFile.XMoveAccSpeed);
                Global_Parameter.YMoveSpeed = Get_StrToInt(IniFile.YMoveSpeed);
                Global_Parameter.YMoveAccSpeed = Get_StrToInt(IniFile.YMoveAccSpeed);
                Global_Parameter.ZMoveSpeed = Get_StrToInt(IniFile.ZMoveSpeed);
                Global_Parameter.ZMoveAccSpeed = Get_StrToInt(IniFile.ZMoveAccSpeed);
                #endregion

                #region （3）.泵的速度和加速度
                //1.刚上电时候使用的速度和加速度
                Global_Parameter.PlungerPumpSlowSpeed = Get_StrToInt(IniFile.PlungerPumpSlowSpeed);
                Global_Parameter.PlungerPumpSlowAccSpeed = Get_StrToInt(IniFile.PlungerPumpSlowAccSpeed);
                //2.初始化时候使用的速度和加速度
                Global_Parameter.PlungerPumpInitSpeed = Get_StrToInt(IniFile.PlungerPumpInitSpeed);
                Global_Parameter.PlungerPumpInitAccSpeed = Get_StrToInt(IniFile.PlungerPumpInitAccSpeed);
                //3.运行时候使用的速度和加速度
                Global_Parameter.PlungerPumpMoveSpeed = Get_StrToInt(IniFile.PlungerPumpMoveSpeed);
                Global_Parameter.PlungerPumpMoveAccSpeed = Get_StrToInt(IniFile.PlungerPumpMoveAccSpeed);
                #endregion

                #endregion
                #region 7.预备执行组
                Global_Parameter.YGroup = (byte)(IniFile.YGroup.HexStringToInt());                          //Y轴预备执行组组名 0x02
                Global_Parameter.ZGroup = (byte)(IniFile.ZGroup.HexStringToInt());                          //Z轴预备执行组组名 0x04
                Global_Parameter.XGroup = (byte)(IniFile.XGroup.HexStringToInt());                          //X轴预备执行组组名 0x06
                Global_Parameter.PumpGroup = (byte)(IniFile.PumpGroup.HexStringToInt());                    //泵预备执行组组名  0x08
                #endregion
                #region 吸液、注液系数对照表
                Global_Parameter.PlungerAspVoll = IniFile.PlungerAspVoll;                                       //泵吸液注液参数
                Global_Parameter.PlungerAspVol2 = IniFile.PlungerAspVol2;                                       //泵吸液注液参数
                Global_Parameter.PlungerAspVol3 = IniFile.PlungerAspVol3;                                       //泵吸液注液参数
                Global_Parameter.PlungerAspVol4 = IniFile.PlungerAspVol4;                                       //泵吸液注液参数
                Global_Parameter.PlungerAspVol5 = IniFile.PlungerAspVol5;                                       //泵吸液注液参数
                Global_Parameter.PlungerAspVol6 = IniFile.PlungerAspVol6;                                       //泵吸液注液参数
                Global_Parameter.PlungerAspVol7 = IniFile.PlungerAspVol7;                                       //泵吸液注液参数
                Global_Parameter.PlungerAspVol8 = IniFile.PlungerAspVol8;                                       //泵吸液注液参数

                Global_Parameter.PlungerDspVoll = IniFile.PlungerDspVoll;                                       //泵吸液注液参数
                Global_Parameter.PlungerDspVol2 = IniFile.PlungerDspVol2;                                       //泵吸液注液参数
                Global_Parameter.PlungerDspVol3 = IniFile.PlungerDspVol3;                                       //泵吸液注液参数
                Global_Parameter.PlungerDspVol4 = IniFile.PlungerDspVol4;                                       //泵吸液注液参数
                Global_Parameter.PlungerDspVol5 = IniFile.PlungerDspVol5;                                       //泵吸液注液参数
                Global_Parameter.PlungerDspVol6 = IniFile.PlungerDspVol6;                                       //泵吸液注液参数
                Global_Parameter.PlungerDspVol7 = IniFile.PlungerDspVol7;                                       //泵吸液注液参数
                Global_Parameter.PlungerDspVol8 = IniFile.PlungerDspVol8;                                       //泵吸液注液参数
                #endregion
                #region 固定吸液位置回升高度
                Global_Parameter.Liftingheight = int.Parse(IniFile.Liftingheight);                                              //2022-08-05 new ADD

                Global_Parameter.TakeNeedleSpeed = int.Parse(IniFile.TakeNeedleSpeed);                                          //2022-08-22 new ADD
                Global_Parameter.TakeNeedleAccSpeed = int.Parse(IniFile.TakeNeedleAccSpeed);                                    //2022-08-22 new ADD
                Global_Parameter.NeedleRemovalSpeed = int.Parse(IniFile.NeedleRemovalSpeed);                                    //2022-08-22 new ADD
                Global_Parameter.NeedleRemovalAccSpeed = int.Parse(IniFile.NeedleRemovalAccSpeed);                              //2022-08-22 new ADD
                Global_Parameter.JudgmentValue = int.Parse(IniFile.JudgmentValue);                                              //2022-10-22 new ADD
                Global_Parameter.TakeNeedlemoment = int.Parse(IniFile.TakeNeedlemoment);                                        //2022-10-22 new ADD
                Global_Parameter.NeedleRemovalmoment = int.Parse(IniFile.NeedleRemovalmoment);                                  //2022-10-22 new ADD

                #endregion


                Global_Parameter.DescendingHeight = int.Parse(IniFile.DescendingHeight);                                        //2022-10-27 new ADD

                Global_Parameter.TakeNeedlemoment = int.Parse(IniFile.TakeNeedlemoment);                                        //2022-10-22 new ADD
                Global_Parameter.NeedleRemovalmoment = int.Parse(IniFile.NeedleRemovalmoment);                                  //2022-10-22 new ADD

                Global_Parameter.TakeNeedleIdleMoment = (byte)int.Parse(IniFile.TakeNeedleIdleMoment);                          //空闲力矩
                Global_Parameter.TakeNeedleLockMoment = (byte)int.Parse(IniFile.TakeNeedleLockMoment);                          //锁定力矩
                Global_Parameter.TakeNeedleMoveMoment = (byte)int.Parse(IniFile.TakeNeedleMoveMoment);                          //运动力矩

                Global_Parameter.NeedleRemovalleIdleMoment = (byte)int.Parse(IniFile.NeedleRemovalleIdleMoment);                //空闲力矩
                Global_Parameter.NeedleRemovalLockMoment = (byte)int.Parse(IniFile.NeedleRemovalLockMoment);                    //锁定力矩
                Global_Parameter.NeedleRemovalMoveMoment = (byte)int.Parse(IniFile.NeedleRemovalMoveMoment);                    //运动力矩

                Global_Parameter.TakeNeedleClearance = int.Parse(IniFile.TakeNeedleClearance);                                  //2022-10-27 new ADD

                Global_Parameter.ZXyMoveSpeed = int.Parse(IniFile.ZXyMoveSpeed);                    //Z轴吸液提起来初始化的速度 2022-10-29 new add
                Global_Parameter.ZXyMoveAccSpeed = int.Parse(IniFile.ZXyMoveAccSpeed);              //Z轴吸液提起来初始化的加速度 2022-10-29 new add

                Global_Parameter.ZXySlowSpeed = int.Parse(IniFile.ZXySlowSpeed);                    // 2022-11-01 new add
                Global_Parameter.ZXySlowAccSpeed = int.Parse(IniFile.ZXySlowAccSpeed);              // 2022-11-01 new add

                Global_Parameter.ZQzSlowSpeed = int.Parse(IniFile.ZQzSlowSpeed);                    // 2022-11-01 new add
                Global_Parameter.ZQzSlowAccSpeed = int.Parse(IniFile.ZQzSlowAccSpeed);              // 2022-11-01 new add

                Global_Parameter.mImbibitionFlag = int.Parse(IniFile.ImbibitionFlag);               // 2022-11-04 new add
                Global_Parameter.mWbSuctionSpeed = int.Parse(IniFile.WbSuctionSpeed);               // 2022-11-04 new add
                Global_Parameter.mWbSuctionAccSpeed = int.Parse(IniFile.WbSuctionAccSpeed);         // 2022-11-04 new add

            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
        }
        #endregion

        #region 辅助函数

        #region 转换函数
        /// <summary>
        /// 文本字符串转换Byte
        /// </summary>
        /// <param name="In_Str"></param>
        /// <returns></returns>
        private byte[] Get_StrToByte(string In_Str)
        {
            byte[] a1 = null;
            try
            {
                string[] strPos = In_Str.Split(',');
                a1 = new byte[strPos.Length];
                for (int i = 0; i < strPos.Length; i++)
                {
                    a1[i] = (byte)(strPos[i].HexStringToInt());
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, "Get_StrToByte errors:" + ex.Message);
            }
            return a1;
        }
        /// <summary>
        /// 文本字符串转换Int
        /// </summary>
        /// <param name="In_Str"></param>
        /// <returns></returns>
        private int[] Get_StrToInt(string In_Str)
        {
            int[] a1 = null;
            try
            {
                string[] strPos = In_Str.Split(',');
                a1 = new int[strPos.Length];
                for (int i = 0; i < strPos.Length; i++)
                {
                    a1[i] = int.Parse(strPos[i]);
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return a1;
        }
        #endregion

        #region 初始化，从demoConfig.ini中读取参数
        public void ReadNode()
        {
            string fileName = MySettingWindow.DemoConfigName;

            #region 样本预处理参数
            string section = "Parameter";

            try
            {
                // 针管数
                PipettingParameter.TxtNumberOfStitches = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtNumberOfStitches", "4"));

                #region 吸液参数
                // 戳到硬物的失步阈值
                PipettingParameter.TextBox1 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox1", "50"));

                #region 是否开启液面探测
                if (IniUtil.INIGetStringValue(fileName, section, "chkLiquidLevelDetection", "OFF") == "ON")
                {
                    PipettingParameter.ChkLiquidLevelDetection = true;
                }
                else
                {
                    PipettingParameter.ChkLiquidLevelDetection = false;
                }
                #endregion

                #region 是否开启不等分吸液，默认不等分
                if (IniUtil.INIGetStringValue(fileName, section, "checkBox7", "OFF") == "ON")
                {
                    PipettingParameter.CheckBox7 = true;
                }
                else
                {
                    PipettingParameter.CheckBox7 = false;
                }
                #endregion

                #region 气压阈值
                PipettingParameter.ThresholdPress = new int[PipettingParameter.TxtNumberOfStitches];
                string parameter = "thresholdPress";
                for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                {
                    string tmp = parameter + (i + 1).ToString();
                    PipettingParameter.ThresholdPress[i] = int.Parse(IniUtil.INIGetStringValue(fileName, section, tmp, "500"));
                }
                #endregion

                // 吸液量
                PipettingParameter.TxtSuctionVolume = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtSuctionVolume", "500"));
                // 吸液回吐量
                PipettingParameter.TxtRegurgitation = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtRegurgitation", "100"));
                // 液面探测最低高度
                PipettingParameter.MtxtHeightLimited = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtHeightLimited", "1200"));

                // 10混吸液高度
                PipettingParameter.TxtImmersionDepth10 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtImmersionDepth10", "1100"));
                // 20混吸液高度
                PipettingParameter.TxtImmersionDepth20 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtImmersionDepth20", "1100"));

                // 回吐高度
                PipettingParameter.TxtRetrusionHeight = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtRetrusionHeight", "300"));
                // 浸没深度
                PipettingParameter.MtxtImmersionDepth = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtImmersionDepth", "100"));

                // 吸液延迟
                PipettingParameter.TxtDelay = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtDelay", "50"));
                // 针尖间隙
                PipettingParameter.TxtTipClearance = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtTipClearance", "50"));
                // 容器底面积
                PipettingParameter.MtxtContainerArea = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtContainerArea", "10"));

                // 吸液速度
                PipettingParameter.MtxtVolumeSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtVolumeSpeed", "2000"));
                // 针尾间隙
                PipettingParameter.TxtNeedleTailGap = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtNeedleTailGap", "50"));
                // 返回探测高度
                PipettingParameter.MtxtFhtcgd = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtFhtcgd", "0"));

                // 吸液加速度
                PipettingParameter.MtxtVolumeAccSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtVolumeAccSpeed", "100"));
                // 液面探测速度
                PipettingParameter.TextBox3 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox3", "500"));
                // 当前位置
                PipettingParameter.TextBox1 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox1", "0"));

                // 回吸量
                PipettingParameter.TxtDesorptionL = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtDesorptionL", "100"));
                // 10混、20混液面探测高度
                PipettingParameter.LiquildDectectionHeight10 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "LiquildDectectionHeight10", "500"));
                // 单混液面探测高度
                PipettingParameter.LiquildDectectionHeight01 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "LiquildDectectionHeight01", "500"));
                #endregion

                #region 注液参数
                // 注液速度
                PipettingParameter.MInjectionPumpSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionPumpSpeed", "1000"));
                // 注液量
                PipettingParameter.MInjectionPumpSpace = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionPumpSpace", "600"));
                // 注液高度
                PipettingParameter.MInjectionHeight = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionHeight", "90"));
                // 注液次数
                PipettingParameter.TextBox5 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox5", "1"));

                // 注液加速度
                PipettingParameter.MInjectionPumpAccSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionPumpAccSpeed", "100"));
                // 注液延迟
                PipettingParameter.TextBox2 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox2", "50"));
                // 注液完毕Z轴提起高度
                PipettingParameter.MInjectionZSpace = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionZSpace", "0"));
                #endregion

            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            #endregion

            #region 体系构建参数
            section = "Build";

            try
            {
                // 针管数
                PipettingParameterBuild.TxtNumberOfStitches = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtNumberOfStitches", "4"));

                #region 吸液参数
                // 戳到硬物的失步阈值
                PipettingParameterBuild.TextBox1 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox1", "50"));

                #region 是否开启液面探测
                if (IniUtil.INIGetStringValue(fileName, section, "chkLiquidLevelDetection", "OFF") == "ON")
                {
                    PipettingParameterBuild.ChkLiquidLevelDetection = true;
                }
                else
                {
                    PipettingParameterBuild.ChkLiquidLevelDetection = false;
                }
                #endregion

                #region 是否开启不等分吸液，默认不等分
                if (IniUtil.INIGetStringValue(fileName, section, "checkBox7", "OFF") == "ON")
                {
                    PipettingParameterBuild.CheckBox7 = true;
                }
                else
                {
                    PipettingParameterBuild.CheckBox7 = false;
                }
                #endregion

                #region 气压阈值
                PipettingParameterBuild.ThresholdPress = new int[PipettingParameterBuild.TxtNumberOfStitches];
                string parameter = "thresholdPress";
                for (int i = 0; i < PipettingParameterBuild.TxtNumberOfStitches; ++i)
                {
                    string tmp = parameter + (i + 1).ToString();
                    PipettingParameterBuild.ThresholdPress[i] = int.Parse(IniUtil.INIGetStringValue(fileName, section, tmp, "700"));
                }
                #endregion

                // 吸液量
                PipettingParameterBuild.TxtSuctionVolume = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtSuctionVolume", "500"));
                // 吸液回吐量
                PipettingParameterBuild.TxtRegurgitation = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtRegurgitation", "100"));
                // 液面探测最低高度
                PipettingParameterBuild.MtxtHeightLimited = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtHeightLimited", "1200"));

                // 10混吸液高度
                PipettingParameterBuild.TxtImmersionDepth10 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtImmersionDepth10", "1100"));
                // 20混吸液高度
                PipettingParameterBuild.TxtImmersionDepth20 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtImmersionDepth20", "1100"));

                // 回吐高度
                PipettingParameterBuild.TxtRetrusionHeight = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtRetrusionHeight", "300"));
                // 浸没深度
                PipettingParameterBuild.MtxtImmersionDepth = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtImmersionDepth", "100"));

                // 吸液延迟
                PipettingParameterBuild.TxtDelay = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtDelay", "50"));
                // 针尖间隙
                PipettingParameterBuild.TxtTipClearance = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtTipClearance", "50"));
                // 容器底面积
                PipettingParameterBuild.MtxtContainerArea = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtContainerArea", "10"));

                // 吸液速度
                PipettingParameterBuild.MtxtVolumeSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtVolumeSpeed", "2000"));
                // 针尾间隙
                PipettingParameterBuild.TxtNeedleTailGap = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtNeedleTailGap", "50"));
                // 返回探测高度
                PipettingParameterBuild.MtxtFhtcgd = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtFhtcgd", "0"));

                // 吸液加速度
                PipettingParameterBuild.MtxtVolumeAccSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mtxtVolumeAccSpeed", "100"));
                // 液面探测速度
                PipettingParameterBuild.TextBox3 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox3", "500"));
                // 当前位置
                PipettingParameterBuild.TextBox1 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox1", "0"));

                // 回吸量
                PipettingParameterBuild.TxtDesorptionL = int.Parse(IniUtil.INIGetStringValue(fileName, section, "txtDesorptionL", "100"));
                // 10混、20混液面探测高度
                PipettingParameterBuild.LiquildDectectionHeight10 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "LiquildDectectionHeight10", "500"));
                // 单混液面探测高度
                PipettingParameterBuild.LiquildDectectionHeight01 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "LiquildDectectionHeight01", "500"));

                #endregion

                #region 注液参数
                // 注液速度
                PipettingParameterBuild.MInjectionPumpSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionPumpSpeed", "1000"));
                // 注液量
                PipettingParameterBuild.MInjectionPumpSpace = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionPumpSpace", "600"));
                // 注液高度
                PipettingParameterBuild.MInjectionHeight = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionHeight", "90"));
                // 注液次数
                PipettingParameterBuild.TextBox5 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox5", "1"));

                // 注液加速度
                PipettingParameterBuild.MInjectionPumpAccSpeed = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionPumpAccSpeed", "100"));
                // 注液延迟
                PipettingParameterBuild.TextBox2 = int.Parse(IniUtil.INIGetStringValue(fileName, section, "textBox2", "50"));
                // 注液完毕Z轴提起高度
                PipettingParameterBuild.MInjectionZSpace = int.Parse(IniUtil.INIGetStringValue(fileName, section, "mInjectionZSpace", "0"));
                #endregion

            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            #endregion

            #region 从PLCConfig.ini中读取COM口
            fileName = MySettingWindow.PLCConfigName;
            section = "COM";
            try
            {
                MySettingWindow.COM = IniUtil.INIGetStringValue(fileName, section, "COM", "COM3");
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            #endregion

        }
        #endregion

        #endregion

    }

    #region democonfig.ini对应的参数
    public class PipettingParameter
    {
        // 针管数
        public static int TxtNumberOfStitches { get; set; }
        // 气压阈值
        public static int[] ThresholdPress { get; set; }
        // 吸液量
        public static int TxtSuctionVolume { get; set; }
        // 吸液回吐量
        public static int TxtRegurgitation { get; set; }
        // 液面探测最低高度
        public static int MtxtHeightLimited { get; set; }
        // 10混吸液高度
        public static int TxtImmersionDepth10 { get; set; }
        // 20混吸液高度
        public static int TxtImmersionDepth20 { get; set; }
        // 回吐高度
        public static int TxtRetrusionHeight { get; set; }
        // 浸没深度
        public static int MtxtImmersionDepth { get; set; }
        // 吸液延迟
        public static int TxtDelay { get; set; }
        // 针尖间隙
        public static int TxtTipClearance { get; set; }
        // 容器底面积
        public static int MtxtContainerArea { get; set; }
        // 吸液速度
        public static int MtxtVolumeSpeed { get; set; }
        // 针尾间隙
        public static int TxtNeedleTailGap { get; set; }
        // 返回探测高度
        public static int MtxtFhtcgd { get; set; }
        // 吸液加速度
        public static int MtxtVolumeAccSpeed { get; set; }
        // 液面探测速度
        public static int TextBox3 { get; set; }
        // 戳到硬物失步阈值
        public static int TextBox1 { get; set; }
        // 回吸量
        public static int TxtDesorptionL { get; set; }
        // 10混、20混液面探测高度
        public static int LiquildDectectionHeight10 { get; set; }
        // 单混液面探测高度
        public static int LiquildDectectionHeight01 { get; set; }
        // 注液速度
        public static int MInjectionPumpSpeed { get; set; }
        // 注液量
        public static int MInjectionPumpSpace { get; set; }
        // 注液高度
        public static int MInjectionHeight { get; set; }
        // 注液次数
        public static int TextBox5 { get; set; }
        // 注液加速度
        public static int MInjectionPumpAccSpeed { get; set; }
        // 注液延迟
        public static int TextBox2 { get; set; }
        // 注液完毕Z轴提起高度
        public static int MInjectionZSpace { get; set; }
        // 是否开启液面探测
        public static bool ChkLiquidLevelDetection { get; set; }
        // 是否不等分吸液位置
        public static bool CheckBox7 { get; set; }
    }

    public class PipettingParameterBuild
    {
        // 针管数
        public static int TxtNumberOfStitches { get; set; }
        // 气压阈值
        public static int[] ThresholdPress { get; set; }
        // 吸液量
        public static int TxtSuctionVolume { get; set; }
        // 吸液回吐量
        public static int TxtRegurgitation { get; set; }
        // 液面探测最低高度
        public static int MtxtHeightLimited { get; set; }
        // 10混吸液高度
        public static int TxtImmersionDepth10 { get; set; }
        // 20混吸液高度
        public static int TxtImmersionDepth20 { get; set; }
        // 回吐高度
        public static int TxtRetrusionHeight { get; set; }
        // 浸没深度
        public static int MtxtImmersionDepth { get; set; }
        // 吸液延迟
        public static int TxtDelay { get; set; }
        // 针尖间隙
        public static int TxtTipClearance { get; set; }
        // 容器底面积
        public static int MtxtContainerArea { get; set; }
        // 吸液速度
        public static int MtxtVolumeSpeed { get; set; }
        // 针尾间隙
        public static int TxtNeedleTailGap { get; set; }
        // 返回探测高度
        public static int MtxtFhtcgd { get; set; }
        // 吸液加速度
        public static int MtxtVolumeAccSpeed { get; set; }
        // 液面探测速度
        public static int TextBox3 { get; set; }
        // 戳到硬物失步阈值
        public static int TextBox1 { get; set; }
        // 回吸量
        public static int TxtDesorptionL { get; set; }
        // 10混、20混液面探测高度
        public static int LiquildDectectionHeight10 { get; set; }
        // 单混液面探测高度
        public static int LiquildDectectionHeight01 { get; set; }
        // 注液速度
        public static int MInjectionPumpSpeed { get; set; }
        // 注液量
        public static int MInjectionPumpSpace { get; set; }
        // 注液高度
        public static int MInjectionHeight { get; set; }
        // 注液次数
        public static int TextBox5 { get; set; }
        // 注液加速度
        public static int MInjectionPumpAccSpeed { get; set; }
        // 注液延迟
        public static int TextBox2 { get; set; }
        // 注液完毕Z轴提起高度
        public static int MInjectionZSpace { get; set; }
        // 是否开启液面探测
        public static bool ChkLiquidLevelDetection { get; set; }
        // 是否不等分吸液位置
        public static bool CheckBox7 { get; set; }
    }
    #endregion

   
}
