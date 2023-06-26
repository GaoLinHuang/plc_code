using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core
{

    /// <summary>
    /// 转动控制
    /// </summary>
    public class TurnManager
    {
        private readonly PlcManager _plcManager;
        public TurnManager(PlcManager plcManager)
        {
            _plcManager = plcManager;
        }

        /// <summary>
        /// 转动原液转盘
        /// </summary>
        public Task<bool> StartTurnSourceLiquid()
        {
            return Task.Run(() =>
            {
                return _plcManager.PLC_SendCommand_Write("", 1);
            });
        }

        /// <summary>
        /// 转动分液转盘
        /// </summary>
        public bool StartTurnTargetLiquid()
        {
           return _plcManager.PLC_SendCommand_Write("", 1);
        }
    }
}
