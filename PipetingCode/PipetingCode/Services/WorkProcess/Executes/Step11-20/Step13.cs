using PipettingCode.Services.Config;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace PipettingCode.Services
{
    internal class Step13 : IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        {
            Console.WriteLine($"凉干约{config.DryByAiringTime}min，磁珠无反光");

            var res = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return MessageBox.Show($"步骤13:凉干约{config.DryByAiringTime}min，磁珠无反光", "确认提示", MessageBoxButton.OKCancel);

            });

            //凉干约3min，磁珠无反光
            return true;
        }
    }
}
