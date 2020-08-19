using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public interface INewsRepository
    {
        Task<int> Create(News item);
        Task DeleteNews(int id);
        Task<DetailsNewsDTO> GetDetailsNewsDTO(int id);
        Task<List<News>> GetNewses();
        Task<PaginatedResponse<List<News>>> GetNewsesFiltered(FilterNewsDTO filterNewsDTO);
        Task<PaginatedResponse<List<News>>> GetNewsForPagination(PaginationDTO paginationDTO);
        Task<NewsUpdateDTO> GetNewsForUpdate(int id);
        Task UpdateNews(News model);
    }
}
