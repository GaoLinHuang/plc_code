using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core
{
    public enum ProcessStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStarted,
        /// <summary>
        /// 进行中
        /// </summary>
        InProgress,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 急停
        /// </summary>
        Emergent
    }
}
