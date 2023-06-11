using System.Collections.Generic;
using System.IO;
using Windows.Base;

namespace PipettingCode.Services
{
    /// <summary>
    /// 流程管理类
    /// </summary>
    internal class ExecuteManager:Singleton<ExecuteManager>
    {
        public ExecuteManager()
        {
            executes = new Dictionary<int, IExecute>();
            executes.Add(1,new Step1());
            executes.Add(5,new Step5());
        }
        private Dictionary<int, IExecute> executes;

        /// <summary>
        /// 获取流程执行步骤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IExecute GetExecute(int id)
        {
            executes.TryGetValue(id, out IExecute result);
            return result;
        }
    }
}