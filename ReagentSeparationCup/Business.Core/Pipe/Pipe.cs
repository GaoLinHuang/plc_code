using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core
{
    public class Pipe
    {
        private Task _initTask;
        public Task InitAsync()
        {
            _initTask = Task.Run(() =>
            {

            });
            return _initTask;
        }
        /// <summary>
        /// 取针
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TakeNeedleAsync()
        {
            return await Task.FromResult(true);
        }
        /// <summary>
        /// 脱针
        /// </summary>
        /// <returns></returns>
        public async Task<bool> OffNeedleAsync()
        {
            return await Task.FromResult(true);
        }
        
        /// <summary>
        /// 吸液
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ImbibitionAsync()
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 分杯
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SeparationCupAsync()
        {
            //根据分杯信息执行相应的操作


            return await Task.FromResult(true);
        }
    }
}
