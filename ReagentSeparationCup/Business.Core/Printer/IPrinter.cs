using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Printer
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

    public class PrintInfo
    {

    }
    public class Printer : IPrinter
    {
        public Printer()
        {

        }
        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task PrintAsync(PrintInfo printInfo)
        {
            throw new NotImplementedException();
        }

        public Task DisConnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
