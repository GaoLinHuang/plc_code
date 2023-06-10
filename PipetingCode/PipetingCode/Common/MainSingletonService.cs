using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Log;
using Windows.Base;

namespace PipetitngCode.Common
{
    internal class MainSingletonService:Singleton<MainSingletonService>
    {
        public MainSingletonService()
        {
            Log = new Logger("Pipetitng");
        }

        public ILogger Log { get; private set; }
    }

   
}
