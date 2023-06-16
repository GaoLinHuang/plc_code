using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Base.Json
{
    /// <summary>
    /// Json 存取
    /// </summary>
    public class JsonRepository
    {
        /// <summary>
        /// Json文件直接映射到某一个类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T TryParse<T>(string fileName)
        {
            if (!File.Exists(fileName))
                return default(T);
            using (var stream = File.OpenRead(fileName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();
                    return content.ToObj<T>();
                }
            }
        }
        /// <summary>
        /// 某一个类型保存到Json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Save<T>(string fileName, T obj)
        {
            if (!File.Exists(fileName))
            {
                using (File.Create(fileName))
                {
                }
            }
            File.WriteAllText(fileName, obj.ToJson());
            return true;
        }
    }
}
