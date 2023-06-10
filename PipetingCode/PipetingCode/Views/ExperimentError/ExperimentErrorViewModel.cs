using System;
using Windows.Base;

namespace PipettingCode.Views
{
    public class ExperimentErrorViewModel : NotifyBase
    {
        private static ExperimentErrorViewModel _instance = new();

        public enum ErrorCode
        {
            Init,
            Stop,
            ReTry,
            InitialReTry,
            Ignore,
            Cancel,
            OK
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

        private string _ErrorContext;

        public string ErrorContext
        {
            get { return _ErrorContext; }
            set
            {
                _ErrorContext = value;
                RaisePropertyChanged("ErrorContext");
            }
        }

        #region 选择的是哪一个命令

        public ErrorCode ErrorSingle { get; set; }

        #endregion 选择的是哪一个命令

        #region 命令

        public DelegateCommand StopCommand { get; set; }

        public DelegateCommand ReTryCommand { get; set; }

        public DelegateCommand InitialReTryCommand { get; set; }

        public DelegateCommand IgnoreCommand { get; set; }
        public DelegateCommand CancleCommand { get; set; }
        public DelegateCommand OKCommand { get; set; }

        #endregion 命令

        private void Stop(object parameter)
        {
            this.ErrorSingle = ErrorCode.Stop;
        }

        private void ReTry(object parameter)
        {
            this.ErrorSingle = ErrorCode.ReTry;
        }

        private void InitialReTry(object parameter)
        {
            this.ErrorSingle = ErrorCode.InitialReTry;
        }

        private void Ignore(object parameter)
        {
            this.ErrorSingle = ErrorCode.Ignore;
        }

        private void Cancle(object parameter)
        {
            this.ErrorSingle = ErrorCode.Cancel;
        }

        private void OK(object parameter)
        {
            this.ErrorSingle = ErrorCode.OK;
        }

        private ExperimentErrorViewModel()
        {
            StopCommand = new DelegateCommand()
            {
                ExecuteAction = new Action<object>(this.Stop)
            };
            ReTryCommand = new DelegateCommand()
            {
                ExecuteAction = new Action<object>(this.ReTry)
            };
            InitialReTryCommand = new DelegateCommand()
            {
                ExecuteAction = new Action<object>(this.InitialReTry)
            };
            IgnoreCommand = new DelegateCommand()
            {
                ExecuteAction = new Action<object>(this.Ignore)
            };
            CancleCommand = new()
            {
                ExecuteAction = new Action<object>(this.Cancle)
            };
            OKCommand = new()
            {
                ExecuteAction = new Action<object>(this.OK)
            };
            this.ErrorSingle = ErrorCode.Init;
        }

        public static ExperimentErrorViewModel GetInstance()
        {
            return _instance;
        }
    }
}