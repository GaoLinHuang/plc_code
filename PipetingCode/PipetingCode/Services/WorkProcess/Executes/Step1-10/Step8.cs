using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services
{
    internal class Step8 : IExecute
    {
        public Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            Console.WriteLine("加入200ul乙醇，静置30S");
            return Task.FromResult(true);
        }
    }
}
