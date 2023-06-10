using PipettingCode.Common;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Base;
using PipettingCode.Services;
using PipettingCode.Services.Config;

namespace PipettingCode
{
    internal class MainWindowViewModel : SingletonNotifyBase<MainWindowViewModel>
    {
        public MainWindowViewModel()
        {
            ExecuteProcessCommand = new DelegateCommand(OnExecuteProcess);
            StopExecuteProcessCommand = new DelegateCommand(OnStopExecuteProcess);
            ConfigInfos=new ObservableCollection<ConfigInfo>(ProcessConfigService.Instance.GetConfigInfos());
        }

        private void OnStopExecuteProcess(object obj)
        {
            MainSingletonService.Instance.PipeProcess.StopProcess();
        }

        private async void OnExecuteProcess(object obj)
        {
            await MainSingletonService.Instance.PipeProcess.InitAsync();
            await MainSingletonService.Instance.PipeProcess.StartProcess(SelectItem.Key);
        }

        // 终止实验
        private bool _KillExperiment = false;

        public bool KillExperiment
        {
            get => _KillExperiment;
            set => SetField(ref _KillExperiment, value);
        }

        public ObservableCollection<ConfigInfo> ConfigInfos { get; }


        public ConfigInfo SelectItem { get; set; }

        public bool IsConnect { get; set; } = false;

        public void Init()
        {
            Console.WriteLine("init");
        }

        private string _ErrorMsg;

        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set
            {
                _ErrorMsg = value;
                RaisePropertyChanged("ErrorMsg");
            }
        }

        public ICommand ExecuteProcessCommand { get; }
        public ICommand StopExecuteProcessCommand { get; }
    }
}