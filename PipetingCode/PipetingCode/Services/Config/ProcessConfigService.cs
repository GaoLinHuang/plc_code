using System;
using System.Collections.Concurrent;
using PipettingCode.Services.Config;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Base;
using Windows.Base.Json;

namespace PipettingCode.Services
{
    public class ProcessConfigService : Singleton<ProcessConfigService>
    {
        private string fileRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessConfig");
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

            var files = Directory.GetFiles(fileRoot);
            foreach (var file in files)
            {
                var configInfo = JsonRepository.TryParse<ConfigInfo>(file);
                _configInfoDic.TryAdd(configInfo.Key, configInfo);
            }

            Console.WriteLine();
            //  List<ConfigInfo> s = new List<ConfigInfo>()
            //{
            //    new ConfigInfo()
            //    {
            //        Key = "process_1",
            //        Title = "第一次",
            //        IsEnable = true,
            //        ConfigInfoItems = configInfos,
            //    }
            //};


            _configInfoDic.TryAdd("process_1",
                  new ConfigInfo()
                  {
                      Key = "process_1",
                      Title = "第一次",
                      IsEnable = true,
                      ConfigInfoItems = configInfos,
                  }
              );

            //  _configInfoDic.TryAdd("process_2",
            //      new ConfigInfo()
            //      {
            //          Key = "process_2",
            //          Title = "第二次",
            //          IsEnable = true,
            //          ConfigInfoItems = configInfos,
            //      }
            //  );

            //  _configInfoDic.TryAdd("process_3",
            //      new ConfigInfo()
            //      {
            //          Key = "process_3",
            //          Title = "第三次",
            //          IsEnable = true,
            //          ConfigInfoItems = configInfos,
            //      }
            //  );

            //  _configInfoDic.TryAdd("process_4",
            //      new ConfigInfo()
            //      {
            //          Key = "process_4",
            //          Title = "第四次",
            //          IsEnable = true,
            //          ConfigInfoItems = configInfos,
            //      }
            //  );
        }

        /// <summary>
        /// 获取配置列表  大的流程的列表 如第一次 第二次  ....
        /// </summary>
        /// <returns></returns>
        public List<ConfigInfo> GetConfigInfos()
        {
            return _configInfoDic.Values.ToList();
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public List<ConfigInfoItem> GetConfigInfos(string processName)
        {
            _configInfoDic.TryGetValue(processName, out var value);
            return value?.ConfigInfoItems ?? new List<ConfigInfoItem>();
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="configInfo"></param>

        public void UpdateConfig(ConfigInfo configInfo)
        {
            _configInfoDic.AddOrUpdate(configInfo.Key, configInfo, (k, v) => v);
            string fullFileName = Path.Combine(fileRoot, $"{configInfo.Key}.json");
            //File.WriteAllText(fullFileName,configInfo.ToJson());

            JsonRepository.Save(fullFileName, configInfo);
        }
    }
}