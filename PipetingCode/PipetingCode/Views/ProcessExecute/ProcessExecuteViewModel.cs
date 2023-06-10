using PipettingCode.Services.Config;
using PipettingCode.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Base;
using PipettingCode.Common;

namespace PipettingCode.Views
{
    internal class ProcessExecuteViewModel : NotifyBase
    {
        public ProcessExecuteViewModel()
        {
            ExecuteProcessCommand = new DelegateCommand(OnExecuteProcess);
            StopExecuteProcessCommand = new DelegateCommand(OnStopExecuteProcess);
            ConfigInfos = new ObservableCollection<ConfigInfo>(ProcessConfigService.Instance.GetConfigInfos());
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="obj"></param>
        private void OnStopExecuteProcess(object obj)
        {
            MainSingletonService.Instance.PipeProcess.StopCurrentProcess();
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="obj"></param>

        private async void OnExecuteProcess(object obj)
        {
            await MainSingletonService.Instance.PipeProcess.InitAsync();
            await MainSingletonService.Instance.PipeProcess.StartProcess(SelectItem.Key);
        }
        /// <summary>
        /// 流程列表
        /// </summary>
        public ObservableCollection<ConfigInfo> ConfigInfos { get; }
        /// <summary>
        /// 当前选中的流程
        /// </summary>
        public ConfigInfo SelectItem { get; set; }
        /// <summary>
        /// 开始执行
        /// </summary>
        public ICommand ExecuteProcessCommand { get; }
        /// <summary>
        /// 停止
        /// </summary>
        public ICommand StopExecuteProcessCommand { get; }
    }
}
