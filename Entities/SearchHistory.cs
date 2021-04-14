using System;
using System.Collections.Generic;

namespace SearchEngine.LoadBalancer.Entities
{
	public class SearchHistory
	{
		public string Keyword { get; set; }
		public List<DateTime> Dates { get; set; }
	}
}
