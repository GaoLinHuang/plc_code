using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipettingCode.Services.Config;

namespace PipettingCode.Services
{
    public class Step1 : IExecute
    {
        /// <summary>
        /// 磁珠充分混匀
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteAsync(ConfigInfoItem config)
        {
            //调用厂家提供的接口
            //执行流程
           return await Task.FromResult(true);
        }
    }

    public class Step5 : Step1
    {

    }
}
