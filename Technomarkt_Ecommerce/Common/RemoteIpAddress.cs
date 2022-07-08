using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace UI.Utils
{
    public class RemoteIpAddress
    {
        public static string GetIpAdress()
        {
            string ip = "";

            IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (var item in localIps)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = item.ToString();
                    return ip;
                }
            }
            return "Ip adress not found";
        }
    }
}