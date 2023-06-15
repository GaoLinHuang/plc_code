using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipettingCode.Services.Config;

namespace PipettingCode.Services
{
    public class Step3: IExecute
    {
        public async Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            //for (int i = 0; i < config.RepeatTime; i++)
            //{
            //    Console.WriteLine("吸液");
            //    await Task.Delay(100);
            //    Console.WriteLine("注液");
            //}

            //Console.WriteLine("完成步骤3");
            return true;
        }
    }
}
