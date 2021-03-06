using System.Collections.Generic;

namespace LoadBalancer.Entities
{
    public class Term
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public IEnumerable<DocumentInTerm> Documents { get; set; }
    }
}
