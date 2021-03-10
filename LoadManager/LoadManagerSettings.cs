namespace LoadBalancer.LoadManager
{
    public class LoadManagerSettings : ILoadManagerSettings
    {
        public string Host { get; set; }
        public int HostsPoolSize { get; set; }
        public int HostIncrementationValue { get; set; }
    }
}
