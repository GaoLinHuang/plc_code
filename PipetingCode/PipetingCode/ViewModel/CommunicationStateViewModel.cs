using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Windows.Base;

namespace PipettingCode.ViewModel
{
    public class CommunicationStateViewModel : SingletonNotifyBase<CommunicationStateViewModel>
    {
        #region 通讯失败或者通讯成功

        private string _communicationStateText;

        public string CommunicationStateText
        {
            get { return _communicationStateText; }
            set
            {
                _communicationStateText = value;
                RaisePropertyChanged("CommunicationStateText");
            }
        }

        #endregion 通讯失败或者通讯成功

        #region 图片

        private BitmapImage _communicationStatePicture;

        public BitmapImage CommunicationStatePicture
        {
            get { return _communicationStatePicture; }
            set
            {
                _communicationStatePicture = value;
                RaisePropertyChanged("CommunicationStatePicture");
            }
        }

        #endregion 图片

        #region Settings是否可见

        private Visibility _CanSeeSettings;

        public Visibility CanSeeSettings
        {
            get { return _CanSeeSettings; }
            set
            {
                _CanSeeSettings = value;
                RaisePropertyChanged("CanSeeSettings");
            }
        }

        #endregion Settings是否可见

        public CommunicationStateViewModel()
        {
            // 默认值
            CanSeeSettings = Visibility.Hidden;
            CommunicationStateText = "通讯成功";
            CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
        }
    }
}