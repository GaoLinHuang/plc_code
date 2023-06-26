namespace Business.Core
{
    public class ProviderManager
    {
        /// <summary>
        /// 状态
        /// </summary>
        public static ProcessManage ProcessManage { get; }=new ProcessManage();

        /// <summary>
        /// 
        /// </summary>
        public static PlcManager PlcManager { get; } = new PlcManager();
        

        /// <summary>
        /// 实验流程管理类
        /// </summary>
        public static ExperimentManager ExperimentManager = new ExperimentManager();
    }
}
