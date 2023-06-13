using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipettingCode.Services.Config;

namespace PipettingCode.Services
{
    internal class Step2:IExecute
    {
        public Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            Console.WriteLine($"样本孔加入{config.Capacity}ul磁珠");
            return Task.FromResult(true);
        }
    }
}
