using System.Collections.Generic;
using System.Linq;

namespace LoadBalancer.LoadManager
{
    public class LoadManager : ILoadManager
    {
        private readonly Queue<string> _hosts = new Queue<string>();
        private readonly object _hostsQueueLock = new object();


        public LoadManager(ILoadBalancerSettings loadManagerSettings)
        {
            string hostUrl = loadManagerSettings.FirstHost[0..^4];
            int hostNumber = int.Parse(loadManagerSettings.FirstHost.Split(":").Last());
            for (int i = 0; i < loadManagerSettings.HostsPoolSize; i++)
            {
                _hosts.Enqueue(hostUrl + hostNumber);
                hostNumber += loadManagerSettings.HostIncrementationValue;
            }
        }

        public string GetNextHost()
        {
            lock (_hostsQueueLock)
            {
                string host = _hosts.Dequeue();
                _hosts.Enqueue(host);
                return host;
            }
        }
    }
}
