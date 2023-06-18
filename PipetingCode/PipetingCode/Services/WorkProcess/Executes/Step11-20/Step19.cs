using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipettingCode.Services
{
    internal class Step19 : IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine("步骤19：磁力板上静置至液体澄清（>3min）");
            var res = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return MessageBox.Show("步骤19：磁力板上静置至液体澄清（>3min）", "提示", MessageBoxButton.OKCancel);

            });
            return (true);
        }
    }
}
