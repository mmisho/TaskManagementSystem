using Shared.Interfaces;

namespace Application.Shared
{
    public class PaginationResponse : IPaginationResponse
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int Total { get; set; }
    }
}
