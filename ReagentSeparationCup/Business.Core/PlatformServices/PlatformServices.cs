using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Business.Core
{
    public class PlatformServices
    {
        private string _baseUrl = "";
        /// <summary>
        /// 获取分液信息
        /// </summary>
        /// <returns></returns>
        public async Task<Result<SplitCupInfo>> GetSplitCupInfo()
        {
            using (RestClient client = new RestClient(_baseUrl))
            {
                var request = new RestRequest("Assets/LargeFile.7z");
                return await client.GetAsync<Result<SplitCupInfo>>(request);
            }
        }

        /// <summary>
        /// 获取实现数据
        /// </summary>
        /// <returns></returns>
        public async Task<Result<ExperimentInfo>> GetExperimentInfo()
        {
            using (RestClient client = new RestClient(_baseUrl))
            {
                var request = new RestRequest("Assets/LargeFile.7z");
                return await client.GetAsync<Result<ExperimentInfo>>(request);
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<Result<T>> PostAsync<T>()
        {
            using (RestClient client = new RestClient(_baseUrl))
            {
                var request = new RestRequest("Assets/LargeFile.7z");
                return await client.PostAsync<Result<T>>(request);
            }
        }
    }
}
