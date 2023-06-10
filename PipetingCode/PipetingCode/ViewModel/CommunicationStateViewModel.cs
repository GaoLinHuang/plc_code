using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using Windows.Base;

namespace PipetitngCode.ViewModel
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
        #endregion

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
        #endregion

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
        #endregion

        public CommunicationStateViewModel()
        {
            // 默认值
            CanSeeSettings = Visibility.Hidden;
            CommunicationStateText = "通讯成功";
            CommunicationStatePicture = new BitmapImage(new Uri("pack://application:,,,/image/ic_warning.png"));
        }

    }


}
