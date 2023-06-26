using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core
{
    public class ExperimentManager
    {
        private CancellationTokenSource _cancellationTokenSource;
        private Pipe _pipe;
        private TurnManager _turnManager;
        private Scanner _scanner;
        public event Func<string, string, Task> OnHandlerMessage; 
        public ExperimentManager()
        {
            _pipe = new Pipe();
            _turnManager = new TurnManager(ProviderManager.PlcManager);
        }

        public async Task InitAsync()
        {
            _pipe.InitAsync();
        }
        public async Task StartExperimentAsync()
        {
            _cancellationTokenSource?.Cancel();

            var takeNeedleTask = _pipe.TakeNeedleAsync();
            //转动原液盘
            var turnSourceLiquidTask = _turnManager.StartTurnSourceLiquid();
            await Task.WhenAll(takeNeedleTask, turnSourceLiquidTask);

            var scanInfos = _scanner.GetScanInfos();

            // 页面探测

            // 页面和PLC获取到的管长比对

            // 满足条件才能往下走

            List<Task> allTask = new List<Task>();
            foreach (var info in scanInfos)
            {
                allTask.Add(Task.Run(async () =>
                {
                    var scanInfo = info;
                    var splitCupInfo = await new PlatformServices().GetSplitCupInfo();

                    int count = 0;
                    while (splitCupInfo.Code != 200 && (count++) < 5)
                    {
                        //尝试调用后台
                        await Task.Delay(100);
                    }

                    if (splitCupInfo.Code != 200)
                    {
                        //提示当前流程因什么原因中断
                        return;//终止剩下的流程
                    }

                    var printerProvider = new PrinterProvider();
                    List<Task> tasks = new List<Task>()
                    {
                            _pipe.SeparationCupAsync(),//分杯操作
                            printerProvider.PrintAsync(new PrintInfo())//打印操作
                    };
                    await Task.WhenAll(tasks);//并行
                }));
            }

            await Task.WhenAll(allTask);//等待所有的步骤执行完成
            //脱针
            await _pipe.OffNeedleAsync();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <returns></returns>
        public async Task PauseExperimentAsync()
        {
            await ProviderManager.ProcessManage.PauseAsync();
        }
    }
}
