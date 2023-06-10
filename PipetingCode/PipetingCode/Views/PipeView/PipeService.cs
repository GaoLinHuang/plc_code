using PipettingControl;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Windows.Base;

namespace PipettingCode.Views
{
    internal class PipeService : NotifyBase
    {
        private TCPUDP_Common mTCPUDP_Common = new TCPUDP_Common();
        private Tcp_CommData mCommData = new Tcp_CommData();

        public PipeService()
        {
            ConnectCommand = new DelegateCommand(OnConnect);
            InitCommand = new DelegateCommand(OnInit);
            InitZCommand = new DelegateCommand(OnInitZ);
            Steps = new ObservableCollection<int>();
            MoveXLeftCommand = new DelegateCommand(MoveXLeft);
            MoveXRightCommand = new DelegateCommand(MoveXRight);
            MoveFrontCommand = new DelegateCommand(MoveFront);
            MoveBackCommand = new DelegateCommand(MoveBack);
            MoveTopCommand = new DelegateCommand(MoveTop);
            MoveBottomCommand = new DelegateCommand(MoveBottom);
            GetPosCommand = new DelegateCommand(GetPos);
            int num = 10;
            for (int i = 1; i < 10; i++)
            {
                Steps.Add(num);
                num *= (i + 1);
            }
        }

        /// <summary>
        /// 获取坐标
        /// </summary>
        private void GetPos(object obj)
        {
            #region 获取坐标

            //读取当前加样臂的坐标
            mCommData.mNode_ID = Global_Parameter.XCMDID[0];//0x28;
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            X = mCommData.mCurrentlocation[0];
            Console.WriteLine(X);
            Thread.Sleep(500);
            mCommData.mNode_ID = Global_Parameter.YCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Y = mCommData.mCurrentlocation[0];
            Console.WriteLine(Y);
            Thread.Sleep(500);
            mCommData.mNode_ID = Global_Parameter.ZCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Z = mCommData.mCurrentlocation[0];
            Console.WriteLine(Z);

            #endregion 获取坐标
        }

        private void MoveXLeft(object obj)
        {
            #region X轴右移

            mCommData.mNode_ID = Global_Parameter.XCMDID[0];//0x28;
            mCommData.mFC2 = 0X00;
            mCommData.mSpeed = Global_Parameter.XSlowSpeed[0];
            mCommData.mACCSpeed = Global_Parameter.XSlowAccSpeed[0];
            mCommData.mWaittime = 500;
            my_XLeft = mTCPUDP_Common.cmd_Returndistance(mCommData);
            my_XLeft = mCommData.mCurrentlocation[0];

            my_XLeft -= StepValue;
            /*
            if (my_XLeft > 5600)
            {
                my_XLeft = 5600;
            }
            //*/
            mCommData.mDistance = my_XLeft;
            int result = mTCPUDP_Common.cmd_MotorControl(mCommData);
            if (result == 0)
            {
                Console.WriteLine(mCommData.mCurrentlocation[0]);
            }
            mCommData.mNode_ID = Global_Parameter.XCMDID[0];//0x28;
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Console.WriteLine(mCommData.mCurrentlocation[0]);
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->X轴右移,绝对位置:" + my_XLeft + "," + m_Msg;
            Console.WriteLine(m_Msg);

            #endregion X轴右移
        }

