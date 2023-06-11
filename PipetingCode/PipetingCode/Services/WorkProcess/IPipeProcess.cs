using System.Threading.Tasks;

namespace PipettingCode.Services
{
    /// <summary>
    /// 加样臂执行接口
    /// </summary>
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
        Task StopCurrentProcess();
    }
}