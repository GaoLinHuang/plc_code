using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services.Config
{
    public class ConfigInfo
    {
        public int Index { get; set; }
        /// <summary>
        /// 流程的总名称：如第一次...
        /// </summary>
        public string Title { get; set; }
        public string Key { get; set; }//配置成文件名
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsEnable { get; set; } = true;

        public List<ConfigInfoItem> ConfigInfoItems { get; set; }

        /// <summary>
        /// 磁珠混匀次数
        /// </summary>
        public int MagneticBeadTimes { get; set; }

        /// <summary>
        /// 磁珠量
        /// </summary>
        public double MagneticBeadCapacity { get; set; }

        /// <summary>
        /// 样本混匀次数
        /// </summary>
        public int SampleBeadTimes { get; set; }
        /// <summary>
        /// 洗脱剂混匀次数
        /// </summary>
        public int WashBeadTimes { get; set; }

        /// <summary>
        /// 凉干时间
        /// </summary>
        public double DryByAiringTime { get; set; }

        /// <summary>
        /// 乙醇量1
        /// </summary>
        public double EthanolCapacityFirst { get; set; }
        /// <summary>
        /// 乙醇量2
        /// </summary>
        public double EthanolCapacitySecond { get; set; }

        /// <summary>
        /// 洗脱液1
        /// </summary>
        public double WashCapacityFirst { get; set; }
        /// <summary>
        /// 洗脱液2
        /// </summary>
        public double WashCapacitySecond { get; set; }
    }
}
