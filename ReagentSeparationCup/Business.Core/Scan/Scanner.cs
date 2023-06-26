using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Core
{
    public class Scanner
    {
        /// <summary>
        /// 扫描回调
        /// </summary>
        public event Func<ScanInfo, Task> OnScanCallBack;
        /// <summary>
        /// 开始连接
        /// </summary>
        /// <returns></returns>
        public Task ConnectAsync()
        {
            return Task.CompletedTask;
        }

        public List<ScanInfo> GetScanInfos()
        {
            return new List<ScanInfo>();
        }
    }
}