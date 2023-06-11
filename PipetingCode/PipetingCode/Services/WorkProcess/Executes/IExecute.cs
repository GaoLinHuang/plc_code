using PipettingCode.Services.Config;
using System.Threading.Tasks;

namespace PipettingCode.Services
{
    /// <summary>
    /// 流程执行接口
    /// </summary>
    public interface IExecute
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<bool> ExecuteAsync(ConfigInfoItem config);
    }
}