        /// <summary>
        /// 向下移动
        /// </summary>
        /// <param name="obj"></param>
        private void MoveBottom(object obj)
        {
            #region Z轴下降

            mCommData.mWaittime = 500;
            mCommData.mNode_ID = Global_Parameter.ZCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            my_ZLeft = mCommData.mCurrentlocation[0];

            my_ZLeft += StepValue;
            mCommData.mNode_ID = Global_Parameter.ZCMDID[0];
            mCommData.mFC2 = 0X00;
            mCommData.mSpeed = Global_Parameter.ZSlowSpeed[0];
            mCommData.mACCSpeed = Global_Parameter.ZSlowAccSpeed[0];
            mCommData.mDistance = my_ZLeft;
            int result = mTCPUDP_Common.cmd_MotorControl(mCommData);
            if (result == 0)
            {
                Console.WriteLine(mCommData.mCurrentlocation[0]);
            }
            mCommData.mNode_ID = Global_Parameter.ZCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Console.WriteLine(mCommData.mCurrentlocation[0]);
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->Z轴下降,绝对位置:" + my_ZLeft + "," + m_Msg;

            #endregion Z轴下降
        }

        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="obj"></param>
        private void MoveTop(object obj)
        {
            #region Z轴升起

            mCommData.mWaittime = 500;
            mCommData.mNode_ID = Global_Parameter.ZCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            my_ZLeft = mCommData.mCurrentlocation[0];
            Console.WriteLine(my_ZLeft);

            my_ZLeft -= StepValue;
            if (my_ZLeft < 0)
            {
                my_ZLeft = 0;
            }
            mCommData.mNode_ID = Global_Parameter.ZCMDID[0];
            mCommData.mFC2 = 0X00;
            mCommData.mSpeed = Global_Parameter.ZSlowSpeed[0];
            mCommData.mACCSpeed = Global_Parameter.ZSlowAccSpeed[0];
            mCommData.mDistance = my_ZLeft;
            int result = mTCPUDP_Common.cmd_MotorControl(mCommData);
            if (result == 0)
            {
                Console.WriteLine(mCommData.mCurrentlocation[0]);
            }
            mCommData.mNode_ID = Global_Parameter.ZCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Console.WriteLine(mCommData.mCurrentlocation[0]);
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->Z轴升起,绝对位置:" + my_ZLeft + "," + m_Msg;

            #endregion Z轴升起
        }

        /// <summary>
        /// 向后移动
        /// </summary>
        /// <param name="obj"></param>
        private void MoveBack(object obj)
        {
            #region Y轴后移

            int result;
            mCommData.mWaittime = 500;
            mCommData.mNode_ID = Global_Parameter.YCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            my_YLeft = mCommData.mCurrentlocation[0];
            my_YLeft -= StepValue;
            if (my_YLeft < 0)
            {
                my_YLeft = 0;
            }
            //1.设置执行者
            mCommData.mGroupName = Global_Parameter.YGroup;//Y轴预备执行组组名
                                                           //2.设置执行组命令
            for (int i = 0; i < Global_Parameter.YCMDID.Length; i++)
            {
                // Thread.Sleep(100);
                mCommData.mNode_ID = Global_Parameter.YCMDID[i];
                mCommData.mSpeed = Global_Parameter.YSlowSpeed[i];
                mCommData.mACCSpeed = Global_Parameter.YSlowAccSpeed[i];// - (i * 20);
                mCommData.mDistance = my_YLeft;
                mCommData.mFC2 = 0x80;
                result = mTCPUDP_Common.cmd_MotorControl(mCommData);
            }
            //3.执行预备执行组
            mCommData.mNode_ID = 0x00;
            mCommData.mFC2 = 0x00;
            mCommData.mCurNode_ID = Global_Parameter.YCMDID;
            result = mTCPUDP_Common.cmd_RunPreparatoryExecutionTeam(mCommData);
            if (result == 0)
            {
                Console.WriteLine(mCommData.mCurrentlocation[0]);
            }
            //--------------------------------------------------------------------------------------
            mCommData.mNode_ID = Global_Parameter.YCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Console.WriteLine(mCommData.mCurrentlocation[0]);
            //--------------------------------------------------------------------------------------
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->Y轴内移,绝对位置:" + my_YLeft + "," + m_Msg;
            my_OldYLeft = my_YLeft;

            #endregion Y轴后移
        }

