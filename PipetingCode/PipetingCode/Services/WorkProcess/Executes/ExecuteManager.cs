using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //executes.Add(1,new Step1());
            //executes.Add(3,new Step3());
            //executes.Add(4,new Step4());
            //executes.Add(5,new Step5());

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().
                    Where(t => t.GetInterfaces().Contains(typeof(IExecute)) && !t.IsAbstract))
                .ToArray();
            foreach (var item in types)
            {
                IExecute obj = (IExecute)Activator.CreateInstance(item);
                if (int.TryParse(item.Name.Replace("Step", ""), out int index))
                {
                    executes[index]=obj;
                }
            }

            Console.WriteLine();
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