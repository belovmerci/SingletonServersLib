using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServersLib
{
    public class Servers
    {
        // lazy instantiation
        // private static Servers instance;

        // eager instantiation
        private static readonly Servers instance = new Servers();
        private static readonly object lockObject = new object();

        private List<string> serverList;

        private Servers()
        {
            serverList = new List<string>();
        }

        public static Servers Instance
        {
            get
            {
                lock (lockObject)
                {
                    // for lazy instantiation
                    // return instance ?? (instance = new Servers());
                    // for eager instantiation
                    return instance;
                }
            }
        }

        public bool AddServer(string server)
        {
            if (!IsValidServer(server) || serverList.Contains(server))
            {
                return false;
            }

            serverList.Add(server);
            return true;
        }

        private bool IsValidServer(string server)
        {
            return server.StartsWith("http://") || server.StartsWith("https://");
        }

        public List<string> GetHttpServers()
        {
            return serverList.Where(s => s.StartsWith("http://")).ToList();
        }

        public List<string> GetHttpsServers()
        {
            return serverList.Where(s => s.StartsWith("https://")).ToList();
        }

    }
}
