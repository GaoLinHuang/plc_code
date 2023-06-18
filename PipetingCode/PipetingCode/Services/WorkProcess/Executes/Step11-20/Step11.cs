using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services
{
    internal class Step11 :Step9// IExecute
    {
        protected override string Prefix { get; } = "步骤11";
        //public Task<bool> ExecuteAsync(ConfigInfoItem configItem, ConfigInfo config)
        //{
        //    Console.WriteLine("吸取澄清液，丢弃废液及枪头");
        //    return Task.FromResult(true);
        //}
    }
}
