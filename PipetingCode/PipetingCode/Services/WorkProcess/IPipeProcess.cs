using System.Threading.Tasks;

namespace PipettingCode.Services
{
    internal interface IPipeProcess
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Task InitAsync();

        /// <summary>
        /// 开启流程
        /// </summary>
        /// <param name="processName">流程名</param>
        /// <returns></returns>
        Task StartProcess(string processName);

        /// <summary>
        /// 结束流程
        /// </summary>
        void StopProcess();
    }
}