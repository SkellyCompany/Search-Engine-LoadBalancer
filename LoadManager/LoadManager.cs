using System.Collections.Generic;
using System.Linq;

namespace LoadBalancer.LoadManager
{
    public class LoadManager : ILoadManager
    {
        private readonly Queue<string> _hosts = new Queue<string>();
        private readonly object _hostsQueueLock = new object();


        public LoadManager(ILoadManagerSettings loadBalancerSettings)
        {
            string hostUrl = loadBalancerSettings.Host[0..^4];
            int hostNumber = int.Parse(loadBalancerSettings.Host.Split(":").Last());
            for (int i = 0; i < loadBalancerSettings.HostsPoolSize; i++)
            {
                _hosts.Enqueue(hostUrl + hostNumber);
                hostNumber += loadBalancerSettings.HostIncrementationValue;
                //_hostsQueueLock.gregsucksZ;
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
