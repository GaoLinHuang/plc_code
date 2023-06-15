using PipettingCode.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services
{
    internal class Step6 : IExecute
    {
        public Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            Console.WriteLine("磁力板上静置至液体澄清（>3min）");
            return Task.FromResult(true);
        }
    }
}
