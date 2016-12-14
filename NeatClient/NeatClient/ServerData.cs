using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testClientWPF
{
    public static class ServerData
    {
        public static string port { get; set; }
        public static string host { get; set; }
        public static string getServerURL()
        {
            return host + ":" + port;
        }
    }
}