        /// <summary>
        /// 向前移动
        /// </summary>
        /// <param name="obj"></param>
        private void MoveFront(object obj)
        {
            #region Y轴前移

            int result = 0;
            mCommData.mWaittime = 500;
            mCommData.mNode_ID = Global_Parameter.YCMDID[0];
            my_YLeft = mTCPUDP_Common.cmd_Returndistance(mCommData);
            my_YLeft = mCommData.mCurrentlocation[0];

            my_YLeft += StepValue;
            if (my_YLeft > 4000)
            {
                my_YLeft = 4000;
            }
            mCommData.mGroupName = Global_Parameter.YGroup;// Y轴预备执行组组名
            int b = 0;
            mCommData.mGroupName = 0x02;//Y轴预备执行组组名
                                        //2.设置执行组命令
            for (int i = 0; i < Global_Parameter.YCMDID.Length; i++)
            {
                mCommData.mNode_ID = Global_Parameter.YCMDID[i];
                mCommData.mSpeed = Global_Parameter.YSlowSpeed[i];
                mCommData.mACCSpeed = Global_Parameter.YSlowAccSpeed[i];// + (i * 20); ;
                mCommData.mDistance = my_YLeft;
                mCommData.mFC2 = 0x80;
                result = mTCPUDP_Common.cmd_MotorControl(mCommData);
                if (result != 0)
                {
                    break;
                }
            }
            if (result != 0)
            {
                return;
            }
            //3.执行预备执行组
            mCommData.mNode_ID = 0x00;
            mCommData.mFC2 = 0x00;
            mCommData.mCurNode_ID = Global_Parameter.YCMDID;
            result = mTCPUDP_Common.cmd_RunPreparatoryExecutionTeam(mCommData);
            if (result == 0)
            {
                Console.WriteLine(mCommData.mCurrentlocation[0]);
            }
            mCommData.mNode_ID = Global_Parameter.YCMDID[0];
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Console.WriteLine(mCommData.mCurrentlocation[0]);
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->Y轴外移,绝对位置:" + my_YLeft + "," + m_Msg;
            my_OldYLeft = my_YLeft;

            #endregion Y轴前移
        }

        private void MoveXRight(object obj)
        {
            #region X轴右移

            mCommData.mNode_ID = Global_Parameter.XCMDID[0];//0x28;
            mCommData.mFC2 = 0X00;
            mCommData.mSpeed = Global_Parameter.XSlowSpeed[0];
            mCommData.mACCSpeed = Global_Parameter.XSlowAccSpeed[0];
            mCommData.mWaittime = 500;
            my_XLeft = mTCPUDP_Common.cmd_Returndistance(mCommData);
            my_XLeft = mCommData.mCurrentlocation[0];

            my_XLeft += StepValue;
            /*
            if (my_XLeft > 5600)
            {
                my_XLeft = 5600;
            }
            //*/
            mCommData.mDistance = my_XLeft;
            int result = mTCPUDP_Common.cmd_MotorControl(mCommData);
            if (result == 0)
            {
                Console.WriteLine(
                mCommData.mCurrentlocation[0]);
            }
            mCommData.mNode_ID = Global_Parameter.XCMDID[0];//0x28;
            mTCPUDP_Common.cmd_Returndistance(mCommData);
            Console.WriteLine(mCommData.mCurrentlocation[0]);
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->X轴右移,绝对位置:" + my_XLeft + "," + m_Msg;
            Console.WriteLine(m_Msg);

            #endregion X轴右移
        }

        private void OnInitZ(object obj)
        {
            #region Z轴初始化

            my_ZLeft = 0;
            mCommData.mGroupName = Global_Parameter.ZGroup;
            int result = -1;
            for (int i = 0; i < Global_Parameter.ZCMDID.Length; i++)
            {
                mCommData.mNode_ID = Global_Parameter.ZCMDID[i];
                result = mTCPUDP_Common.cmd_SetPreparatoryExecutionTeam(mCommData);
                if (result != 0)
                {
                    LogUtils.SaveLog("button1_Click 建立预备执行组Z轴组失败！", "TCPdata");         //  写日志
                    break;
                }
            }
            LogUtils.SaveLog("button1_Click Z轴初始化....", "TCPdata");         //  写日志
            result = mTCPUDP_Common.cmd_SamplingarmZaxisInit(mCommData);
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->Z轴初始化," + m_Msg;
            Console.WriteLine(m_Msg);

            #endregion Z轴初始化
        }

