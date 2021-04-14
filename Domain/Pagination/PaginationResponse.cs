namespace SearchEngine.LoadBalancer.Domain.Pagination {
	public class PaginationResponse<T> {
		public int PageNumber { get; set; }
		public int PageCount { get; set; }
		public int PageSize { get; set; }
		public T Data { get; set; }
	}
}
