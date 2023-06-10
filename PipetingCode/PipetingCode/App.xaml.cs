using PipettingControl;
using System;
using System.Windows;

namespace PipettingCode
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Get_NodeData();
        }

        #region 获取配置文件各个参数数据

        /// <summary>
        /// 获取配置文件各个参数数据
        /// </summary>
        private void Get_NodeData()
        {
            try
            {
                #region 1.系统参数

                Global_Parameter.SysXyxtkq = Convert.ToInt16(IniFile.SysXyxtkq);
                Global_Parameter.IPAddress = IniFile.IPAddress;                                             //仪器网卡IP地址
                Global_Parameter.IPPort = IniFile.IPPort;                                                   //仪器网卡使用的端口号
                Global_Parameter.Sys_SaveLog_Flag = IniFile.Sys_SaveLog_Flag;                               //写日日志标志  ON 写  OFF 不写
                Global_Parameter.AutoInitFlag = IniFile.AutoInitFlag;                                       //是否自动初始化标志

                #endregion 1.系统参数

                #region 2.各模块节点值

                Global_Parameter.XCMDID = Get_StrToByte(IniFile.XCMDID);                                    //1.加样臂X轴地址
                Global_Parameter.YCMDID = Get_StrToByte(IniFile.YCMDID);                                    //2.加样臂Y轴地址
                Global_Parameter.ZCMDID = Get_StrToByte(IniFile.ZCMDID);                                    //3.加样臂Z轴地址
                Global_Parameter.PlungerCMDID = Get_StrToByte(IniFile.PlungerCMDID);                        //4.泵节点编号

                #endregion 2.各模块节点值

                #region 3.系统使用的所有节点，节点匹配使用

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

                #endregion 3.系统使用的所有节点，节点匹配使用

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
                //2.盐水槽
                Global_Parameter.Brine1StartX = int.Parse(IniFile.Brine1StartX);                            //7.盐水槽1开始X轴坐标
                Global_Parameter.Brine1StartY = int.Parse(IniFile.Brine1StartY);                            //8.盐水槽1开始Y轴坐标
                Global_Parameter.Brine1StartZ = int.Parse(IniFile.Brine1StartZ);                            //9.盐水槽1开始Z轴坐标
                Global_Parameter.Brine1EndX = int.Parse(IniFile.Brine1EndX);                                //10.盐水槽1结束X轴坐标
                Global_Parameter.Brine1EndY = int.Parse(IniFile.Brine1EndY);                                //11.盐水槽1结束Y轴坐标
                Global_Parameter.Brine1EndZ = int.Parse(IniFile.Brine1EndZ);                                //12.盐水槽1结束Z轴坐标
                Global_Parameter.Brine1MaxZ = int.Parse(IniFile.Brine1MaxZ);                                //9.盐水槽1开始Z轴坐标
                Global_Parameter.Brine1MoveZ = int.Parse(IniFile.Brine1MoveZ);                              //9.盐水槽1开始Z轴坐标
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

                //4.试剂位
                Global_Parameter.ReagentStartX = Get_StrToInt(IniFile.ReagentStartX);                       //19.试剂位开始X轴坐标
                Global_Parameter.ReagentStartY = Get_StrToInt(IniFile.ReagentStartY);                       //20.试剂位开始Y轴坐标
                Global_Parameter.ReagentStartZ = Get_StrToInt(IniFile.ReagentStartZ);                       //21.试剂位开始Z轴坐标
                Global_Parameter.ReagentEndX = Get_StrToInt(IniFile.ReagentEndX);                           //22.试剂位结束X轴坐标
                Global_Parameter.ReagentEndY = Get_StrToInt(IniFile.ReagentEndY);                           //23.试剂位结束Y轴坐标
                Global_Parameter.ReagentEndZ = Get_StrToInt(IniFile.ReagentEndZ);                           //24.试剂位结束Z轴坐标
                Global_Parameter.ReagentMaxZ = Get_StrToInt(IniFile.ReagentMaxZ);                           //21.试剂位开始Z轴坐标
                Global_Parameter.ReagentMoveZ = Get_StrToInt(IniFile.ReagentMoveZ);                         //21.试剂位开始Z轴坐标
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

                #endregion 4.加样臂使用的坐标

                #region 5.加样臂、夹手臂、泵的速度和加速度

                #region （1）.加样臂的速度和加速度

                //1.加样臂 刚接电时速度和加速度
                Global_Parameter.XSlowSpeed = Get_StrToInt(IniFile.XSlowSpeed);
                Global_Parameter.XSlowAccSpeed = Get_StrToInt(IniFile.XSlowAccSpeed);
                Global_Parameter.YSlowSpeed = Get_StrToInt(IniFile.YSlowSpeed);
                Global_Parameter.YSlowAccSpeed = Get_StrToInt(IniFile.YSlowAccSpeed);
                Global_Parameter.ZSlowSpeed = Get_StrToInt(IniFile.ZSlowSpeed);
                Global_Parameter.ZSlowAccSpeed = Get_StrToInt(IniFile.ZSlowAccSpeed);
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

                #endregion （1）.加样臂的速度和加速度

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

                #endregion （3）.泵的速度和加速度

                #endregion 5.加样臂、夹手臂、泵的速度和加速度

                #region 7.预备执行组

                Global_Parameter.YGroup = (byte)(IniFile.YGroup.HexStringToInt());                          //Y轴预备执行组组名 0x02
                Global_Parameter.ZGroup = (byte)(IniFile.ZGroup.HexStringToInt());                          //Z轴预备执行组组名 0x04
                Global_Parameter.XGroup = (byte)(IniFile.XGroup.HexStringToInt());                          //X轴预备执行组组名 0x06
                Global_Parameter.PumpGroup = (byte)(IniFile.PumpGroup.HexStringToInt());                    //泵预备执行组组名  0x08

                #endregion 7.预备执行组

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

                #endregion 吸液、注液系数对照表
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get_NodeData errors:" + ex.Message);
            }
        }

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
                MessageBox.Show("Get_StrToByte errors:" + ex.Message);
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
                MessageBox.Show("Get_StrToInt errors:" + ex.Message);
            }
            return a1;
        }

        #endregion 获取配置文件各个参数数据
    }
}