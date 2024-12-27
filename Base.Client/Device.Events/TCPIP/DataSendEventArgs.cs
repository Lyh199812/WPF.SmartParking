using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Events.TCPIP
{
    public class DataSendEventArgs : EventArgs
    {
        public string Message { get; }
        public string ClientID { get; }

        public DataSendEventArgs(string message, string _ClientID)
        {
            ClientID = _ClientID;

            Message = message;
        }
    }
}
