namespace PipettingCode.Services.Config
{
    public class ConfigInfoItem
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public int Id { get; set; } = 1;

        /// <summary>
        /// 重复次数
        /// </summary>
        public int RepeatTime { get; set; } = 1000;

        /// <summary>
        /// 容量
        /// </summary>
        public double Capacity { get; set; } = 80;

        /// <summary>
        /// 持续时间
        /// </summary>
        public int ContinueTime { get; set; } = 1000;

        /// <summary>
        /// X坐标
        /// </summary>
        public int X { get; set; } = 10;

        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y { get; set; } = 100;

        /// <summary>
        /// Z坐标 待定
        /// </summary>
        public int Z { get; set; }
    }
}