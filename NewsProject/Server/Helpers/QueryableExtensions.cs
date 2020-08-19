using NewsProject.Shared.DataTransferObjects;
using System.Linq;

namespace NewsProject.Server.Helpers
{
    public static class QueryableExtensions
    {
        //This method needing for pagination
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable.
                Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage).
                Take(paginationDTO.RecordsPerPage);
        }
    }
}
