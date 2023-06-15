using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipettingCode.Services.Config;

namespace PipettingCode.Services
{
    internal class Step5:IExecute
    {
        public Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            Console.WriteLine("将样本转移到磁力板上");
            return Task.FromResult(true);
        }
    }
}
