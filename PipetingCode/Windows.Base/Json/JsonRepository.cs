using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Base.Json
{
    public class JsonRepository
    {
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
        public static bool Save<T>(string fileName, T obj)
        {
            File.WriteAllText(fileName, obj.ToJson());
            return true;
        }
    }
}
