using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Base;
using PipettingCode.Services;
using PipettingCode.Services.Config;

namespace PipettingCode.Views
{
    public class SettingViewModel : NotifyBase
    {

        public SettingViewModel()
        {
            SaveConfigCommand = new DelegateCommand(OnSaveConfig);
            ExtendsConfig = ProcessConfigService.Instance.GetExtendsConfig();
            ConfigInfos = new ObservableCollection<ConfigInfo>(ProcessConfigService.Instance.GetConfigInfos());
            //ProcessConfigService.Instance.Update(ExtendsConfig);
        }

        private void OnSaveConfig(object obj)
        {
            ProcessConfigService.Instance.Update(ExtendsConfig);
            ProcessConfigService.Instance.UpdateConfig(SelectItem);
            Console.WriteLine();
        }

        private ExtendsConfig _endsConfig;
        public ExtendsConfig ExtendsConfig
        {
            get => _endsConfig;
            set => SetField(ref _endsConfig, value);
        }

        /// <summary>
        /// 流程列表
        /// </summary>
        public ObservableCollection<ConfigInfo> ConfigInfos { get; }

        private ConfigInfo _selectItem;
        /// <summary>
        /// 当前选中的流程
        /// </summary>
        public ConfigInfo SelectItem
        {
            get => _selectItem;
            set => SetField(ref _selectItem, value);
        }


        public ICommand SaveConfigCommand { get; }
    }
}
