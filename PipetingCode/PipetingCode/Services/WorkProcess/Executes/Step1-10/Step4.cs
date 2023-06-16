using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PipettingCode.Services.Config;

namespace PipettingCode.Services
{
    internal class Step4:IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine($"等待{configItem.ContinueTime/1000}s");
            await Task.Delay(TimeSpan.FromMilliseconds(configItem.ContinueTime));
            
            return await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var messageBoxResult = MessageBox.Show("等待确认", "确认", MessageBoxButton.OKCancel);
                return messageBoxResult == MessageBoxResult.OK;
            });
        }
    }
}
