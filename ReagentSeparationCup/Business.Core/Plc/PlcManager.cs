using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Plc
{
    public class PlcManager
    {
        public PlcManager(string ip, int port)
        {

        }
        public Task StartConnectAsync()
        {
            IsConnectedTask = Task.Run(async () =>
            {
                await Task.Delay(1000);

                return true;
            });
            return Task.CompletedTask;
        }

        public Task<bool> IsConnectedTask;

    }
}