        private void OnInit(object obj)
        {
            #region 加样臂初始化

            my_XLeft = 0;
            my_ZLeft = 0;
            my_YLeft = 0;
            int result = mTCPUDP_Common.Cmd_GroupInit(mCommData);                   //建立预备执行组
            if (result != 0)
            {
                MessageBox.Show("button1_Click 建立执行组失败!");

                return;
            }
            Display_List("加样臂初始化开始....");
            result = mTCPUDP_Common.cmd_SamplingarmInit(mCommData);
            if (result == 0)
            {
                my_InitFlag1 = true;
            }
            else
            {
                my_InitFlag1 = false;
            }
            string m_Msg = (result == 0) ? "操作成功！" : "操作失败!";
            m_Msg = "实验平台-->加样臂调试-->加样臂初始化," + m_Msg;
            Console.WriteLine(m_Msg);
            ;

            #endregion 加样臂初始化
        }

        /// <summary>
        /// 点击连接
        /// </summary>
        /// <param name="obj"></param>
        private void OnConnect(object obj)
        {
            Check_Connect();
        }

        public ICommand ConnectCommand { get; }

        public ICommand MoveXLeftCommand { get; }
        public ICommand MoveXRightCommand { get; }
        public ICommand MoveFrontCommand { get; }
        public ICommand MoveBackCommand { get; }
        public ICommand MoveTopCommand { get; }
        public ICommand MoveBottomCommand { get; }
        public ICommand InitCommand { get; }
        public ICommand InitZCommand { get; }
        public ICommand GetPosCommand { get; }
        public ObservableCollection<int> Steps { get; set; }
        private int _stepValue;

        public int StepValue
        {
            get => _stepValue;
            set => SetField(ref _stepValue, value);
        }

        private int _x;

        public int X
        {
            get => _x;
            set => SetField(ref _x, value);
        }

        private int _y;

        public int Y
        {
            get => _y;
            set => SetField(ref _y, value);
        }

        private int _z;

        public int Z
        {
            get => _z;
            set => SetField(ref _z, value);
        }

