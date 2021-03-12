namespace LoadBalancer.LoadManager
{
    public class LoadBalancerSettings : ILoadBalancerSettings
    {
        public string FirstHost { get; set; }
        public int HostsPoolSize { get; set; }
        public int HostIncrementationValue { get; set; }
    }
}
