using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Base;
using PipettingCode.Common;

namespace PipettingCode.Services
{
    /// <summary>
    /// 四管实现类
    /// </summary>
    public class PipeProcessFour : IPipeProcess, IProcessStatus
    {
        #region 字段
        /// <summary>
        /// 运行中
        /// </summary>
        private bool _isRunning;
        /// <summary>
        /// 正在停止
        /// </summary>
        private bool _isStoping = false;
        /// <summary>
        /// 当前流程名称
        /// </summary>
        private string _processName;
        /// <summary>
        /// 配置服务
        /// </summary>
        private ProcessConfigService _configService;
        /// <summary>
        /// 流程执行信号量
        /// </summary>
        SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        /// <summary>
        /// 用于通知停止的信号量
        /// </summary>
        AutoResetEvent _stopResetEvent = new AutoResetEvent(false);

        ///

        private event Action<string> _processStatusCallBack; 
        #endregion
        #region 构造函数
        public PipeProcessFour(ProcessConfigService processConfig)
        {
            _configService = processConfig;
        } 
        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            try
            {
                if (_isRunning)
                {
                    return;
                }
                //连接
                //Reset();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
        /// <summary>
        /// 重置  主要是做加样臂的节点搜索，初始化等操作
        /// </summary>
        private void Reset()
        {
            Console.WriteLine("设置复位");
        }

        /// <summary>
        /// 执行流程
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public async Task StartProcess(string processName)
        {
            try
            {
                await Task.Delay(10);
                await _semaphore.WaitAsync();
                if (_isRunning)
                {
                    Console.WriteLine($"{_processName} 正在执行");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"====== {processName} 即将开始....");
                Reset();
                _isRunning = true;
                _processName = processName;
                ExecuteProcess(_processName);
                Console.WriteLine($"==================={_processName} 正在执行");
            }
            catch (Exception e)
            {
                MainSingletonService.Instance.Log.Error(e);
            }
            finally
            {
                _semaphore.Release();
            }

        }

        private void ExecuteProcess(string processName)
        {
            _ = Task.Run(async () =>
            {
                try
                {

                    //获取配置
                    List<ConfigInfoItem> configs = _configService.GetConfigInfos(processName);
                    bool notifyStop = false;//监听当前执行流程是否请求取消
                    while (_isRunning && !notifyStop)
                    {
                        foreach (ConfigInfoItem configInfo in configs)
                        {
                            var execute = ExecuteManager.Instance.GetExecute(configInfo.Id);
                            if (execute!=null)
                            {
                                await execute.ExecuteAsync(configInfo);
                            }

                            string msg = $"执行步骤：{configInfo.Id}";
                            Console.WriteLine(msg);
                            _processStatusCallBack?.Invoke(msg);
                            //await Task.Delay(configInfo.ContinueTime);
                            if (_isStoping)
                            {
                                notifyStop = await TryStop();
                                break;
                            }
                        }

                        await Task.Delay(3000);
                        if (_isStoping)
                        {
                            notifyStop = await TryStop();
                            break;
                        }
                    }

                    Console.WriteLine("流程终止=====================");
                }
                catch (Exception e)
                {
                    MainSingletonService.Instance.Log.Error(e);
                }
            });
        }

        /// <summary>
        /// 尝试停止
        /// </summary>
        /// <returns></returns>
        private async Task<bool> TryStop()
        {
            Console.WriteLine("停止.....");
            await Task.Delay(3000);//模拟耗时
            _isStoping = false;
            _stopResetEvent.Set();
            return true;
        }

        /// <summary>
        /// 停止流程
        /// </summary>
        /// <returns></returns>
        public Task StopCurrentProcess()
        {
            return Task.Run(() =>
              {

                  if (!_isRunning || _isStoping)
                  {
                      if (_isStoping)
                      {
                          Console.WriteLine("停止中....");
                      }

                      if (!_isRunning)
                      {
                          Console.WriteLine("未运行...");
                      }
                      return;
                  }
                  _isStoping = true;
                  Console.WriteLine("通知停止");
                  _stopResetEvent.WaitOne();
                  Reset();
                  _isRunning = false;
              });

        }

        /// <summary>
        /// 注册状态回调
        /// </summary>
        /// <param name="onCallBack"></param>
        public void RegisterStatusCallBack(Action<string> onCallBack)
        {
            _processStatusCallBack += onCallBack;
        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="msg"></param>
        public void NotifyMsg(string msg)
        {
            _processStatusCallBack?.Invoke(msg);
        }
    }
}