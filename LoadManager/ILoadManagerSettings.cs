namespace LoadBalancer.LoadManager
{
    public interface ILoadManagerSettings
    {
        public string FirstHost { get; set; }
        public int HostsPoolSize { get; set; }
        public int HostIncrementationValue { get; set; }
    }
}
