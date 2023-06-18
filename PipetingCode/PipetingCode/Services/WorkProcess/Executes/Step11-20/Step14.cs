using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipettingCode.Services
{
    internal class Step14 : IExecute
    {
        public  async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine("从磁力架上取下");
    

            var res = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return MessageBox.Show($"步骤14:从磁力架上取下", "确认提示", MessageBoxButton.OKCancel);

            });

            return true;
        }
    }
}
