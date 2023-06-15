﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PipettingCode.Services.Config;
using PipettingCode.Views;

namespace PipettingCode.Services
{
    internal class Step2 : IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            Console.WriteLine($"样本孔加入{config.Capacity}ul磁珠");
            var res = await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                return MessageBox.Show("确保有针头", "换针提示", MessageBoxButton.OKCancel);

            });

            if (res == MessageBoxResult.Cancel)
            {
                return false;
            }
            for (int i = 0; i < 96; i++)
            {
                //取针
                PipettingViewModel.Instance.TakeNeedle(i);

                //吸液
                PipettingViewModel.Instance.Imbibition(i);
                // 吐液
                PipettingViewModel.Instance.Injection(i);

                //步骤三的操作
                for (int j = 0; j < config.RepeatTime; j++)
                {
                    PipettingViewModel.Instance.TakeNeedle(i);//吸液量

                    PipettingViewModel.Instance.OffNeedle(i);//吐液
                }

                //脱针
                PipettingViewModel.Instance.OffNeedle(i);
            }

            return await Task.FromResult(true);
        }
    }
}
