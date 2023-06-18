using PipettingCode.Common;
using PipettingCode.Services.Config;
using PipettingCode.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PipettingControl;

namespace PipettingCode.Services
{
    internal class Step8 : IExecute
    {
        protected virtual string Prefix { get; } = "步骤8：";

        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine("加入200ul乙醇，静置30S");
            var res = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return MessageBox.Show($"{Prefix}加入200ul乙醇，静置30S.确保有针头", "换针提示", MessageBoxButton.OKCancel);

            });

            if (res == MessageBoxResult.Cancel)
            {
                return false;
            }

            Global_Parameter.TubersStartX = ProcessConfigService.Instance.GetExtendsConfig().EthanolLeft;
            Global_Parameter.TubersStartY = ProcessConfigService.Instance.GetExtendsConfig().EthanolTop;
            Global_Parameter.TubersEndX = ProcessConfigService.Instance.GetExtendsConfig().EthanolRight;
            Global_Parameter.TubersEndY = ProcessConfigService.Instance.GetExtendsConfig().EthanolBottom;

            for (int i = 0; i < GlobalConfig.OrificePlateErgodicCount; i++)//96孔是4*24  也就是24次
            {
                //取针
                PipettingViewModel.Instance.TakeNeedle(i);

                //吸液
                PipettingViewModel.Instance.Imbibition(i,(int)config.EthanolCapacityFirst);
                // 吐液
                PipettingViewModel.Instance.Injection(i,1);//磁力板位置设置成1

                //脱针
                PipettingViewModel.Instance.OffNeedle(0);
            }

            return await Task.FromResult(true);
        }
    }
}
