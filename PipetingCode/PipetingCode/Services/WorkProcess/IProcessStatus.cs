using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services
{
    public interface IProcessStatus
    {
        /// <summary>
        /// 注册回调
        /// </summary>
        /// <param name="onCallBack"></param>
        void RegisterStatusCallBack(Action<string> onCallBack);
    }
}
