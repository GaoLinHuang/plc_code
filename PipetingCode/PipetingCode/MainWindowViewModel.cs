using System;
using Windows.Base;

namespace PipettingCode
{
    internal class MainWindowViewModel : SingletonNotifyBase<MainWindowViewModel>
    {
        // 终止实验
        private bool _KillExperiment = false;

        public bool KillExperiment
        {
            get => _KillExperiment;
            set => SetField(ref _KillExperiment, value);
        }

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
    }
}