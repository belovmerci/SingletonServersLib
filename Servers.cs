using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServersLib
{

    public class Servers
    {
        private static Servers instance;
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
                    return instance ?? (instance = new Servers());
                }
            }
        }
        // Task 1*: Sync Servers for multi-threaded use
        private static object syncRoot = new object();

        public static Servers InstanceSync
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new Servers());
                }
            }
        }

        // Task 2*: Change singleton type from lazy to eager
        private static readonly Servers eagerInstance = new Servers();

        public static Servers EagerInstance
        {
            get { return eagerInstance; }
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
