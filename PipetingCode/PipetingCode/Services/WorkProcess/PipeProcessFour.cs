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
        private string _processName;
        private ProcessConfigService _configService;
        SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        public PipeProcessFour(ProcessConfigService processConfig)
        {
            _configService = processConfig;
        }

        public async Task InitAsync()
        {
            try
            {
                await _semaphore.WaitAsync();
                if (_isRunning)
                {
                    return;
                }
                //连接
                Reset();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _semaphore.Release();
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

                    while (_isRunning)
                    {
                        foreach (ConfigInfoItem configInfo in configs)
                        {
                            Console.WriteLine($"执行步骤：{configInfo.Id}");
                            await Task.Delay(configInfo.ContinueTime);
                            if (!_isRunning)
                            {
                                Reset();
                                break;
                            }
                        }

                        Console.WriteLine("===================================");
                        await Task.Delay(1000);
                    }
                }
                catch (Exception e)
                {
                    MainSingletonService.Instance.Log.Error(e);
                }
            });
        }

        public void StopCurrentProcess()
        {
            _isRunning = false;
        }
    }
}