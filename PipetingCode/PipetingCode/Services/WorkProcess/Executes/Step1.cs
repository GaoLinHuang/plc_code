using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PipettingCode.Services.Config;
using PipettingCode.Views;

namespace PipettingCode.Services
{
    public class Step1 : IExecute
    {
        /// <summary>
        /// 磁珠充分混匀
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            //调用厂家提供的接口
            var res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show("确保有针头", "换针提示", MessageBoxButton.OKCancel));
            if (res == MessageBoxResult.Cancel)
            {
                return false;
            }

            var result = PipettingViewModel.Instance.TakeNeedle(0);
            if (result == -1)//取针失败
            {
                return false;
            }

            await Task.WhenAll(PipettingViewModel.Instance.MoveX(config.X),
                PipettingViewModel.Instance.MoveY(config.Y));//移动到磁珠位置
            for (int i = 0; i < config.RepeatTime; i++)
            {
                //吸液
                PipettingViewModel.Instance.Pipetting_Imbibition(0);
                // 吐液
                PipettingViewModel.Instance.Pipetting_Injection(0);//注液
            }

            PipettingViewModel.Instance.OffNeedle(0);//脱针

            //执行流程
            return await Task.FromResult(true);
        }
    }

}
