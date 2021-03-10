namespace LoadBalancer.LoadBalancer
{
	public interface ILoadManagerSettings
	{
		public string Host { get; set; }
		public int HostsPoolSize  { get; set; }
		public int HostIncrementationValue { get; set; }
	}
}
