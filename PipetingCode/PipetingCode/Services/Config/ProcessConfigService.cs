using System.Collections.Concurrent;
using PipettingCode.Services.Config;
using System.Collections.Generic;
using System.Linq;
using Windows.Base;

namespace PipettingCode.Services
{
    public class ProcessConfigService : Singleton<ProcessConfigService>
    {

        private readonly ConcurrentDictionary<string, ConfigInfo> _configInfoDic;
        public ProcessConfigService()
        {
           
            _configInfoDic = new ConcurrentDictionary<string, ConfigInfo>();
            //TODO 从本地中加载相关的配置
            List<ConfigInfoItem> configInfos = new List<ConfigInfoItem>();
            for (int i = 1; i < 21; i++)
            {
                var configInfo = new ConfigInfoItem();
                configInfo.Id = i;
                configInfos.Add(configInfo);
            }

          

            _configInfoDic.TryAdd("process_1",
                new ConfigInfo()
                {
                    Key = "process_1",
                    Title = "第一次",
                    IsEnable = true,
                    ConfigInfoItems = configInfos,
                }
            );

            _configInfoDic.TryAdd("process_2",
                new ConfigInfo()
                {
                    Key = "process_2",
                    Title = "第二次",
                    IsEnable = true,
                    ConfigInfoItems = configInfos,
                }
            );

            _configInfoDic.TryAdd("process_3",
                new ConfigInfo()
                {
                    Key = "process_3",
                    Title = "第三次",
                    IsEnable = true,
                    ConfigInfoItems = configInfos,
                }
            );

            _configInfoDic.TryAdd("process_4",
                new ConfigInfo()
                {
                    Key = "process_4",
                    Title = "第四次",
                    IsEnable = true,
                    ConfigInfoItems = configInfos,
                }
            );
        }

        public List<ConfigInfo> GetConfigInfos()
        {
            return _configInfoDic.Values.ToList();
        }
        public List<ConfigInfoItem> GetConfigInfos(string processName)
        {
            _configInfoDic.TryGetValue(processName, out var value);
            return value?.ConfigInfoItems ?? new List<ConfigInfoItem>();
        }

        public void UpdateConfig(ConfigInfo configInfo)
        {
            _configInfoDic.AddOrUpdate(configInfo.Key, configInfo, (k, v) => v);
        }
    }
}