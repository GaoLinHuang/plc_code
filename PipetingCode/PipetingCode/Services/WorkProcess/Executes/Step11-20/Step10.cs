using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services
{
    internal class Step10 : Step8
    {
        protected override string Prefix { get; } = "步骤10";
        //public Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        //{
        //    Console.WriteLine("加入200ul乙醇，静置30S");
        //    return Task.FromResult(true);
        //}
    }
}
