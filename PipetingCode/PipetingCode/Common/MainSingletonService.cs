using Common.Log;
using PipettingCode.Services;
using System;
using Windows.Base;

namespace PipettingCode.Common
{
    internal class MainSingletonService : Singleton<MainSingletonService>
    {
        public MainSingletonService()
        {
            Console.WriteLine("new MainSingletonService()");
            Log = new Logger("Pipetitng");
            PipeProcess = new PipeProcessFour(ProcessConfigService.Instance);
        }

        public ILogger Log { get; }

        public IPipeProcess PipeProcess { get; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public IProcessStatus ProcessStatus => PipeProcess as IProcessStatus;
    }
}