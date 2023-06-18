using PipettingCode.Common;
using PipettingCode.Services.Config;
using PipettingCode.Views;
using PipettingControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipettingCode.Services
{
    internal class Step20 : IExecute
    {
        public  async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine($"步骤20：吸取{config.WashCapacitySecond}ul的洗脱液到新的96孔板中");
            var res = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return MessageBox.Show($"步骤20：吸取{config.WashCapacitySecond}ul的洗脱液到新的96孔板中", "换针提示", MessageBoxButton.OKCancel);

            });

            if (res == MessageBoxResult.Cancel)
            {
                return false;
            }

            Global_Parameter.TubersStartX = ProcessConfigService.Instance.GetExtendsConfig().WashLeft;
            Global_Parameter.TubersStartY = ProcessConfigService.Instance.GetExtendsConfig().WashTop;
            Global_Parameter.TubersEndX = ProcessConfigService.Instance.GetExtendsConfig().WashRight;
            Global_Parameter.TubersEndY = ProcessConfigService.Instance.GetExtendsConfig().WashBottom;

            for (int i = 0; i < GlobalConfig.OrificePlateErgodicCount; i++)//96孔是4*24  也就是24次
            {
                //取针
                PipettingViewModel.Instance.TakeNeedle(i);

                //吸液
                PipettingViewModel.Instance.Imbibition(i,(int)config.WashCapacitySecond);
                // 吐液
                PipettingViewModel.Instance.Injection(i,1);//磁力板位置设置成1
                PipettingViewModel.Instance.OffNeedle(0);
            }

            return await Task.FromResult(true);
        }
    }
}
