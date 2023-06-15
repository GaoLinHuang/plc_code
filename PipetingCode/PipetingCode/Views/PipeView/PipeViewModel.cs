using PipettingCode.Common;
using PipettingCode.ViewModel;
using PipettingControl;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using Windows.Base;
using System.Windows;

namespace PipettingCode.Views
{
    public class PipettingViewModel : Singleton<PipettingViewModel>
    {
        //private static object _locker = new object();
        private static TCPUDP_Common mTCPUDP_Common = new TCPUDP_Common();
        // 默认阈值
        private int Threshold = 100;
        private List<List<int>> Pressure = new();
        // 打印信息函数
        private void DisplayMessage(string msg)
        {
            MySettingWindow.SaveLog(MySettingWindow.RunningLog, msg);
        }

        private Tcp_CommData CreateTcpCommData()
        {
            return new Tcp_CommData()
            {
                mIPAddress = Global_Parameter.IPAddress,// 1.加样臂网卡IP地址
                mIPport = Global_Parameter.IPPort,// 2.加样臂网卡使用的端口
                mSamplesize = PipettingParameter.TxtNumberOfStitches,//样本数量
            };
        }

        public Task<bool> MoveX(int x)
        {
            return Task.Run(() =>
            {
                var mCommData = CreateTcpCommData();
                mCommData.mMoveTargetX = x;
                var result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTakeNeedle_X_AxisPosition(mCommData);
                return result>0; 
            });
        }
        public Task<bool> MoveY(int y)
        {
            return Task.Run(() =>
            {
                var mCommData = CreateTcpCommData();
                mCommData.mMoveTargetY = y;             // 5.Y轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                var result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTakeNeedle_Y_AxisPosition(mCommData);                    //X轴移到取针位置坐标
                return result > 0;
            });
        }

        #region 0 连接网络

