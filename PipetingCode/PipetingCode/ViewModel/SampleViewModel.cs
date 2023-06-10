using PipetitngCode.Views;
using System;
using System.Collections.ObjectModel;
using Windows.Base;
using PipetitngCode.Models;

namespace PipetitngCode.ViewModel
{
    [Serializable]
    public class SampleViewModel : NotifiactionObject
    {
        // 单例
        private static SampleViewModel _instance = new SampleViewModel();

        // 96 * 1，移液这一边的
        private ObservableCollection<SampleModel> _sample96;

        public ObservableCollection<SampleModel> Sample96
        {
            get { return _sample96; }
            set
            {
                _sample96 = value;
                RaisePropertyChanged("Sample96");
            }
        }

        // 96 * 1, 试管架，开盖那一边的
        private ObservableCollection<SampleModel> _shiGuanJia;

        public ObservableCollection<SampleModel> ShiGuanJia
        {
            get { return _shiGuanJia; }
            set
            {
                _shiGuanJia = value;
                RaisePropertyChanged("ShiGuanJia");
            }
        }


        // 16 * 6，移液这一边的
        private ObservableCollection<SampleModel> _sample16;
        public ObservableCollection<SampleModel> Sample16
        {
            get { return _sample16; }
            set
            {
                _sample16 = value;
                RaisePropertyChanged("Sample16");
            }
        }

        public void Reset()
        {
            try
            {
                // 96 * 1，移液
                for (int i = 0; i < 96; ++i)
                {
                    Sample96[i].On = false;
                    Sample96[i].IsChecked = false;
                    Sample96[i].Completed = false;
                    Sample96[i].Error = false;
                }
                // 96 * 1，开盖
                for (int i = 0; i < 96; ++i)
                {
                    ShiGuanJia[i].On = false;
                    ShiGuanJia[i].IsChecked = false;
                    ShiGuanJia[i].Completed = false;
                    ShiGuanJia[i].Error = false;
                }

                // 16 * 6，移液
                for (int i = 0; i < 96; ++i)
                {
                    Sample16[i].On = false;
                    Sample16[i].IsChecked = false;
                    Sample16[i].Completed = false;
                    Sample16[i].Error = false;
                }
            }
            catch (Exception ex)
            {
                MySettingWindow.SaveLog(MySettingWindow.ErrorLog, ex.StackTrace + "\n" + ex.ToString());     // 保存错误日志
            }
        }
        private void Init()
        {
            // 96 * 1，移液
            Sample96 = new ObservableCollection<SampleModel>();
            for (int i = 0; i < 96; ++i)
            {
                Sample96.Add(new SampleModel()
                {
                    On = false,
                    Completed = false,
                    IsChecked = false,
                    Error = false
                });
            }
            // 96 * 1，开盖
            ShiGuanJia = new ObservableCollection<SampleModel>();
            for (int i = 0; i < 96; ++i)
            {
                ShiGuanJia.Add(new SampleModel()
                {
                    On = false,
                    Completed = false,
                    IsChecked = false,
                    Error = false
                });
            }

            // 16 * 6，移液
            Sample16 = new ObservableCollection<SampleModel>();
            for (int j = 0; j < 96; ++j)
            {
                Sample16.Add(new SampleModel()
                {
                    On = false,
                    Completed = false,
                    IsChecked = false,
                    Error = false
                });
            }
        }


        private SampleViewModel()
        {
            Init();
        }
        public static SampleViewModel GetInstance()
        {
            return _instance;
        }

    }
}
