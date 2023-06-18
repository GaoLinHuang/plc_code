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
    internal class Step15 : IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine($"步骤15：加入{config.WashCapacityFirst}ul洗脱液");
            var res = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return MessageBox.Show($"步骤15：加入{config.WashCapacityFirst}ul洗脱液.确保有针头", "换针提示", MessageBoxButton.OKCancel);

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
                PipettingViewModel.Instance.Imbibition(i,(int)config.WashCapacityFirst);//洗脱液
                // 吐液
                PipettingViewModel.Instance.Injection(i);

                Console.WriteLine("步骤16：上下吸打10次以上达到充分混匀");
                //步骤16
                for (int j = 0; j < config.SampleBeadTimes; j++)
                {
                    //吸液
                    PipettingViewModel.Instance.Pipetting_Imbibition(i);
                    // 吐液
                    PipettingViewModel.Instance.Pipetting_Injection(i);
                }
                //脱针
                PipettingViewModel.Instance.OffNeedle(0);
            }

            return await Task.FromResult(true);
        }
    }
}

