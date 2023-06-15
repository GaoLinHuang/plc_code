using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PipettingCode.Services.Config;

namespace PipettingCode.Services
{
    internal class Step5:IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            Console.WriteLine("将样本转移到磁力板上");
            return await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var messageBoxResult = MessageBox.Show("将样本转移到磁力板上", "确认", MessageBoxButton.OKCancel);
                return messageBoxResult == MessageBoxResult.OK;
            });
        }
    }
}
