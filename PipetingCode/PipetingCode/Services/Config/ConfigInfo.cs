using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipettingCode.Services.Config
{
    public class ConfigInfo
    {
        public string Title { get; set; }
        public string Key { get; set; }
    

        public bool IsEnable { get; set; }

        public List<ConfigInfoItem> ConfigInfoItems { get; set; }
    }
}
