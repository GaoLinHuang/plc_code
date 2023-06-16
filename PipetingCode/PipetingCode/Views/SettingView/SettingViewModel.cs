using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Base;
using PipettingCode.Services;

namespace PipettingCode.Views
{
    public class SettingViewModel : NotifyBase
    {

        public SettingViewModel()
        {
            ExtendsConfig = ProcessConfigService.Instance.GetExtendsConfig();

            //ProcessConfigService.Instance.Update(ExtendsConfig);
        }
        private ExtendsConfig _endsConfig;
        public ExtendsConfig ExtendsConfig
        {
            get => _endsConfig;
            set => SetField(ref _endsConfig, value);
        }
    }
}
