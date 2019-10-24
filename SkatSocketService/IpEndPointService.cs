using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SkatSocketService
{
    class IpEndPointService
    {
   

        public static IPEndPoint SetIp(string ip, int port)
        {
            IPAddress newIp = IPAddress.Parse(ip);
            IPEndPoint iPEndPoint = new IPEndPoint(newIp, port);

            return iPEndPoint;
        }

    }
}
