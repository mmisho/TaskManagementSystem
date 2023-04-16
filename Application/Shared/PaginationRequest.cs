using Shared.Interfaces;

namespace Application.Shared
{
    public abstract class PaginationRequest : IPaginationRequest
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
