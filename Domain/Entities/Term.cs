using System.Collections.Generic;

namespace SearchEngine.LoadBalancer.Entities {
	public class Term {
		public string Id { get; set; }
		public string Value { get; set; }
		public IEnumerable<Document> Documents { get; set; }
	}
}
