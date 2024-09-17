using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MultiThreadedPing
{
    internal class HostData
    {
        public string hostName;
        public string ipAddress;
        public string[] hostNameArray;

        public HostData(IPHostEntry host, string ipaddress)
        {
            hostNameArray = host.HostName.ToString().Split('.');
            this.hostName = hostNameArray[0];
            this.ipAddress= ipaddress;
        }
    }
}
