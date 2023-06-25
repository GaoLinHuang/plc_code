using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Core
{


    public class ProcessManage
    {
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        public ProcessStatus CurProcessStatus { get; private set; } = ProcessStatus.NotStarted;
        /// <summary>
        /// 状态改变
        /// </summary>
        public event Action<ProcessStatus> OnProcessStatusChanged;

        /// <summary>
        /// 重置
        /// </summary>
        /// <returns></returns>
        public async Task ResetAsync()
        {
            await ChangeStatus(ProcessStatus.NotStarted);
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            await ChangeStatus(ProcessStatus.InProgress);
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <returns></returns>
        public async Task PauseAsync()
        {
            await ChangeStatus(ProcessStatus.Pause);
        }
        /// <summary>
        /// 急停
        /// </summary>
        /// <returns></returns>
        public async Task EmergentAsync()
        {
            await ChangeStatus(ProcessStatus.Emergent);
        }

        private async Task ChangeStatus(ProcessStatus processStatus)
        {
            await _semaphoreSlim.WaitAsync();
            if (CurProcessStatus != processStatus)
            {
                CurProcessStatus = processStatus;
                OnProcessStatusChanged?.Invoke(CurProcessStatus);
            }

            _semaphoreSlim.Release();
        }
    }
}
