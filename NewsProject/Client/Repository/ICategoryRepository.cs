using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public interface ICategoryRepository
    {
        Task CreateCategory(Category category);
        Task<PaginatedResponse<List<Category>>> GetCategories(PaginationDTO paginationDTO);
        Task<Category> GetCategories(int id);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
        Task<List<Category>> GetCategorie();
    }
}
