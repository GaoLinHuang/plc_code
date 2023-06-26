using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core
{
    public class PrinterProvider : IPrinter
    {
        public PrinterProvider()
        {

        }
        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task PrintAsync(PrintInfo printInfo)
        {
            throw new NotImplementedException();
        }

        public Task DisConnectAsync()
        {
            throw new NotImplementedException();
        }
    }
   
}