        #region 0.1 断开网络
        public void Pipetting_DisConnect(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            try
            {
                DisplayMessage("正在进行网络断开...");
                mCommData.InitBz = 0;                                                // 3.初始化标志: 0 节点匹配，建立预备执行组，X,Y,Z，泵初始化；1 Z轴初始化 2 X轴初始化 3 Y轴初始化  4 泵初始化
                result = mTCPUDP_Common.DisConnect();                     // 加样臂初始化
                if (result != 0)
                {
                    DisplayMessage("断开网络失败!");
                }
                else
                {
                    DisplayMessage("网络断开成功");
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
        }
        #endregion

        #region 0.2 连接网络，连接网络之前需要先断开网络
        public void Pipetting_Connect(int count)
        {
            bool result = false;
            Tcp_CommData mCommData = CreateTcpCommData();
            string s1 = "";
            try
            {
                DisplayMessage("正在进行网络连接...");
                mCommData.InitBz = 0;                                                // 3.初始化标志: 0 节点匹配，建立预备执行组，X,Y,Z，泵初始化；1 Z轴初始化 2 X轴初始化 3 Y轴初始化  4 泵初始化
                result = mTCPUDP_Common.client_Connect(mCommData.mIPAddress, mCommData.mIPport, mCommData.mBdIPAddress, mCommData.mBdIPport);                     // 加样臂初始化
                // 必须保证连接网络成功
                int i = 0;
                while (!result && (i++) < 3)
                {
                    if (App.Current == null)
                    {
                        return;
                    }
                    result = mTCPUDP_Common.client_Connect(mCommData.mIPAddress, mCommData.mIPport, mCommData.mBdIPAddress, mCommData.mBdIPport);                     // 加样臂初始化
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
        }
        #endregion

        #endregion

        #region 1 初始化

        #region 1.1 加样臂初始化
        public int Pipetting_Initial(int count)
        {
            this.Pipetting_DisConnect(count);
            this.Pipetting_Connect(count);

            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            string s1 = "";
            try
            {
                DisplayMessage("正在进行加样臂初始化..");
                mCommData.InitBz = 0;
                result = mTCPUDP_Common.cmd_SamplingarmInit(mCommData);                     // 加样臂初始化
                if (result != 0)
                {
                    switch (result)
                    {
                        case 1:
                            s1 = "节点匹配失败,匹配失败节点:" + mCommData.mFailNode;
                            break;
                        case 2:
                            s1 = "建立预备执行组失败";
                            break;
                        case 3:
                            s1 = "Z轴初始化失败";
                            break;
                        case 4:
                            s1 = "X轴初始化失败";
                            break;
                        case 5:
                            s1 = "Y轴移动失败";
                            break;
                        case 6:
                            s1 = "泵初始化失败";
                            break;
                    }
                    DisplayMessage("加样臂初始化失败," + s1);
                    App.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ExperimentErrorViewModel.GetInstance().ErrorMsg = "初始化失败";
                    }));
                }
                else
                {
                    DisplayMessage("加样臂初始化成功");
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 1.2 加样臂初始化(不脱针)
        public int Pipetting_InitialWithNeedle(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            string s1 = "";
            try
            {
                DisplayMessage("正在进行加样臂初始化..");
                mCommData.InitBz = 0;
                result = mTCPUDP_Common.cmd_SamplingarmInit_NotRemoval(mCommData);                     // 加样臂初始化
                if (result != 0)
                {
                    switch (result)
                    {
                        case 1:
                            s1 = "节点匹配失败,匹配失败节点:" + mCommData.mFailNode;
                            break;
                        case 2:
                            s1 = "建立预备执行组失败";
                            break;
                        case 3:
                            s1 = "Z轴初始化失败";
                            break;
                        case 4:
                            s1 = "X轴初始化失败";
                            break;
                        case 5:
                            s1 = "Y轴移动失败";
                            break;
                        case 6:
                            s1 = "泵初始化失败";
                            break;
                    }
                    DisplayMessage("加样臂初始化失败," + s1);
                }
                else
                {
                    DisplayMessage("加样臂初始化成功");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("初始化失败!" + ex.Message);
            }
            return result;
        }
        #endregion

        #endregion

        #region 2 取针

        #region 2.1 取针移动X轴
        private int TakeNeedle_MoveX(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                int NeedleXInternal = (Global_Parameter.NeedleEndX[0] - Global_Parameter.NeedleStartX[0]) / 11;         // 取针X间距

                // 根据count计算x和y的位置
                mCommData.mMoveTargetX = Global_Parameter.NeedleStartX[0] + (count / 2) * NeedleXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值

                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTakeNeedle_X_AxisPosition(mCommData);                    //X轴移到取针位置坐标
                if (result == 0)
                {
                    DisplayMessage("取针移动X轴成功");
                }
                else
                {
                    DisplayMessage("取针移动X轴失败");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTakeNeedle_X_AxisPosition(mCommData);                    //X轴移到取针位置坐标
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 2.2 取针移动Y轴
        private int TakeNeelde_MoveY(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                int NeedleYInternal = (Global_Parameter.NeedleEndY[0] - Global_Parameter.NeedleStartY[0]) / 7;         // 取针X间距

                mCommData.mMoveTargetY = Global_Parameter.NeedleStartY[0] + (count % 2) * 4 * NeedleYInternal;             // 5.Y轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTakeNeedle_Y_AxisPosition(mCommData);                    //X轴移到取针位置坐标

                if (result == 0)
                {
                    DisplayMessage("取针移动到Y轴成功");
                }
                else
                {
                    DisplayMessage("取针移动到Y轴失败");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTakeNeedle_Y_AxisPosition(mCommData);                    //X轴移到取针位置坐标
                    }
                }

            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 2.3 取针函数
        public int Pipetting_TakeNeedle(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            try
            {
                DisplayMessage("正在进行取针....");    // 通道数量，也就是样本数量

                int NeedleXInternal = (Global_Parameter.NeedleEndX[0] - Global_Parameter.NeedleStartX[0]) / 11;         // 取针X间距
                int NeedleYInternal = (Global_Parameter.NeedleEndY[0] - Global_Parameter.NeedleStartY[0]) / 7;          // 取针Y间距

                // 根据count计算x和y的位置
                mCommData.mMoveTargetX = Global_Parameter.NeedleStartX[0] + (count / 2) * NeedleXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                mCommData.mMoveTargetY = Global_Parameter.NeedleStartY[0] + (count % 2) * 4 * NeedleYInternal;             // 5.Y轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值

                // 根据是不是质控，质控传入0，非质控传入1
                //mCommData.mEnableFlag[0] = 1;
                //mCommData.mEnableFlag[1] = 1;
                //mCommData.mEnableFlag[2] = 1;
                //mCommData.mEnableFlag[3] = 1;

                for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                {
                    int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                    if (CheckViewModel.GetInstance().SelectedItems.Contains(index))
                    {
                        mCommData.mEnableFlag[i] = 0;
                    }
                }

                result = mTCPUDP_Common.cmd_TakeNeedle(mCommData);                          // 8针操作
                if (result == 0)
                {
                    DisplayMessage("取针成功");
                }
                else
                {
                    string msg = "";
                    for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                    {
                        int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                        if (mCommData.mTakeNeedleFlag[i] == 0 && !CheckViewModel.GetInstance().SelectedItems.Contains(index))
                        {
                            SampleViewModel.GetInstance().Sample96[index].Error = true;
                            msg += "第" + (i + 1).ToString() + "根针、";
                            SampleViewModel.GetInstance().Sample96[index].Error = true;
                            result = -2;
                        }
                    }
                    msg = msg.Substring(0, msg.Length - 1) + "没有取到针!";
                    bool retry = result != 0 && result != -1;

                    if (retry)
                    {
                        #region 报警
                        if (App.Current == null)
                        {
                            return -2;
                        }
                        App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 1);
                            ExperimentErrorViewModel.GetInstance().ErrorContext = msg;
                            new ExperimentError.ExperimentError().ShowDialog();
                            ExperimentErrorViewModel experimentErrorViewModel = ExperimentErrorViewModel.GetInstance();
                            switch (experimentErrorViewModel.ErrorSingle)
                            {
                                case ExperimentErrorViewModel.ErrorCode.ReTry:
                                    retry = true;
                                    break;
                                case ExperimentErrorViewModel.ErrorCode.Stop:
                                    retry = false;
                                    Stop();
                                    break;
                                // 忽略
                                case ExperimentErrorViewModel.ErrorCode.Ignore:
                                    retry = false;
                                    break;
                            }
                            PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 0);
                        }));
                        #endregion
                    }
                    while (retry)
                    {
                        if (App.Current == null)
                        {
                            return -2;
                        }
                        DisplayMessage("吸液失败..");

                        result = mTCPUDP_Common.cmd_TakeNeedle(mCommData);                      // 取针

                        msg = "";
                        // 大于阈值，就是吸上液了
                        for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                        {
                            int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                            if (mCommData.mTakeNeedleFlag[i] == 0 && CheckViewModel.GetInstance().SelectedItems.Contains(index))
                            {
                                result = -2;
                                msg += "第" + (i + 1).ToString() + "根针、";
                            }
                        }


                        retry = result != 0 && result != -1;
                        msg = msg.Substring(0, msg.Length - 1) + "没有吸到液体!";
                        if (retry)
                        {
                            #region 报警
                            if (App.Current == null)
                            {
                                return -2;
                            }
                            App.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 1);
                                ExperimentErrorViewModel.GetInstance().ErrorContext = msg;
                                new ExperimentError.ExperimentError().ShowDialog();
                                ExperimentErrorViewModel experimentErrorViewModel = ExperimentErrorViewModel.GetInstance();
                                switch (experimentErrorViewModel.ErrorSingle)
                                {
                                    case ExperimentErrorViewModel.ErrorCode.ReTry:
                                        retry = true;
                                        break;
                                    case ExperimentErrorViewModel.ErrorCode.Stop:
                                        retry = false;
                                        Stop();
                                        break;
                                    // 忽略
                                    case ExperimentErrorViewModel.ErrorCode.Ignore:
                                        retry = false;
                                        break;
                                }
                                PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 0);
                            }));
                            #endregion
                        }
                    }




                    //if (result != -1)
                    //{
                    //    DisplayMessage("取针失败");
                    //    // 第一次失败后再次尝试
                    //    result = mTCPUDP_Common.cmd_TakeNeedle(mCommData);                          // 8针
                    //}
                    //if (App.Current == null)
                    //{
                    //    return -1;
                    //}
                    //App.Current.Dispatcher.Invoke(new Action(() =>
                    //{
                    //    ExperimentErrorViewModel.GetInstance().ErrorMsg = "取针失败!";
                    //}));
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 2.4 取针
        public int TakeNeedle(int count)
        {
            Task<int> task1 = Task.Run(() =>
            {
                return TakeNeedle_MoveX(count);
            });
            Task<int> task2 = Task.Run(() =>
            {
                return TakeNeelde_MoveY(count);
            });

            Task.WaitAll(task1, task2);       // 两个动作完成后，才能继续下一个动作

            if (task1.Result == 0 && task2.Result == 0)
            {
                return Pipetting_TakeNeedle(count);
            }

            return -1;
        }
        #endregion

