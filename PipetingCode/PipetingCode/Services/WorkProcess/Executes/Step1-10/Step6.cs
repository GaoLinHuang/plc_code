using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipettingCode.Services
{
    internal class Step6 : IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            return await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var messageBoxResult = MessageBox.Show("磁力板上静置至液体澄清", "确认", MessageBoxButton.OKCancel);
                return messageBoxResult == MessageBoxResult.OK;
            });
        }
    }
}
