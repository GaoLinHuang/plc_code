using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services.Config
{
    public class ConfigInfo
    {
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
    }
}
