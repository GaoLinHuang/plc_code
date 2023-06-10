using System;

namespace Common.Log
{
    // Token: 0x02000003 RID: 3
    public interface ILogger
    {
        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600000B RID: 11
        string LogFolder { get; }

        // Token: 0x0600000C RID: 12
        void Info(string info);

        // Token: 0x0600000D RID: 13
        void Message(string message);

        // Token: 0x0600000E RID: 14
        void Error(string error);

        // Token: 0x0600000F RID: 15
        void Error(string error, Exception ex);

        // Token: 0x06000010 RID: 16
        void Error(Exception ex);
    }
}