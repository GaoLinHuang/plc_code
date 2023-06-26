using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core
{
    public interface IPrinter
    {
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        Task ConnectAsync();

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="printInfo"></param>
        /// <returns></returns>
        Task PrintAsync(PrintInfo printInfo);
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        Task DisConnectAsync();
    }
}
