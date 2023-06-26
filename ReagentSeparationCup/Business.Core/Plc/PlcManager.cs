using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Business.Core
{
    public class PlcManager
    {
        private static readonly object _locker = new object();
        //private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        public event Action OnConnected;
        public event Action OnDisconnected;

        #region 串口

        private readonly SerialPort _serialPort = new SerialPort();

        #endregion

        public PlcManager()
        {
            Initial();
        }

        void Initial()
        {
            try
            {
                _serialPort.PortName = "COM3"; // 默认端口就是COM3
                _serialPort.BaudRate = Convert.ToInt32(115200); // 波特率
                _serialPort.Parity = Parity.Odd; // 奇校验
                _serialPort.DataBits = Convert.ToInt16(8); // 8位数据位
                _serialPort.StopBits = StopBits.One;

                _serialPort.ReadTimeout = 800; // 0.8秒读超时
                _serialPort.WriteTimeout = 800; // 0.8秒写超时
            }
            catch (Exception ex)
            {

            }
        }

        public Task StartConnectAsync()
        {
            Task.Run(async () =>
            {
                while (!IsConnectedTask)
                {
                    try
                    {
                       
                        lock (_locker)
                        {
                            _serialPort.Open();
                        }
                        IsConnectedTask = true;
                        OnConnected?.Invoke();
                    }
                    catch (Exception e)
                    {
                        IsConnectedTask = false;
                    }
                    await Task.Delay(3000);
                    Console.WriteLine("尝试重连...");
                }
            });
            return Task.CompletedTask;
        }

        public bool IsConnectedTask { get; private set; } = false;


        #region 内部

        #region 辅助函数，得到需要传输的16进制

        private string ConvertDecToHex(string input, int k)
        {
            int n = Convert.ToInt32(input);
            int copyK = k;
            UInt32 tmp = (UInt32)n;

            string ans = "";
            string[] arr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

            for (int i = 0; i < copyK / 4; ++i)
            {
                int index = 0;
                for (int j = 0; j < 4; ++j)
                {
                    k -= 1;
                    index = 2 * index + ((tmp & (1 << k)) == 0 ? 0 : 1);
                }

                ans += arr[index];
            }

            if (copyK == 16)
            {
                ans = ans.Substring(2, 2) + ans.Substring(0, 2);
            }
            else if (copyK == 32)
            {
                ans = ans.Substring(6, 2) + ans.Substring(4, 2) + ans.Substring(2, 2) + ans.Substring(0, 2);
            }

            return ans;
        }

        #endregion

        #region 从输入里面解析参数，将其转换成16进制

        private int ConvertHexToDec(string input, int k)
        {
            int ans = 0;
            if (k == 16)
            {
                input = input.Substring(2, 2) + input.Substring(0, 2);
                UInt16 tmp = Convert.ToUInt16(input, 16);
                ans = (Int16)tmp;
            }
            else if (k == 32)
            {
                input = input.Substring(6, 2) + input.Substring(4, 2) + input.Substring(2, 2) + input.Substring(0, 2);
                UInt32 tmp = Convert.ToUInt32(input, 16);
                ans = (Int32)tmp;
            }

            return ans;
        }

        #endregion

        #region 得到真正的地址表示

        // 5, 7, 8
        private string GetRealAddr(string address, int n)
        {
            string tmp = "";
            for (int i = address.Length; i < n; ++i)
            {
                tmp += "0"; // 不足n位补0
            }

            if (address[0] == 'R' || address[0] == 'Y')
            {
                return address[0] + tmp + address.Substring(1, address.Length - 1);
            }
            else if (address.StartsWith("DT"))
            {
                return address.Substring(0, 2) + tmp + address.Substring(2, address.Length - 2);
            }
            else if (address.StartsWith("DDT"))
            {
                return address.Substring(0, 3) + tmp + address.Substring(3, address.Length - 3);
            }

            return tmp;
        }

        #endregion

        #region 读取一个值

        public int PLC_SendCommand_Read(string address)
        {
            if (address[0] == 'R' || address[0] == 'Y')
            {
                return PLC_RCS(GetRealAddr(address, 5));
            }
            else if (address.StartsWith("DT"))
            {
                return PLC_RD(GetRealAddr(address, 7));
            }
            else if (address.StartsWith("DDT"))
            {
                return PLC_RD(GetRealAddr(address, 8));
            }

            return 0;
        }

        #endregion

        #region 写入一个值

        public bool PLC_SendCommand_Write(string address, int value)
        {
            if (address[0] == 'R' || address[0] == 'Y')
            {
                return PLC_WCS(GetRealAddr(address, 5), value);
            }
            else if (address.StartsWith("DT"))
            {
                return PLC_WD(GetRealAddr(address, 7), value);
            }
            else if (address.StartsWith("DDT"))
            {
                return PLC_WD(GetRealAddr(address, 8), value);
            }

            return false;
        }

        #endregion

        #region 读取数组

        public int[] PLC_SendCommand_ReadArray(string address, int size)
        {
            if (address.StartsWith("R"))
            {
                return PLC_RDArray(GetRealAddr(address, 5), size);
            }
            else if (address.StartsWith("DT"))
            {
                return PLC_RDArray(GetRealAddr(address, 7), size);
            }
            else if (address.StartsWith("DDT"))
            {
                return PLC_RDArray(GetRealAddr(address, 8), size);
            }

            return new int[0];
        }

        #endregion

        #region 写入数组

        public bool PLC_SendCommand_WriteArray(string address, int[] input)
        {
            if (address.StartsWith("DT"))
            {
                return PLC_WDArray(GetRealAddr(address, 7), input);
            }
            else if (address.StartsWith("DDT"))
            {
                return PLC_WDArray(GetRealAddr(address, 8), input);
            }

            return false;
        }

        #endregion

        #region 松下PLC指令格式

        // 写WCS
        //string command = "%01#WCSR00010**\r";
        // 读RCS
        //string command = "%01#RCSR0001**\r";

        // 读RD
        //string command = "%01#RDD0010000100**\r";

        // 写WD
        //string command = "%01#WDD00100001006300**\r";
        // _serialPort.Write(command);
        //string s =  _serialPort.ReadTo("\x0d");

        // 读 RCP
        //string command = "%01#RCPR0181R0182R0183R0184R0185R0186R0187R0188**\r";
        // _serialPort.Write(command);
        //string s =  _serialPort.ReadTo("\x0d");

        #endregion

        #region 读取R类型寄存器

        private int PLC_RCS(string address)
        {
            try
            {
                string command = "%01#RCS" + address + "**\r";
                // 加锁，只能允许一个线程访问下面的资源
                lock (_locker)
                {
                    _serialPort.Write(command);
                    string? receive = _serialPort.ReadTo("\x0d");
                    if (receive[3] == '$')
                    {
                        #region 更新状态栏和PLC连接标志

                        //if (App.Current == null)
                        //{
                        //    return -1;
                        //}
                        //App.Current.Dispatcher.Invoke(new Action(() =>
                        //{
                        //    CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
                        //    CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯成功";
                        //    MainWindowViewModel.GetInstance().IsConnect = true;
                        //}));
                        IsConnectedTask = true;
                        OnConnected?.Invoke(); //通讯成功

                        #endregion

                        return receive[receive.Length - 3] == '0' ? 0 : 1;
                    }
                }
            }
            catch (Exception ex)
            {
                //#region 更新状态栏和PLC连接标志
                //if (App.Current == null)
                //{
                //    return -1;
                //}
                //App.Current.Dispatcher.Invoke(new Action(() =>
                //{

                //    CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning copy.png"));
                //    CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯失败";
                //    MainWindowViewModel.GetInstance().IsConnect = false;
                //}));
                //#endregion
                //MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                IsConnectedTask = false;
                OnDisconnected?.Invoke(); //通讯失败
            }

            return -1;
        }

        #endregion

        #region 写入R类型寄存器

        private bool PLC_WCS(string address, int value)
        {
            if (value != 0 && value != 1)
            {
                throw new Exception("写入的值必须是0或者1!");
            }

            try
            {
                string command = "%01#WCS" + address + value.ToString() + "**\r";
                lock (_locker)
                {
                    _serialPort.Write(command);
                    string? receive = _serialPort.ReadTo("\x0d");
                    if (receive[3] == '$')
                    {
                        #region 更新状态栏和PLC连接标志

                        //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯失败")
                        //{
                        //    if (App.Current == null)
                        //    {
                        //        return false;
                        //    }
                        //    App.Current.Dispatcher.Invoke(new Action(() =>
                        //    {
                        //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
                        //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯成功";
                        //        MainWindowViewModel.GetInstance().IsConnect = true;
                        //    }));
                        //}
                        IsConnectedTask = true;
                        OnConnected?.Invoke(); //通讯成功

                        #endregion

                        return true;
                    }
                }
                //if (App.Current == null)
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                #region 更新状态栏和PLC连接标志并弹窗

                //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯成功")
                //{
                //    if (App.Current == null)
                //    {
                //        return false;
                //    }
                //    App.Current.Dispatcher.Invoke(new Action(() =>
                //    {

                //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning copy.png"));
                //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯失败";
                //        MainWindowViewModel.GetInstance().IsConnect = false;
                //    }));
                //}
                //#endregion

                //MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                IsConnectedTask = false;
                OnDisconnected?.Invoke(); //通讯失败
            }

            return false;
        }

        #endregion

        #region 读取DT和DDT类型，只读取一个

        private int PLC_RD(string beginAddress)
        {
            try
            {
                string address = "", endAddress = "";
                if (beginAddress.StartsWith("DT"))
                {
                    address = "D" + beginAddress.Substring(2, beginAddress.Length - 2);
                    endAddress = address.Substring(1, address.Length - 1);
                }
                else if (beginAddress.StartsWith("DDT"))
                {
                    address = "D" + beginAddress.Substring(3, beginAddress.Length - 3);
                    endAddress = ((Convert.ToInt32(address.Substring(1, address.Length - 1))) + 1).ToString();
                }

                string command = "%01#RD" + address + endAddress + "**\r";

                lock (_locker)
                {
                    _serialPort.Write(command);
                    string? receive = _serialPort.ReadTo("\x0d");
                    int ans = 0;
                    if (receive[3] == '$')
                    {
                        #region 更新状态栏和PLC连接标志

                        //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯失败")
                        //{
                        //    if (App.Current == null)
                        //    {
                        //        return -1;
                        //    }
                        //    App.Current.Dispatcher.Invoke(new Action(() =>
                        //    {
                        //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
                        //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯成功";
                        //        MainWindowViewModel.GetInstance().IsConnect = true;

                        //    }));
                        //}
                        IsConnectedTask = true;
                        OnConnected?.Invoke(); //通讯成功

                        #endregion

                        // 6, receive[:-2]
                        if (beginAddress.StartsWith("DT"))
                        {
                            ans = ConvertHexToDec(receive.Substring(6, receive.Length - 8), 16);
                        }
                        else if (beginAddress.StartsWith("DDT"))
                        {
                            ans = ConvertHexToDec(receive.Substring(6, receive.Length - 8), 32);
                        }

                        return ans;
                    }
                }
            }
            catch (Exception ex)
            {
                //#region 更新状态栏和PLC连接标志并弹窗
                //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯成功")
                //{
                //    if (App.Current == null)
                //    {
                //        return -1;
                //    }
                //    App.Current.Dispatcher.Invoke(new Action(() =>
                //    {

                //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning copy.png"));
                //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯失败";
                //        MainWindowViewModel.GetInstance().ErrorMsg = "PLC连接错误！";
                //        MainWindowViewModel.GetInstance().IsConnect = false;
                //    }));
                //}
                //#endregion
                //MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                IsConnectedTask = false;
                OnDisconnected?.Invoke(); //通讯失败
            }

            return -1;
        }

        #endregion

        #region 写入DT和DDT类型，只读取一个

        private bool PLC_WD(string beginAddress, int value)
        {
            try
            {
                string address = "", endAddress = "", Data = "";
                if (beginAddress.Substring(0, 2) == "DT")
                {
                    address = "D" + beginAddress.Substring(2, beginAddress.Length - 2);
                    endAddress = address.Substring(1, address.Length - 1);
                    Data = ConvertDecToHex(value.ToString(), 16);
                }
                else if (beginAddress.Substring(0, 3) == "DDT")
                {
                    address = "D" + beginAddress.Substring(3, beginAddress.Length - 3);
                    endAddress = ((Convert.ToInt32(address.Substring(1, address.Length - 1))) + 1).ToString();
                    Data = ConvertDecToHex(value.ToString(), 32);
                }

                string command = "%01#WD" + address + endAddress + Data + "**\r";
                lock (_locker)
                {
                    _serialPort.Write(command);
                    string? receive = _serialPort.ReadTo("\x0d");
                    if (receive[3] == '$')
                    {
                        #region 更新状态栏和PLC连接标志

                        //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯失败")
                        //{
                        //    if (App.Current == null)
                        //    {
                        //        return false;
                        //    }
                        //    App.Current.Dispatcher.Invoke(new Action(() =>
                        //    {
                        //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
                        //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯成功";
                        //        MainWindowViewModel.GetInstance().IsConnect = true;
                        //    }));
                        //}
                        OnConnectedSucess();

                        #endregion

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                //#region 更新状态栏和PLC连接标志并弹窗
                //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯成功")
                //{
                //    if (App.Current == null)
                //    {
                //        return false;
                //    }
                //    App.Current.Dispatcher.Invoke(new Action(() =>
                //    {
                //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning copy.png"));
                //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯失败";
                //        MainWindowViewModel.GetInstance().ErrorMsg = "PLC连接错误！";
                //        MainWindowViewModel.GetInstance().IsConnect = false;
                //    }));
                //}
                //#endregion
                //MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                OnConnectedFail();
            }

            return false;
        }

        private void OnConnectedFail()
        {
            IsConnectedTask = false;
            OnDisconnected?.Invoke(); //通讯失败
        }

        private void OnConnectedSucess()
        {
            IsConnectedTask = true;
            OnConnected?.Invoke(); //通讯成功
        }

        #endregion

        #region 读取R类型数组，beginAddress就是开始地址，size就是读取多少个

        public int[] PLC_RCP(string beginAddress, int size)
        {
            if (size <= 0)
            {
                throw new Exception("传入的长度有误!");
            }

            try
            {
                // R181
                string address = "";
                int begin = int.Parse(beginAddress.Substring(1, beginAddress.Length - 1)); // 181
                for (int i = 0; i < size; ++i)
                {
                    address += GetRealAddr("R" + (begin + i).ToString(), 5);
                }

                string command = "%01#RCP" + size.ToString() + address + "**\r";
                lock (_locker)
                {
                    _serialPort.Write(command);
                    string? receive = _serialPort.ReadTo("\x0d");
                    if (receive[3] == '$')
                    {
                        #region 更新状态栏和PLC连接标志

                        OnConnectedSucess();

                        #endregion

                        int[] ans = new int[size];
                        int k = 0;
                        // 6, receive[:-2]
                        // 6, 6 + size
                        for (int i = 6; i < 6 + size; ++i)
                        {
                            ans[k++] = receive[i] == '0' ? 0 : 1;
                        }

                        return ans;
                    }
                }
            }
            catch (Exception e)
            {
                #region 更新状态栏和PLC连接标志并弹窗

                //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯成功")
                //{
                //    if (App.Current == null)
                //    {
                //        return new int[0];
                //    }
                //    App.Current.Dispatcher.Invoke(new Action(() =>
                //    {
                //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning copy.png"));
                //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯失败";
                //        MainWindowViewModel.GetInstance().ErrorMsg = "PLC连接错误！";
                //        MainWindowViewModel.GetInstance().IsConnect = false;
                //    }));
                //}
                OnConnectedFail();

                #endregion
            }

            return new int[0]; // 返回空数组
        }

        #endregion

        #region 读取DT和DDT类型，beginAddress就是开始地址，size就是需要读取多少个

        private int[] PLC_RDArray(string beginAddress, int size)
        {
            if (size <= 0)
            {
                throw new Exception("传入的长度有误!");
            }

            try
            {
                string address = "", endAddress = "";
                if (beginAddress.StartsWith("DT"))
                {
                    address = "D" + beginAddress.Substring(2, beginAddress.Length - 2);
                    endAddress = string.Format("{0:D5}",
                        Convert.ToInt32(address.Substring(1, address.Length - 1)) + size - 1);
                }
                else if (beginAddress.StartsWith("DDT"))
                {
                    address = "D" + beginAddress.Substring(3, beginAddress.Length - 3);
                    endAddress = ((Convert.ToInt32(address.Substring(1, address.Length - 1))) + 2 * size - 1)
                        .ToString();
                }

                string command = "%01#RD" + address + endAddress + "**\r";

                lock (_locker)
                {
                    _serialPort.Write(command);
                    string? receive = _serialPort.ReadTo("\x0d");

                    if (receive[3] == '$')
                    {
                        #region 更新状态栏和PLC连接标志

                        //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯失败")
                        //{
                        //    if (App.Current == null)
                        //    {
                        //        return new int[0];
                        //    }
                        //    App.Current.Dispatcher.Invoke(new Action(() =>
                        //    {
                        //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
                        //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯成功";
                        //        MainWindowViewModel.GetInstance().IsConnect = true;
                        //    }));
                        //}
                        OnConnectedSucess();

                        #endregion

                        int[] ans = new int[size];
                        // 6, receive[:-2]
                        if (beginAddress.StartsWith("DT"))
                        {
                            // 每4个字符就是一个答案
                            string input = receive.Substring(6, receive.Length - 8);
                            for (int i = 0, k = 0; i < input.Length; i += 4, ++k)
                            {
                                ans[k] = ConvertHexToDec(input.Substring(i, 4), 16);
                            }
                        }
                        else if (beginAddress.StartsWith("DDT"))
                        {
                            // 这个实时显示的那个线程的。
                            string input = receive.Substring(6, receive.Length - 8);
                            for (int i = 0, k = 0; i < input.Length; i += 8, ++k)
                            {
                                ans[k] = ConvertHexToDec(input.Substring(i, 8), 32);
                            }
                        }

                        return ans;
                    }
                }

            }
            catch (Exception ex)
            {
                //#region 更新状态栏和PLC连接标志并弹窗
                //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯成功")
                //{
                //    if (App.Current == null)
                //    {
                //        return new int[0];
                //    }
                //    App.Current.Dispatcher.Invoke(new Action(() =>
                //    {

                //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning copy.png"));
                //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯失败";
                //        MainWindowViewModel.GetInstance().ErrorMsg = "PLC连接错误！";
                //        MainWindowViewModel.GetInstance().IsConnect = false;
                //    }));
                //}
                //#endregion
                //MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                OnConnectedFail();
            }

            return new int[0]; // 返回空数组
        }

        #endregion

        #region 写入，需要传入一个数组

        private bool PLC_WDArray(string beginAddress, int[] WriteData)
        {
            int size = WriteData.Length;
            if (size <= 0)
            {
                throw new Exception("传入的长度有误!");
            }

            try
            {
                string address = "", endAddress = "", Data = "";
                if (beginAddress.StartsWith("DT"))
                {
                    address = "D" + beginAddress.Substring(2, beginAddress.Length - 2);
                    endAddress = string.Format("{0:D5}",
                        Convert.ToInt32(address.Substring(1, address.Length - 1)) + size - 1);
                    //endAddress = (.ToString();
                    // 将数字转成4位16进制
                    foreach (int number in WriteData)
                    {
                        Data += ConvertDecToHex(number.ToString(), 16);
                    }
                }
                else if (beginAddress.StartsWith("DDT"))
                {
                    address = "D" + beginAddress.Substring(3, beginAddress.Length - 3);
                    endAddress = ((Convert.ToInt32(address.Substring(1, address.Length - 1))) + 2 * size - 1)
                        .ToString();
                    // 将数字转成8位16进制
                    foreach (int number in WriteData)
                    {
                        Data += ConvertDecToHex(number.ToString(), 32);
                    }
                }

                string command = "%01#WD" + address + endAddress + Data + "**\r";
                lock (_locker)
                {
                    _serialPort.Write(command);
                    string? receive = _serialPort.ReadTo("\x0d");
                    if (receive[3] == '$')
                    {
                        #region 更新状态栏和PLC连接标志

                        //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯失败")
                        //{
                        //    if (App.Current == null)
                        //    {
                        //        return false;
                        //    }
                        //    App.Current.Dispatcher.Invoke(new Action(() =>
                        //    {
                        //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
                        //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯成功";
                        //        MainWindowViewModel.GetInstance().IsConnect = true;
                        //    }));
                        //}
                        OnConnectedSucess();

                        #endregion

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                //#region 更新状态栏和PLC连接标志并弹窗
                //if (CommunicationStateViewModel.GetInstance().CommunicationStateText == "通讯成功")
                //{
                //    if (App.Current == null)
                //    {
                //        return false;
                //    }
                //    App.Current.Dispatcher.Invoke(new Action(() =>
                //    {

                //        CommunicationStateViewModel.GetInstance().CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning copy.png"));
                //        CommunicationStateViewModel.GetInstance().CommunicationStateText = "通讯失败";
                //        MainWindowViewModel.GetInstance().ErrorMsg = "PLC连接错误！";
                //        MainWindowViewModel.GetInstance().IsConnect = false;
                //    }));
                //}
                //#endregion
                //MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
                OnConnectedFail();
            }

            return false;
        }

        #endregion

        #endregion
        #endregion
    }
}

