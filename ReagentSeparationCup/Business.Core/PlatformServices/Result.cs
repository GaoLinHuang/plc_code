using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Business.Core
{
    public class Result<T>
    {
        [JsonProperty("code")]

        public int Code { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
