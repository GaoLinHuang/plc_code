using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PipettingCode.Services.Config;
using PipettingCode.Views;
using PipettingControl;

namespace PipettingCode.Services
{
    public class Step1 : IExecute
    {
        /// <summary>
        /// 磁珠充分混匀
        /// </summary>
        /// <param name="configItem"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            //调用厂家提供的接口
            var res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show("步骤1：磁珠充分混匀.确保有针头", "换针提示", MessageBoxButton.OKCancel));
            if (res == MessageBoxResult.Cancel)
            {
                return false;
            }

            var result = PipettingViewModel.Instance.TakeNeedle(0);
            if (result == -1)//取针失败
            {
                return false;
            }

            //await Task.WhenAll(PipettingViewModel.Instance.MoveX(configItem.X),
            //    PipettingViewModel.Instance.MoveY(configItem.Y));//移动到磁珠位置


            Global_Parameter.TubersStartX = ProcessConfigService.Instance.GetExtendsConfig().MagneticBeadLeft;
            Global_Parameter.TubersStartY = ProcessConfigService.Instance.GetExtendsConfig().MagneticBeadTop;
            Global_Parameter.TubersEndX = ProcessConfigService.Instance.GetExtendsConfig().MagneticBeadRight;
            Global_Parameter.TubersEndY = ProcessConfigService.Instance.GetExtendsConfig().MagneticBeadBottom;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    //吸液
                    PipettingViewModel.Instance.Imbibition(0);//磁珠
                }
                else
                {
                    PipettingViewModel.Instance.Pipetting_Imbibition(i);//磁珠
                }

                await Task.Delay(1000);
                //移动Z轴
                var brine1MoveZ = Global_Parameter.Brine1MaxZ;
                //await PipettingViewModel.Instance.MoveZ(800);
                // 吐液
                PipettingViewModel.Instance.Pipetting_Injection(0);//注液
                await Task.Delay(200);
            }

            PipettingViewModel.Instance.OffNeedle(0);//脱针

            //执行流程
            return await Task.FromResult(true);
        }
    }

}
