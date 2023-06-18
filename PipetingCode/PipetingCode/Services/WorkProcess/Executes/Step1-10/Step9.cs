using PipettingCode.Common;
using PipettingCode.Services.Config;
using PipettingCode.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipettingCode.Services
{
    internal class Step9 : IExecute
    {
        protected virtual string Prefix { get; } = "步骤8：";
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine("吸取澄清液，丢弃废液及枪头");
            var res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show($"{Prefix}:吸取澄清液，丢弃废液及枪头.确保有针头", "换针提示", MessageBoxButton.OKCancel));
            if (res == MessageBoxResult.Cancel)
            {
                return false;
            }

            Operatort();

            //吸干
            while (true)
            {
                res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show($"{Prefix}:吸取澄清液，丢弃废液及枪头.继续进行吸干操作吗", "吸干提示", MessageBoxButton.OKCancel));
                var needRepeat = res == MessageBoxResult.OK;
                if (!needRepeat) break;
                res = await Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show($"{Prefix}:吸取澄清液，丢弃废液及枪头.确保有针头", "换针提示", MessageBoxButton.OKCancel));
                if (res == MessageBoxResult.Cancel)
                {
                    return false;
                }
                Operatort();
            }

            return await Task.FromResult(true);
        }

        private void Operatort()
        {
            for (int i = 0; i < GlobalConfig.OrificePlateErgodicCount; i++)
            {
                PipettingViewModel.Instance.TakeNeedle(i);
                // 磁力架吸取（吸干），到指定区域吐液 脱针
                PipettingViewModel.Instance.SuckDry(i);
                //吸液
                Console.WriteLine("磁力架吸取（吸干），到指定区域吐液 脱针");
                //吐液
                PipettingViewModel.Instance.InjectionAndOffNeedle(i);//丢液
            }
        }
    }
}
