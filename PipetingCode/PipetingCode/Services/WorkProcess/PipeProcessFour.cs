using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Base;
using PipettingCode.Common;

namespace PipettingCode.Services
{
    public class PipeProcessFour : IPipeProcess
    {
        private bool _isRunning;
        private bool _isStoping = false;
        private string _processName;
        private ProcessConfigService _configService;
        SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        AutoResetEvent _stopResetEvent = new AutoResetEvent(false);
        public PipeProcessFour(ProcessConfigService processConfig)
        {
            _configService = processConfig;
        }

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

        private void Reset()
        {
            Console.WriteLine("设置复位");
        }

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

                    List<ConfigInfoItem> configs = _configService.GetConfigInfos(processName);

                    var json = configs.ToJson();
                    bool notifyStop = false;
                    while (_isRunning && !notifyStop)
                    {
                        foreach (ConfigInfoItem configInfo in configs)
                        {
                            Console.WriteLine($"执行步骤：{configInfo.Id}");
                            await Task.Delay(configInfo.ContinueTime);
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

        private async Task<bool> TryStop()
        {
            Console.WriteLine("停止.....");
            await Task.Delay(10000);
            _isStoping = false;
            _stopResetEvent.Set();
            return true;
        }

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
    }
}