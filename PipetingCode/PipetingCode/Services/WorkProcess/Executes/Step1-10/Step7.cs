using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PipettingCode.Views;

namespace PipettingCode.Services
{
    internal class Step7 : IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine("吸取澄清液，丢弃废液及枪头");

            var res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show("确保有针头", "换针提示", MessageBoxButton.OKCancel));
            if (res == MessageBoxResult.Cancel)
            {
                return false;
            }

            Operatort();

            //吸干
            while (true)
            {
                res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show("确保有针头", "换针提示", MessageBoxButton.OKCancel));
                var needRepeat = res == MessageBoxResult.OK;
                if (!needRepeat) break;
                res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show("确保有针头", "换针提示", MessageBoxButton.OKCancel));
                if (res == MessageBoxResult.Cancel)
                {
                    return false;
                }
                Operatort();
            }

            return await Task.FromResult(true);
        }

        private static void Operatort()
        {
            for (int i = 0; i < 24; i++)
            {
                // 磁力架吸取（吸干），到指定区域吐液 脱针
                PipettingViewModel.Instance.TakeNeedle(i);
                //吸液
                Console.WriteLine("磁力架吸取（吸干），到指定区域吐液 脱针");
                //吐液

                //脱针
                PipettingViewModel.Instance.OffNeedle(i);
            }
        }
    }
}
