using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Core.Plc;

namespace Business.Core
{
    public class ProviderManager
    {
        public static PlcManager PlcManager { get; } = new PlcManager();
    }
}