        #endregion
        #region 3 吸液

        #region 3.1 吸液移动X轴
        private int Imbibition_MoveX(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTuber(mCommData);                        //X轴移动开始合拢位置
                if (result == 0)
                {
                    DisplayMessage("吸液移动X轴成功");
                }
                else
                {
                    DisplayMessage("吸液移动X轴成功失败");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToTuber(mCommData);                        //X轴移动开始合拢位置
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 3.1.1 吸液移动X轴，体系构建
        private int Imbibition_MoveXBuild(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                //if (ShiJiViewModel.GetInstance().SelectedNumbers.StartsWith("96")) //TODO：TT 96孔
                {
                    int InjectionXInternal = (Global_Parameter.SampleEndX[0] - Global_Parameter.SampleStartX[0]) / 11;      // 注液X间距
                    mCommData.mMoveTargetX = Global_Parameter.SampleStartX[0] + (count / 2) * InjectionXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                }
                //else
                //{
                //    int index = count / 4;
                //    int InjectionXInternal = (Global_Parameter.SampleEndX[index] - Global_Parameter.SampleStartX[index]) / 11;      // 注液X间距
                //    mCommData.mMoveTargetX = Global_Parameter.SampleStartX[index] + ((count % 4) / 2) * 6 * InjectionXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                //}

                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_X_AxisPosition(mCommData);                        //X轴移动开始合拢位置
                if (result == 0)
                {
                    DisplayMessage("吸液移动X轴成功");
                }
                else
                {
                    DisplayMessage("吸液移动X轴成功失败");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_X_AxisPosition(mCommData);                        //X轴移动开始合拢位置
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 3.2 吸液移动Y轴
        public int Imbibition_MoveY(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            try
            {
                DisplayMessage("正在进行取针..");


                result = mTCPUDP_Common.cmd_OpenYaxis(mCommData);                          // 8针操作
                if (result == 0)
                {
                    DisplayMessage("Y轴张开成功!");
                }
                else
                {
                    DisplayMessage("Y轴张开失败!");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_OpenYaxis(mCommData);                   // 再试一次
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 3.2.1 吸液移动Y轴，体系构建
        public int Imbibition_MoveYBuild(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            try
            {
                DisplayMessage("正在进行取针..");


                //if (ShiJiViewModel.GetInstance().SelectedNumbers.StartsWith("96"))//TODO：TT 96孔
                {
                    int InjectionYInternal = (Global_Parameter.SampleEndY[0] - Global_Parameter.SampleStartY[0]) / 7;       // 注液Y间距
                    mCommData.mMoveTargetY = Global_Parameter.SampleStartY[0] + (count % 2) * InjectionYInternal;               //4.Y轴坐标，动1态传递
                }
                //else
                //{
                //    int index = count / 4;
                //    int InjectionYInternal = (Global_Parameter.SampleEndY[index] - Global_Parameter.SampleStartY[index]) / 7;       // 注液Y间距
                //    mCommData.mMoveTargetY = Global_Parameter.SampleStartY[index] + (count % 2) * InjectionYInternal;               //4.Y轴坐标，动1态传递
                //}

                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_Y_AxisClosure(mCommData);                          // 8针操作
                if (result == 0)
                {
                    DisplayMessage("Y轴张开成功!");
                }
                else
                {
                    DisplayMessage("Y轴张开失败!");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_OpenYaxis(mCommData);                   // 再试一次
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 3.3 多通道同时吸液
        public int Pipetting_Imbibition(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            Class_ImbibitionParameter mParameter = new Class_ImbibitionParameter();
            try
            {
                DisplayMessage("吸液操作开始..");
                // 4. 液面探测
                if (PipettingParameter.ChkLiquidLevelDetection)
                {
                    mParameter.mLiquidLevelDetectionFlag = true;
                }
                else
                {
                    mParameter.mLiquidLevelDetectionFlag = false;
                }
                mParameter.mLiquidlevelfollowingFlag = false;          //3.样本数量; 
                mParameter.mSamplesize = PipettingParameter.TxtNumberOfStitches;          //5.样本数量; 

                // 如果没有设置，取吸液界面设置的参数
                mParameter.mSuctionHeight = 1000;
                // 10混 TODO：TT 多少混
                //if (ShiGuanViewModel.GetInstance().SelectedType == "10混" || ShiGuanViewModel.GetInstance().SelectedType == "单混")
                //{
                //    mParameter.mSuctionHeight = PipettingParameter.TxtImmersionDepth10;            //6.吸液高度
                //}
                //else if (ShiGuanViewModel.GetInstance().SelectedType == "20混")
                //{
                //    mParameter.mSuctionHeight = PipettingParameter.TxtImmersionDepth20;            //6.吸液高度
                //}
                mParameter.mTipGap = PipettingParameter.TxtTipClearance;              //7.30针尖空气间隙(μl):
                mParameter.mNeedleTailGap = PipettingParameter.TxtNeedleTailGap;             //8.50//针尾空气间隙(μl)
                mParameter.mSuctionSpeed = PipettingParameter.MtxtVolumeSpeed;              //9.1000//吸液速度(μl/s):
                mParameter.mSuctionAccSpeed = PipettingParameter.MtxtVolumeAccSpeed;           //10.700//吸液加速度(μl/s^2):
                mParameter.mImmersionDepth = PipettingParameter.MtxtImmersionDepth;           //11.20//浸没深度(步):
                mParameter.mLiquidabsorption = PipettingParameter.TxtSuctionVolume;             //12.吸液量
                mParameter.mRegurgitationvolume = PipettingParameter.TxtRegurgitation;             //13.回吐量
                mParameter.mRetrusionHeight = PipettingParameter.TxtRetrusionHeight;           //14.回吐高度
                mParameter.mHeightLimited = PipettingParameter.MtxtHeightLimited;            //15.液面探测的限制高度

                // 单混液面探测高度
                // TODO：TT 单混
                //if (ShiGuanViewModel.GetInstance().SelectedType == "单混")
                //{
                //    mParameter.mLiquidLevelDetectionHeight = PipettingParameter.LiquildDectectionHeight01;                     //16.液面探测高度
                //}
                //else           // 10混、20混液面探测高度
                {
                    mParameter.mLiquidLevelDetectionHeight = PipettingParameter.LiquildDectectionHeight10;                     //16.液面探测高度
                }

                mParameter.mLiquidLevelDetectionSpeed = PipettingParameter.TextBox3;                     //17.液面探测速度
                mParameter.mSuctionDelay = PipettingParameter.TxtDelay;                     //18.吸液延时
                mParameter.mAmountOfSuction = PipettingParameter.TxtDesorptionL;                        // 19.回吸量


                for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                {
                    int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                    if (CheckViewModel.GetInstance().SelectedItems.Contains(index))
                    {
                        mCommData.mEnableFlag[i] = 0;
                    }
                }

                #region 检测是否吸到液
                this.Pressure = new();
                int time = 7000;
                Task task = Task.Run(() =>
                {
                    GetMaxPressure(time);
                });
                #endregion

                result = mTCPUDP_Common.cmd_Imbibition_All(mCommData, mParameter);                      //多通道同时吸液

                task.Wait();
                HashSet<int> HasLiquild = new();          // 那根针吸到液体了

                int[] diff = GetMaxDiff(count);

                string msg = "";
                // 大于阈值，就是吸上液了
                for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                {
                    // 差值不满足要求
                    if (Math.Abs(diff[i]) < this.Threshold + PipettingParameter.ThresholdPress[i])
                    {
                        int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                        if (CheckViewModel.GetInstance().SelectedItems.Contains(index))
                        {
                            HasLiquild.Add(i);             // 质控默认吸液成功
                            continue;
                        }
                        SampleViewModel.GetInstance().Sample96[index].Error = true;
                        msg += "第" + (i + 1).ToString() + "根针、";
                        result = -2;
                    }
                    else
                    {
                        HasLiquild.Add(i);
                    }
                }

                if (result == 0)
                {
                    DisplayMessage("吸液成功..");
                }
                else
                {
                    if (msg.Length > 20)
                    {
                        msg = msg.Substring(0, 19) + "\n" + msg.Substring(20, msg.Length - 20);
                    }
                    msg = msg.Substring(0, msg.Length - 1) + "没有吸到液体!";
                    bool retry = result != 0 && result != -1;
                    if (retry)
                    {
                        #region 报警
                        if (App.Current == null)
                        {
                            return -2;
                        }
                        App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 1);
                            ExperimentErrorViewModel.GetInstance().ErrorMsg = msg;
                            //new ImbibitionErrorWindow().ShowDialog(); // TODO：TT 吸液失败提示
                            ExperimentErrorViewModel experimentErrorViewModel = ExperimentErrorViewModel.GetInstance();
                            switch (experimentErrorViewModel.ErrorSingle)
                            {
                                case ExperimentErrorViewModel.ErrorCode.ReTry:
                                    retry = true;
                                    break;
                                case ExperimentErrorViewModel.ErrorCode.Stop:
                                    retry = false;
                                    Stop();
                                    break;
                                // 忽略
                                case ExperimentErrorViewModel.ErrorCode.Ignore:
                                    retry = false;
                                    break;
                            }
                            PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 0);
                        }));
                        #endregion
                    }
                    while (retry)
                    {
                        if (App.Current == null)
                        {
                            return -2;
                        }
                        DisplayMessage("吸液失败..");

                        mCommData.mEnableFlag[0] = 0;          // 1针
                        mCommData.mEnableFlag[1] = 0;          // 2针
                        mCommData.mEnableFlag[2] = 0;          // 3针
                        mCommData.mEnableFlag[3] = 0;          // 4针

                        for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                        {
                            if (Math.Abs(diff[i]) < this.Threshold + PipettingParameter.ThresholdPress[i] && !HasLiquild.Contains(i))
                            {
                                mCommData.mEnableFlag[i] = 1;
                            }
                        }

                        #region 检测是否吸到液
                        task = Task.Run(() =>
                        {
                            GetMaxPressure(time);
                        });

                        result = mTCPUDP_Common.cmd_Imbibition_All(mCommData, mParameter);                      //多通道同时吸液
                        task.Wait();       // 等待获取气压数据完成

                        diff = GetMaxDiff(count);

                        msg = "";
                        // 大于阈值，就是吸上液了
                        for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                        {
                            if (Math.Abs(diff[i]) < this.Threshold + PipettingParameter.ThresholdPress[i] && !HasLiquild.Contains(i))
                            {
                                result = -2;
                                msg += "第" + (i + 1).ToString() + "根针、";
                            }
                            else
                            {
                                HasLiquild.Add(i);
                            }
                            if (msg.Length >= 20)
                            {
                                msg += "\n";
                            }
                        }
                        #endregion

                        retry = result != 0 && result != -1;
                        msg = msg.Substring(0, msg.Length - 1) + "没有吸到液体!";
                        if (retry)
                        {
                            #region 报警
                            if (App.Current == null)
                            {
                                return -2;
                            }
                            App.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 1);
                                ExperimentErrorViewModel.GetInstance().ErrorMsg = msg;
                                //new ImbibitionErrorWindow().ShowDialog(); // TODO：TT 吸液失败提示
                                ExperimentErrorViewModel experimentErrorViewModel = ExperimentErrorViewModel.GetInstance();
                                switch (experimentErrorViewModel.ErrorSingle)
                                {
                                    case ExperimentErrorViewModel.ErrorCode.ReTry:
                                        retry = true;
                                        break;
                                    case ExperimentErrorViewModel.ErrorCode.Stop:
                                        retry = false;
                                        Stop();
                                        break;
                                    // 忽略
                                    case ExperimentErrorViewModel.ErrorCode.Ignore:
                                        retry = false;
                                        break;
                                }
                                PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 0);
                            }));
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 3.3.1 吸液，体系构建
        public int Pipetting_ImbibitionBuild(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            Class_ImbibitionParameter mParameter = new Class_ImbibitionParameter();
            try
            {
                DisplayMessage("吸液操作开始..");
                // 4. 液面探测
                if (PipettingParameterBuild.ChkLiquidLevelDetection)
                {
                    mParameter.mLiquidLevelDetectionFlag = true;
                }
                else
                {
                    mParameter.mLiquidLevelDetectionFlag = false;
                }
                mParameter.mLiquidlevelfollowingFlag = false;

                mCommData.mSamplesize = PipettingParameterBuild.TxtNumberOfStitches;          //3.样本数量; 
                mParameter.mSamplesize = PipettingParameterBuild.TxtNumberOfStitches;          //5.样本数量; 

                // 如果没有设置，取吸液界面设置的参数
                mParameter.mSuctionHeight = 700;
                // 10混 // TODO：TT 单混
                //if (ShiGuanViewModel.GetInstance().SelectedType == "10混" || ShiGuanViewModel.GetInstance().SelectedType == "单混")
                //{
                //    mParameter.mSuctionHeight = PipettingParameterBuild.TxtImmersionDepth10;            //6.吸液高度
                //}
                //else if (ShiGuanViewModel.GetInstance().SelectedType == "20混")
                //{
                //    mParameter.mSuctionHeight = PipettingParameterBuild.TxtImmersionDepth20;            //6.吸液高度
                //}
                mParameter.mTipGap = PipettingParameterBuild.TxtTipClearance;              //7.30针尖空气间隙(μl):
                mParameter.mNeedleTailGap = PipettingParameterBuild.TxtNeedleTailGap;             //8.50//针尾空气间隙(μl)
                mParameter.mSuctionSpeed = PipettingParameterBuild.MtxtVolumeSpeed;              //9.1000//吸液速度(μl/s):
                mParameter.mSuctionAccSpeed = PipettingParameterBuild.MtxtVolumeAccSpeed;           //10.700//吸液加速度(μl/s^2):
                mParameter.mImmersionDepth = PipettingParameterBuild.MtxtImmersionDepth;           //11.20//浸没深度(步):
                mParameter.mLiquidabsorption = PipettingParameterBuild.TxtSuctionVolume;             //12.吸液量
                mParameter.mRegurgitationvolume = PipettingParameterBuild.TxtRegurgitation;             //13.回吐量
                mParameter.mRetrusionHeight = PipettingParameterBuild.TxtRetrusionHeight;           //14.回吐高度
                mParameter.mHeightLimited = PipettingParameterBuild.MtxtHeightLimited;            //15.液面探测的限制高度
                mParameter.mLiquidLevelDetectionHeight = PipettingParameterBuild.LiquildDectectionHeight10;                     //16.液面探测高度
                mParameter.mLiquidLevelDetectionSpeed = PipettingParameterBuild.TextBox3;                     //17.液面探测速度
                mParameter.mSuctionDelay = PipettingParameterBuild.TxtDelay;                     //18.吸液延时
                mParameter.mAmountOfSuction = PipettingParameterBuild.TxtDesorptionL;                        // 19.回吸量


                for (int i = 0; i < PipettingParameterBuild.TxtNumberOfStitches; ++i)
                {
                    int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                    if (CheckViewModel.GetInstance().SelectedItems.Contains(index))
                    {
                        mCommData.mEnableFlag[i] = 0;
                    }
                }

                #region 检测是否吸到液
                this.Pressure = new();
                int time = 7000;
                Task task = Task.Run(() =>
                {
                    GetMaxPressure(time);
                });
                #endregion

                result = mTCPUDP_Common.cmd_Imbibition_All(mCommData, mParameter);                      //多通道同时吸液

                task.Wait();       // 等待获取气压数据完成
                HashSet<int> HasLiquild = new();

                int[] diff = GetMaxDiff(count);

                string msg = "";
                // 大于阈值，就是吸上液了
                for (int i = 0; i < PipettingParameterBuild.TxtNumberOfStitches; ++i)
                {
                    // 差值不满足要求
                    if (Math.Abs(diff[i]) < this.Threshold + PipettingParameterBuild.ThresholdPress[i])
                    {
                        int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                        if (CheckViewModel.GetInstance().SelectedItems.Contains(index))
                        {
                            HasLiquild.Add(i);           // 质控默认吸上液了
                            continue;
                        }
                        SampleViewModel.GetInstance().Sample96[index].Error = true;
                        msg += "第" + (i + 1).ToString() + "根针、";
                        result = -2;
                    }
                    else
                    {
                        HasLiquild.Add(i);
                    }
                }
                if (result == 0)
                {
                    DisplayMessage("吸液成功..");
                }
                else
                {
                    if (msg.Length > 20)
                    {
                        msg = msg.Substring(0, 19) + "\n" + msg.Substring(20, msg.Length - 20);
                    }
                    msg = msg.Substring(0, msg.Length - 1) + "没有吸到液体!";
                    bool retry = result != 0 && result != -1;
                    if (retry)
                    {
                        #region 报警
                        if (App.Current == null)
                        {
                            return -2;
                        }
                        App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 1);
                            ExperimentErrorViewModel.GetInstance().ErrorMsg = msg;
                            //new ImbibitionErrorWindow().ShowDialog(); // TODO：TT 吸液失败提示
                            ExperimentErrorViewModel experimentErrorViewModel = ExperimentErrorViewModel.GetInstance();
                            switch (experimentErrorViewModel.ErrorSingle)
                            {
                                case ExperimentErrorViewModel.ErrorCode.ReTry:
                                    retry = true;
                                    break;
                                case ExperimentErrorViewModel.ErrorCode.Stop:
                                    retry = false;
                                    Stop();
                                    break;
                                // 忽略
                                case ExperimentErrorViewModel.ErrorCode.Ignore:
                                    retry = false;
                                    break;
                            }
                            PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 0);
                        }));
                        #endregion
                    }
                    while (retry)
                    {
                        if (App.Current == null)
                        {
                            return -2;
                        }
                        DisplayMessage("吸液失败..");

                        mCommData.mEnableFlag[0] = 0;          // 1针
                        mCommData.mEnableFlag[1] = 0;          // 2针
                        mCommData.mEnableFlag[2] = 0;          // 3针
                        mCommData.mEnableFlag[3] = 0;          // 4针

                        for (int i = 0; i < PipettingParameterBuild.TxtNumberOfStitches; ++i)
                        {
                            if (Math.Abs(diff[i]) < this.Threshold + PipettingParameterBuild.ThresholdPress[i] && !HasLiquild.Contains(i))
                            {
                                mCommData.mEnableFlag[i] = 1;
                            }
                        }

                        #region 检测是否吸到液
                        this.Pressure = new();
                        time = 7000;
                        task = Task.Run(() =>
                        {
                            GetMaxPressure(time);
                        });
                        #endregion

                        result = mTCPUDP_Common.cmd_Imbibition_All(mCommData, mParameter);                      //多通道同时吸液
                        task.Wait();       // 等待获取气压数据完成

                        #region 检查是否吸到液

                        diff = GetMaxDiff(count);

                        msg = "";
                        // 大于阈值，就是吸上液了
                        for (int i = 0; i < PipettingParameterBuild.TxtNumberOfStitches; ++i)
                        {
                            if (Math.Abs(diff[i]) < this.Threshold + PipettingParameterBuild.ThresholdPress[i] && !HasLiquild.Contains(i))
                            {
                                result = -2;
                                msg += "第" + (i + 1).ToString() + "根针、";
                            }
                            else
                            {
                                HasLiquild.Add(i);
                            }
                            if (msg.Length >= 20)
                            {
                                msg += "\n";
                            }
                        }
                        #endregion

                        retry = result != 0 && result != -1;
                        msg = msg.Substring(0, msg.Length - 1) + "没有吸到液体!";
                        if (retry)
                        {
                            #region 报警
                            if (App.Current == null)
                            {
                                return -2;
                            }
                            App.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 1);
                                ExperimentErrorViewModel.GetInstance().ErrorMsg = msg;
                                //new ImbibitionErrorWindow().ShowDialog(); // TODO：TT 吸液失败提示
                                ExperimentErrorViewModel experimentErrorViewModel = ExperimentErrorViewModel.GetInstance();
                                switch (experimentErrorViewModel.ErrorSingle)
                                {
                                    case ExperimentErrorViewModel.ErrorCode.ReTry:
                                        retry = true;
                                        break;
                                    case ExperimentErrorViewModel.ErrorCode.Stop:
                                        retry = false;
                                        Stop();
                                        break;
                                    // 忽略
                                    case ExperimentErrorViewModel.ErrorCode.Ignore:
                                        retry = false;
                                        break;
                                }
                                PLCViewModel.GetInstance().PLC_SendCommand_Write("Y19", 0);
                            }));
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 3.4 吸液
        public int Imbibition(int count)
        {
            Task<int> task1, task2;
            if (MainWindowViewModel.GetInstance().TabIndex == 0)
            {
                task1 = Task.Run(() =>
                {
                    return Imbibition_MoveX(count);
                });

                task2 = Task.Run(() =>
                {
                    return Imbibition_MoveY(count);
                });
            }
            else
            {
                task1 = Task.Run(() =>
                {
                    return Imbibition_MoveXBuild(count);
                });

                task2 = Task.Run(() =>
                {
                    return Imbibition_MoveYBuild(count);
                });
            }

            Task.WaitAll(task1, task2);     // 两个动作完成后，才能继续下一个动作

            if (task1.Result == 0 && task2.Result == 0)
            {
                if (MainWindowViewModel.GetInstance().TabIndex == 0)
                {
                    return Pipetting_Imbibition(count);
                }
                else
                {
                    return Pipetting_ImbibitionBuild(count);
                }
            }

            return -1;
        }
        #endregion

        #endregion

        #region 4 注液

        #region 4.1 注液移动X轴
        private int Injection_MoveX(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                //TODO：TT  96
                //if (ShiJiViewModel.GetInstance().SelectedNumbers.StartsWith("96"))
                //{
                int InjectionXInternal = (Global_Parameter.SampleEndX[0] - Global_Parameter.SampleStartX[0]) / 11;      // 注液X间距
                mCommData.mMoveTargetX = Global_Parameter.SampleStartX[0] + (count / 2) * InjectionXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                //}
                //else
                //{
                //    int index = count / 4;
                //    int InjectionXInternal = (Global_Parameter.SampleEndX[index] - Global_Parameter.SampleStartX[index]) / 11;      // 注液X间距
                //    mCommData.mMoveTargetX = Global_Parameter.SampleStartX[index] + ((count % 4) / 2) * 6 * InjectionXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                //}


                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_X_AxisPosition(mCommData);     // 移动X到加样位置

                if (result == 0)
                {
                    DisplayMessage("注液移动X轴成功");
                }
                else
                {
                    DisplayMessage("注液移动X轴失败");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_X_AxisPosition(mCommData);     // 移动X到加样位置
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 4.1.1 注液移动X轴，体系构建
        private int Injection_MoveXBuild(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                mCommData.mSamplesize = PipettingParameterBuild.TxtNumberOfStitches;

                int InjectionXInternal = (Global_Parameter.SystemInjectionEndX - Global_Parameter.SystemInjectionStartX) / 11;      // 注液X间距
                mCommData.mMoveTargetX = Global_Parameter.SystemInjectionStartX + (count / 2) * InjectionXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值

                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_X_AxisPosition(mCommData);     // 移动X到加样位置

                if (result == 0)
                {
                    DisplayMessage("注液移动X轴成功");
                }
                else
                {
                    DisplayMessage("注液移动X轴失败");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_X_AxisPosition(mCommData);     // 移动X到加样位置
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 4.2 注液移动Y轴
        private int Injection_MoveY(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                //TODO：TT  96
                //if (ShiJiViewModel.GetInstance().SelectedNumbers.StartsWith("96"))
                //{
                int InjectionYInternal = (Global_Parameter.SampleEndY[0] - Global_Parameter.SampleStartY[0]) / 7;       // 注液Y间距
                mCommData.mMoveTargetY = Global_Parameter.SampleStartY[0] + (count % 2) * InjectionYInternal;               //4.Y轴坐标，动1态传递
                //}
                //else
                //{
                //    int index = count / 4;
                //    int InjectionYInternal = (Global_Parameter.SampleEndY[index] - Global_Parameter.SampleStartY[index]) / 7;       // 注液Y间距
                //    mCommData.mMoveTargetY = Global_Parameter.SampleStartY[index] + (count % 2) * InjectionYInternal;               //4.Y轴坐标，动1态传递
                //}

                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_Y_AxisClosure(mCommData);
                if (result != 0)
                {
                    result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_Y_AxisClosure(mCommData);
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 4.2.1 注液移动Y轴，体系构建
        private int Injection_MoveYBuild(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                int InjectionYInternal = (Global_Parameter.SystemInjectionEndY - Global_Parameter.SystemInjectionStartY) / 7;       // 注液Y间距
                mCommData.mMoveTargetY = Global_Parameter.SystemInjectionStartY + (count % 2) * InjectionYInternal;               //4.Y轴坐标，动1态传递

                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_Y_AxisClosure(mCommData);
                if (result != 0)
                {
                    result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToSample_Y_AxisClosure(mCommData);
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 4.3 多通道同时注液
        public int Pipetting_Injection(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            Class_ImbibitionParameter mParameter = new Class_ImbibitionParameter();
            try
            {
                DisplayMessage("注液开始..");
                //int InjectionXInternal = (Global_Parameter.SampleEndX[0] - Global_Parameter.SampleStartX[0]) / 11;      // 注液X间距
                //int InjectionYInternal = (Global_Parameter.SampleEndY[0] - Global_Parameter.SampleStartY[0]) / 7;       // 注液Y间距

                //TODO：TT  96
                //if (ShiJiViewModel.GetInstance().SelectedNumbers.StartsWith("96"))
                //{
                int InjectionXInternal = (Global_Parameter.SampleEndX[0] - Global_Parameter.SampleStartX[0]) / 11;      // 注液X间距
                mCommData.mMoveTargetX = Global_Parameter.SampleStartX[0] + (count / 2) * InjectionXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值

                int InjectionYInternal = (Global_Parameter.SampleEndY[0] - Global_Parameter.SampleStartY[0]) / 7;       // 注液Y间距
                mCommData.mMoveTargetY = Global_Parameter.SampleStartY[0] + (count % 2) * InjectionYInternal;
                //}
                //else
                //{
                //    int index = count / 4;
                //    int InjectionXInternal = (Global_Parameter.SampleEndX[index] - Global_Parameter.SampleStartX[index]) / 11;      // 注液X间距
                //    mCommData.mMoveTargetX = Global_Parameter.SampleStartX[index] + ((count % 4) / 2) * 6 * InjectionXInternal;             // 4.X轴坐标是动态传递的，根据实际实验取针位置计算，然后赋值
                //    int InjectionYInternal = (Global_Parameter.SampleEndY[index] - Global_Parameter.SampleStartY[index]) / 7;       // 注液Y间距
                //    mCommData.mMoveTargetY = Global_Parameter.SampleStartY[index] + (count % 2) * InjectionYInternal;
                //}          //5.样本数量; 
                mParameter.mSamplesize = PipettingParameter.TxtNumberOfStitches;          //6.样本数量; 
                mParameter.mTipGap = PipettingParameter.TxtTipClearance;              //7.30针尖空气间隙(μl):
                mParameter.mNeedleTailGap = PipettingParameter.TxtNeedleTailGap;             //8.50//针尾空气间隙(μl)
                mParameter.mInjectionPumpSpace = PipettingParameter.MInjectionPumpSpace;          //9.注液量
                mParameter.mInjectionPumpSpeed = PipettingParameter.MInjectionPumpSpeed;          //10.注液速度
                mParameter.mInjectionPumpAccSpeed = PipettingParameter.MInjectionPumpAccSpeed;       //11.注液加速度(μl/s^2):
                mParameter.mInjectionHeight = PipettingParameter.MInjectionHeight;             //12.注液高度(步):
                mParameter.mInjectionZSpace = PipettingParameter.MInjectionZSpace;             //13.注液完Z轴提起的高度(步):
                mParameter.m_Liquidinjectiontimes = PipettingParameter.TextBox5;                     //14.注液次数



                for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                {
                    int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                    if (CheckViewModel.GetInstance().SelectedItems.Contains(index))
                    {
                        mCommData.mEnableFlag[i] = 0;
                    }
                }

                result = mTCPUDP_Common.cmd_Injection_All(mCommData, mParameter);                       //多通道同时注液
                if (result == 0)
                {
                    DisplayMessage("注液成功..");
                }
                else
                {
                    DisplayMessage("注液失败..");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_Injection_All(mCommData, mParameter);                       //再来一次多通道同时注液    
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 4.3.1 注液，体系构建
        public int Pipetting_InjectionBuild(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            Class_ImbibitionParameter mParameter = new Class_ImbibitionParameter();
            try
            {
                DisplayMessage("注液开始..");
                int InjectionXInternal = (Global_Parameter.SystemInjectionEndX - Global_Parameter.SystemInjectionStartX) / 11;      // 注液X间距
                mCommData.mMoveTargetX = Global_Parameter.SystemInjectionStartX + (count / 2) * InjectionXInternal;             // 4.X轴坐标是动
                                                                                                                                // 态传递的，根据实际实验取针位置计算，然后赋值
                int InjectionYInternal = (Global_Parameter.SystemInjectionEndY - Global_Parameter.SystemInjectionStartY) / 7;       // 注液Y间距
                mCommData.mMoveTargetY = Global_Parameter.SystemInjectionStartY + (count % 2) * InjectionYInternal;               //4.Y轴坐标，动1态传递


                mCommData.mSamplesize = PipettingParameterBuild.TxtNumberOfStitches;          //5.样本数量; 
                mParameter.mSamplesize = PipettingParameterBuild.TxtNumberOfStitches;          //6.样本数量; 
                mParameter.mTipGap = PipettingParameterBuild.TxtTipClearance;              //7.30针尖空气间隙(μl):
                mParameter.mNeedleTailGap = PipettingParameterBuild.TxtNeedleTailGap;             //8.50//针尾空气间隙(μl)
                mParameter.mInjectionPumpSpace = PipettingParameterBuild.MInjectionPumpSpace;          //9.注液量
                mParameter.mInjectionPumpSpeed = PipettingParameterBuild.MInjectionPumpSpeed;          //10.注液速度
                mParameter.mInjectionPumpAccSpeed = PipettingParameterBuild.MInjectionPumpAccSpeed;       //11.注液加速度(μl/s^2):
                mParameter.mInjectionHeight = PipettingParameterBuild.MInjectionHeight;             //12.注液高度(步):
                mParameter.mInjectionZSpace = PipettingParameterBuild.MInjectionZSpace;             //13.注液完Z轴提起的高度(步):
                mParameter.m_Liquidinjectiontimes = PipettingParameterBuild.TextBox5;                     //14.注液次数



                for (int i = 0; i < PipettingParameterBuild.TxtNumberOfStitches; ++i)
                {
                    int index = (count / 2 * 2) * 4 + count % 2 + (i % 4) * 2;
                    if (CheckViewModel.GetInstance().SelectedItems.Contains(index))
                    {
                        mCommData.mEnableFlag[i] = 0;
                    }
                }

                result = mTCPUDP_Common.cmd_Injection_All(mCommData, mParameter);                       //多通道同时注液
                if (result == 0)
                {
                    DisplayMessage("注液成功..");
                }
                else
                {
                    DisplayMessage("注液失败..");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_Injection_All(mCommData, mParameter);                       //再来一次多通道同时注液    
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 4.4 注液
        public int Injection(int count)
        {
            Task<int> task1, task2;
            if (MainWindowViewModel.GetInstance().TabIndex == 0)
            {
                task1 = Task.Run(() =>
                {
                    return Injection_MoveX(count);
                });
                task2 = Task.Run(() =>
                {
                    return Injection_MoveY(count);
                });
            }
            else
            {
                task1 = Task.Run(() =>
                {
                    return Injection_MoveXBuild(count);
                });
                task2 = Task.Run(() =>
                {
                    return Injection_MoveYBuild(count);
                });
            }

            Task.WaitAll(task1, task2);        // 两个动作完成后，才能继续下一个动作

            if (task1.Result == 0 && task2.Result == 0)
            {
                if (MainWindowViewModel.GetInstance().TabIndex == 0)
                {
                    return Pipetting_Injection(count);
                }
                else
                {
                    return Pipetting_InjectionBuild(count);
                }
            }

            return -1;
        }
        #endregion

        #endregion

        #region 5 脱针

        #region 5.1 脱针移动X轴
        private int OffNeedle_MoveX(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToNeedleRemoval_X_AxisPosition(mCommData);                    //X轴移到脱针位置坐标
                if (result == 0)
                {
                    DisplayMessage("脱针移动X轴成功");
                }
                else
                {
                    DisplayMessage("脱针移动X轴失败");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToNeedleRemoval_X_AxisPosition(mCommData);                    //X轴移到脱针位置坐标
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
            return result;
        }
        #endregion

        #region 5.2 脱针移动Y轴
        private int OffNeedle_MoveY(int count)
        {
            Tcp_CommData mCommData = CreateTcpCommData();
            int result = -1;
            try
            {
                result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToNeedleRemoval_Y_AxisPosition(mCommData);                    //X轴移到脱针位置坐标
                if (result == 0)
                {
                    DisplayMessage("脱针移动Y轴成功");
                }
                else
                {
                    DisplayMessage("脱针移动Y轴成功");
                    if (result != -1)
                    {
                        result = mTCPUDP_Common.cmd_SamplingarmPointtopointmotionToNeedleRemoval_Y_AxisPosition(mCommData);                    //X轴移到脱针位置坐标
                    }
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 5.3 脱针函数
        public int Pipetting_OffNeedle(int count)
        {
            int result = -1;
            Tcp_CommData mCommData = CreateTcpCommData();
            try
            {
                DisplayMessage("正在进行脱针..");


                result = mTCPUDP_Common.cmd_NeedleRemoval(mCommData);                       //8针操作
                if (result == 0)
                {
                    DisplayMessage("脱针成功");
                }
                else
                {
                    DisplayMessage("脱针失败");
                    if (result != -1)
                    {
                        // 脱针失败，再次脱针
                        result = mTCPUDP_Common.cmd_NeedleRemoval(mCommData);
                    }
                    if (App.Current == null)
                    {
                        return -1;
                    }
                    App.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ExperimentErrorViewModel.GetInstance().ErrorMsg = "脱针失败!";
                    }));
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region 5.4 脱针
        public int OffNeedle(int count)
        {
            Task<int> task1 = Task.Run(() => OffNeedle_MoveX(count));
            Task<int> task2 = Task.Run(() => OffNeedle_MoveY(count));

            Task.WaitAll(task1, task2);           // 两个动作完成后，才能继续下一个动作

            if (task1.Result == 0 && task2.Result == 0)
            {
                return Pipetting_OffNeedle(count);

            }
            return -1;
        }
        #endregion

        #endregion

        #region 获取当前距离
        public int GetDistance()
        {
            Tcp_CommData mCommData = CreateTcpCommData();

            if (mTCPUDP_Common.cmd_ReturnXaxisLocation(mCommData) >= 0)
            {
                return mCommData.mCurrentlocation[0];
            }
            return int.MaxValue;
        }
        #endregion

        #region 吸液的时候获取压强
        private int GetMaxPressure(int time)
        {
            int span = 150;
            int n = time / span;

            Tcp_CommData mCommData = CreateTcpCommData();

            for (int i = 0; i < n; ++i)
            {
                int result = mTCPUDP_Common.cmd_Pump_Get_AllPumpAdc_Zdtd(mCommData);                // 调用谢工提供的api，获取当前每根针的压强
                List<int> tmp = new();
                if (result == 0)
                {
                    foreach (int num in mCommData.mPumpAdc)
                    {
                        tmp.Add(num);
                    }
                    this.Pressure.Add(tmp);
                }
                Thread.Sleep(span);         // 每隔span毫秒获取一次
            }
            return 0;
        }
        #endregion

        #region 断开另一边的行动
        public static void ShutDownPipetting()
        {
            mTCPUDP_Common.ReceiveMsg_Flag = true;   // 退出线程
        }

        #endregion

        #region 开始另一边的行动
        public static void ResetPipetting()
        {
            mTCPUDP_Common.ReceiveMsg_Flag = false;   // 设置线程标志
        }
        #endregion

        #region 软急停
        private void Stop()
        {
            PLCViewModel.GetInstance().PLC_SendCommand_Write("R11B", 1);
            if (App.Current == null)
            {
                return;
            }
            App.Current.Dispatcher.Invoke(new Action(() =>
            {
                MainWindowViewModel.GetInstance().KillExperiment = true;
                MainWindowViewModel.GetInstance().IsConnect = false;
                PipettingViewModel.ShutDownPipetting();       // 停止移液臂方面的动作
            }));
        }
        #endregion

        private int[] GetMaxDiff(int count)
        {
            #region 检查是否吸到液

            int[] diff = new int[PipettingParameter.TxtNumberOfStitches];

            #region 如果目录不存在，创建目录
            string dir = MySettingWindow.PRESSDIR;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            #endregion

            string fileName = dir + "\\" + "test" + (count + 1).ToString() + ".csv";
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

                int k = 0;
                foreach (List<int> tmp in this.Pressure)
                {
                    string s = "";
                    for (int i = 0; i < PipettingParameter.TxtNumberOfStitches; ++i)
                    {
                        s += tmp[i] + ",";
                        if (k > 0)
                        {
                            diff[i] = Math.Max(diff[i], Math.Abs(this.Pressure[k][i] - this.Pressure[k - 1][i]));
                        }
                    }
                    ++k;
                    sw.WriteLine(s);
                }
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }

            return diff;
            #endregion
        }
    }
}