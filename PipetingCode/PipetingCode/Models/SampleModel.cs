using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Base;

namespace PipetitngCode.Models
{
    // 样本，4个状态，未处理，进行中，已完成，质检
    public class SampleModel : NotifiactionObject
    {
        private void Update()
        {
            if (IsChecked)
            {
                Color = "yellow";
                return;
            }
            if (Error)
            {
                Color = "#FF0000";
                return;
            }
            if (Completed)
            {
                Color = "#16FF00";
                return;
            }
            Color = On ? "#00BCFF" : "#D8D8D8";
        }

        // 是否出错
        private bool _Error;

        public bool Error
        {
            get { return _Error; }
            set
            {
                _Error = value;
                Update();
            }
        }

        private bool _on;

        // 是否进行中
        public bool On
        {
            get
            {
                return _on;
            }
            set
            {
                _on = value;
                Update();
            }
        }

        // 是否已完成
        private bool _completed;

        public bool Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                Update();
            }
        }


        // 是否质检
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                Update();
            }
        }

        // 根据4个状态得到的颜色
        private string _color;
        public string Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
                RaisePropertyChanged("Color");
            }
        }
    }
}
