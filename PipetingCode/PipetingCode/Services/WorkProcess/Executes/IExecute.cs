using PipettingCode.Services.Config;
using System.Threading.Tasks;

namespace PipettingCode.Services
{
    /// <summary>
    /// 执行接口
    /// </summary>
    internal interface IExecute
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<bool> ExecuteAsync(ConfigInfoItem config);
    }
}