        private void Check_Connect()
        {
            bool result = false;
            int m_Countnum = 0;
            try
            {
                while (!result)
                {
                    Thread.Sleep(10);
                    result = mTCPUDP_Common.client_Connect(Global_Parameter.IPAddress, Global_Parameter.IPPort);
                    if (!result)
                    {
                        m_Countnum++;
                        if (m_Countnum >= 5)
                        {
                            LogUtils.SaveLog("Check_Connect 连接网络失败......", "TCPdata");
                            //DialogResult dt = new DialogResult();
                            //Run_circularProgressFlag = true;
                            //this.Invoke((MethodInvoker)delegate
                            //{
                            //    dt = MessageBoxEX.Show("请检查线路连接是否正确,是否继续等待连接!", "连接失败提示", MessageBoxButtons.YesNo, new string[] { "等待", "关闭" });
                            //});
                            //if (dt == DialogResult.Yes)
                            //{
                            //    m_Countnum = 0;
                            //    Run_circularProgressFlag = false;
                            //}
                            //else if (dt == DialogResult.No)
                            //{
                            //    Exit_circularProgressFlag = true;
                            //    System.Environment.Exit(0);
                            //}
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //Application.DoEvents();
                //this.Invoke(new Action(() =>
                //{
                LogUtils.SaveLog("Check_Connect 连接网络成功，进行血型仪初始化......", "TCPdata");
                //}));
                Init_Sys();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check_Connect errors:" + ex.Message);
            }
        }

        #region 节点匹配初始化

        /// <summary>
        /// 节点匹配初始化
        /// </summary>
        private void Init_Sys()
        {
            int result = -1;
            string s1 = "";
            bool m_BreakFlag = false;
            int m_NodeNo = 0;
            try
            {
                //------------------------------------------------------------------------------------------------------------
                mCommData.mIPAddress = Global_Parameter.IPAddress;
                mCommData.mIPport = Global_Parameter.IPPort;
                mCommData.mWaittime = 500;
                //------------------------------------------------------------------------------------------------------------

                #region 节点匹配

                //this.Invoke(new Action(() =>
                //{
                //    Display_List("正在进行系统节点匹配......");
                //    LogUtils.SaveLog("Init_Sys 节点匹配......", "TCPdata");
                //}));
                Thread.Sleep(200);
                //listBox1.Items.Clear();
                //   Global_Parameter.Node_ID = "0x11";
                while (m_BreakFlag == false)
                {
                    string[] strPos = Global_Parameter.Node_ID.Split(',');
                    string m_NoNG = "";
                    Thread.Sleep(10);
                    m_NodeNo = 0;
                    for (int i = 0; i < strPos.Length; i++)
                    {
                        Thread.Sleep(10);
                        mCommData.mNode_ID = (byte)(strPos[i].HexStringToInt());
                        Console.WriteLine("节点匹配,节点:" + mCommData.mNode_ID.ToString("X2"));

                        result = mTCPUDP_Common.cmd_SearchNode(mCommData);                              //搜索节点
                        if (result != 0)
                        {
                            Thread.Sleep(100);
                            result = mTCPUDP_Common.cmd_SearchNode(mCommData);                              //重复一次搜索节点
                        }
                        if (result == 0)
                        {
                            Console.WriteLine("节点匹配成功,节点:" + mCommData.mNode_ID.ToString("X2"));
                            s1 = "";
                            for (int j = 0; j < mCommData.mBackNode.Length; j++)
                            {
                                if (mCommData.mNode_ID == mCommData.mBackNode[j])
                                {
                                    s1 = "0x" + mCommData.mBackNode[j].ToString("X2");
                                    break;
                                }
                            }
                            if (s1 == "")
                            {
                                m_NoNG += "," + strPos[i];
                            }
                            else
                            {
                                m_NodeNo++;
                                s1 = "搜素到节点：" + strPos[i];
                                Console.WriteLine(s1);
                            }
                        }
                        else
                        {
                            Console.WriteLine("节点匹配失败,节点:" + mCommData.mNode_ID.ToString("X2"));
                            m_NoNG += "," + strPos[i];
                        }
                    }
                    if (m_NodeNo == strPos.Length)
                    {
                        Console.WriteLine("Init_Sys 节点匹配成功!", "TCPdata");
                        break;
                    }
                    else
                    {
                        //Console.WriteLine("节点匹配失败,缺少节点:" + m_NoNG);
                        //LogUtils.SaveLog("Init_Sys 节点匹配失败,缺少节点:" + m_NoNG, "TCPdata");
                        ////获取用户点击提示框的按钮值
                        //DialogResult bl = MessageBox.Show("缺少节点:" + m_NoNG, "系统提示", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                        //if (bl == DialogResult.Ignore)//忽略到最后一个节点时，初始化现有节点得电机
                        //{
                        //    m_BreakFlag = true;
                        //}
                        //if (bl == DialogResult.Abort)//如果为终止，关闭程序
                        //{
                        //    Application.Exit();
                        //}
                        //if (bl == DialogResult.Retry)//如果为重试，重启程序
                        //{
                        //    ;
                        //}
                    }
                }

                #endregion 节点匹配

                //1.建立预备执行组
                result = mTCPUDP_Common.Cmd_GroupInit(mCommData);                   //建立预备执行组
                if (result != 0)
                {
                    MessageBox.Show("设置预备执行组失败!");
                    LogUtils.SaveLog("Init_Sys 设置预备执行组失败....", "TCPdata");         //  写日志
                }

                Console.WriteLine
                ("设置预备执行组成功！");
                LogUtils.SaveLog("设置预备执行组成功", "TCPdata");

                if (Global_Parameter.AutoInitFlag.Equals("ON"))
                {
                    result = mTCPUDP_Common.cmd_CheckAllNoteInitFlag(mCommData);
                    if (result != 0)
                    {
                        Console.WriteLine("正在进行系统初始化......");
                        LogUtils.SaveLog("Init_Sys 设置预备执行组......", "TCPdata");

                        Thread.Sleep(500);
                        Global_Parameter.Sys_InitFlag = false;
                        mCommData.mSamplesize = Global_Parameter.YCMDID.Length;//8;
                        result = DeviceCheck_AutoInit(0);
                        if (result == 0)
                        {
                            Global_Parameter.Sys_InitFlag = true;//系统初始化
                        }
                        else
                        {
                            MessageBox.Show("系统初始化失败，无法进行实验操作!");
                        }
                    }
                    else
                    {
                        Global_Parameter.Sys_InitFlag = true;
                        Display_List("仪器已经初始化......");
                    }
                }
                else
                {
                    result = mTCPUDP_Common.cmd_CheckAllNoteInitFlag(mCommData);
                    if (result == 0)
                    {
                        Global_Parameter.Sys_InitFlag = true;
                    }
                }
                LogUtils.SaveLog("Init_Sys 血型仪初始化完成!", "TCPdata");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init_Sys errors:" + ex.Message);
            }
            //Run_circularProgressFlag = true;
            //Exit_circularProgressFlag = true;
        }

        public int DeviceCheck_AutoInit(int In_Bz)
        {
            int result = -1;
            string resulterr = "";
            int m_Countnum = 0;
            //  DialogResult dt = new DialogResult();
            int a1 = 0;
            int errNo = 0;
            bool m_IgnoreFlag = false;
            bool m_AbortFlag = false;
            try
            {
                mCommData.mIPAddress = Global_Parameter.IPAddress;
                mCommData.mIPport = Global_Parameter.IPPort;
                mCommData.mWaittime = 500;

                #region 1.设置预备执行组

                try
                {
                    resulterr = "设置预备执行组......";
                    Display_List(resulterr);
                    result = -1;
                    a1 = 0;
                    while (result != 0)
                    {
                        Thread.Sleep(100);
                        result = mTCPUDP_Common.Cmd_GroupInit(mCommData);                   //建立预备执行组
                        if (result != 0)
                        {
                            errNo++;
                            resulterr = "设置预备执行组失败";
                            Display_List(resulterr);
                            LogUtils.SaveLog("DeviceCheck_AutoInit 设置预备执行组失败....", "TCPdata");         //  写日志
                            m_Countnum++;
                            if (m_Countnum >= 5)
                            {
                                #region DialogResult

                                m_IgnoreFlag = false;
                                m_AbortFlag = false;
                                //获取用户点击提示框的按钮值
                                //DialogResult bl = MessageBox.Show("设置预备执行组失败,是否重试!", "初始化并自检提示", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                //if (bl == DialogResult.Ignore)//忽略到最后一个节点时，初始化现有节点得电机
                                //{
                                //    m_IgnoreFlag = true;
                                //}
                                //if (bl == DialogResult.Abort)//如果为终止，关闭程序
                                //{
                                //    m_AbortFlag = true;
                                //}
                                //if (bl == DialogResult.Retry)//如果为重试，重启程序
                                //{
                                //    m_Countnum = 0;
                                //}

                                if (m_IgnoreFlag == true)
                                {
                                    break;
                                }
                                if (m_AbortFlag == true)
                                {
                                    goto errNo;
                                }

                                #endregion DialogResult
                            }
                        }
                        else
                        {
                            resulterr = "设置预备执行组成功......";
                            Display_List(resulterr);
                            LogUtils.SaveLog("DeviceCheck_AutoInit 设置预备执行组成功....", "TCPdata");         //  写日志
                            break;
                        }
                    }
                }
                catch (Exception ex1)
                {
                    ;
                }

                #endregion 1.设置预备执行组

                #region 2.加样臂初始化

                try
                {
                    result = -1;
                    a1 = 0;
                    resulterr = "加样臂初始化......";
                    Display_List(resulterr);
                    while (result != 0)
                    {
                        Thread.Sleep(100);
                        result = mTCPUDP_Common.cmd_SamplingarmInit(mCommData);                   //加样臂初始化
                        if (result != 0)
                        {
                            errNo++;
                            resulterr = "加样臂初始化失败";
                            Display_List(resulterr);
                            LogUtils.SaveLog("DeviceCheck_AutoInit 加样臂初始化失败....", "TCPdata");         //  写日志
                            m_Countnum++;
                            if (m_Countnum >= 5)
                            {
                                #region DialogResult

                                //this.Invoke((MethodInvoker)delegate
                                //{
                                //    m_IgnoreFlag = false;
                                //    //获取用户点击提示框的按钮值
                                //    DialogResult bl = MessageBox.Show("加样臂初始化失败,是否重试!", "初始化并自检提示", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                //    if (bl == DialogResult.Ignore)//忽略到最后一个节点时，初始化现有节点得电机
                                //    {
                                //        m_IgnoreFlag = true;
                                //    }
                                //    if (bl == DialogResult.Abort)//如果为终止，关闭程序
                                //    {
                                //        m_AbortFlag = true;
                                //    }
                                //    if (bl == DialogResult.Retry)//如果为重试，重启程序
                                //    {
                                //        m_Countnum = 0;
                                //    }
                                //});
                                if (m_IgnoreFlag == true)
                                {
                                    break;
                                }
                                if (m_AbortFlag == true)
                                {
                                    goto errNo;
                                }

                                #endregion DialogResult
                            }
                        }
                        else
                        {
                            resulterr = "加样臂初始化成功......";
                            Display_List(resulterr);
                            LogUtils.SaveLog("DeviceCheck_AutoInit 加样臂初始化成功....", "TCPdata");         //  写日志
                            break;
                        }
                    }
                }
                catch (Exception ex1)
                {
                    ;
                }
                Thread.Sleep(1000);

                #endregion 2.加样臂初始化

                #region 4.泵初始化

                try
                {
                    result = -1;
                    a1 = 0;
                    resulterr = "泵初始化......";
                    Display_List(resulterr);
                    while (result != 0)
                    {
                        Thread.Sleep(100);
                        result = mTCPUDP_Common.Cmd_PlungerPumpOneInit(mCommData);                   //泵初始化
                        if (result != 0)
                        {
                            errNo++;
                            resulterr = "泵初始化失败......";
                            Display_List(resulterr);
                            LogUtils.SaveLog("DeviceCheck_AutoInit 泵初始化失败....", "TCPdata");         //  写日志
                            m_Countnum++;
                            if (m_Countnum >= 5)
                            {
                                #region DialogResult

                                //this.Invoke((MethodInvoker)delegate
                                //{
                                //    m_IgnoreFlag = false;
                                //    //获取用户点击提示框的按钮值
                                //    DialogResult bl = MessageBox.Show("泵初始化失败,是否重试!", "初始化并自检提示", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                //    if (bl == DialogResult.Ignore)//忽略到最后一个节点时，初始化现有节点得电机
                                //    {
                                //        m_IgnoreFlag = true;
                                //    }
                                //    if (bl == DialogResult.Abort)//如果为终止，关闭程序
                                //    {
                                //        m_AbortFlag = true;
                                //    }
                                //    if (bl == DialogResult.Retry)//如果为重试，重启程序
                                //    {
                                //        m_Countnum = 0;
                                //    }
                                //});
                                if (m_IgnoreFlag == true)
                                {
                                    break;
                                }
                                if (m_AbortFlag == true)
                                {
                                    goto errNo;
                                }

                                #endregion DialogResult
                            }
                        }
                        else
                        {
                            resulterr = "泵初始化成功......";
                            Display_List(resulterr);
                            LogUtils.SaveLog("DeviceCheck_AutoInit 泵初始化成功....", "TCPdata");         //  写日志
                            break;
                        }
                    }
                }
                catch (Exception ex1)
                {
                    ;
                }
                Thread.Sleep(1000);

                #endregion 4.泵初始化

                #region 8.把加样臂移到原点

                if (In_Bz == 0)
                {
                    try
                    {
                        result = -1;
                        a1 = 0;
                        resulterr = "把加样臂移到原点......";
                        Display_List(resulterr);
                        while (result != 0)
                        {
                            Thread.Sleep(100);
                            mCommData.mNode_ID = Global_Parameter.XCMDID[0];
                            mCommData.mSpeed = Global_Parameter.XInitSpeed[0];
                            mCommData.mACCSpeed = Global_Parameter.XInitAccSpeed[0];
                            mCommData.mFC2 = 0x00;
                            result = mTCPUDP_Common.cmd_NoteInit(mCommData);//把加样臂初始化移到原点
                            if (result != 0)
                            {
                                errNo++;
                                resulterr = "把加样臂移到原点失败......";
                                Display_List(resulterr);
                                LogUtils.SaveLog("DeviceCheck_AutoInit 把加样臂移到原点失败....", "TCPdata");         //  写日志
                                m_Countnum++;
                                if (m_Countnum >= 5)
                                {
                                    #region DialogResult

                                    //this.Invoke((MethodInvoker)delegate
                                    //{
                                    //    m_IgnoreFlag = false;
                                    //    //获取用户点击提示框的按钮值
                                    //    DialogResult bl = MessageBox.Show("把加样臂移到原点失败,是否重试!", "初始化并自检提示", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                    //    if (bl == DialogResult.Ignore)//忽略到最后一个节点时，初始化现有节点得电机
                                    //    {
                                    //        m_IgnoreFlag = true;
                                    //    }
                                    //    if (bl == DialogResult.Abort)//如果为终止，关闭程序
                                    //    {
                                    //        m_AbortFlag = true;
                                    //    }
                                    //    if (bl == DialogResult.Retry)//如果为重试，重启程序
                                    //    {
                                    //        m_Countnum = 0;
                                    //    }
                                    //});
                                    if (m_IgnoreFlag == true)
                                    {
                                        break;
                                    }
                                    if (m_AbortFlag == true)
                                    {
                                        goto errNo;
                                    }

                                    #endregion DialogResult
                                }
                            }
                            else
                            {
                                resulterr = "把加样臂移到原点成功......";
                                Display_List(resulterr);
                                LogUtils.SaveLog("DeviceCheck_AutoInit 把加样臂移到原点成功....", "TCPdata");         //  写日志
                                break;
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        ;
                    }
                }

                #endregion 8.把加样臂移到原点
            }
            catch (Exception ex)
            {
                resulterr = "初始化出错,errors:" + ex.Message;
                LogUtils.SaveLog("DeviceCheck_AutoInit 初始化出错，errors:" + ex.Message, "TCPdata");
            }
        errNo:
            if (errNo > 0)
            {
                result = -1;
                resulterr = "仪器初始化结束，但初始化失败......";
                LogUtils.SaveLog("DeviceCheck_AutoInit 仪器初始化结束，但初始化失败......", "TCPdata");
            }
            else
            {
                result = 0;
                resulterr = "仪器整个初始化成功！";
                LogUtils.SaveLog("DeviceCheck_AutoInit 仪器整个初始化成功！", "TCPdata");
            }
            Display_List(resulterr);
            return result;
        }

        private void Display_List(string resulterr)
        {
            Console.WriteLine(resulterr);
        }

        /// <summary>
        /// 加样臂按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private int my_XLeft = 0;

        private int my_YLeft = 0;
        private int my_ZLeft = 0;

        private bool my_InitFlag1 = false;
        private int my_OldYLeft = 0;
    }

    #endregion 节点匹配初始化
}