using NewsProject.Client.Helpers;
using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/categories"; //Write here controller name without suffix, this property indicate route to Controller

        public CategoryRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateCategory(Category category)
        {
            var response = await _httpService.Post(url, category);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateCategory(Category category)
        {
            var response = await _httpService.Put(url, category);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<List<Category>> GetCategorie()
        {
            var response = await _httpService.Get<List<Category>>(url);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<Category>>> GetCategories(PaginationDTO paginationDTO)//PaginatedResponse locating in BlazorProject.Shared/DataTransferObjects/PaginatedResponse
        {
            return await _httpService.GetHelper<List<Category>>(url, paginationDTO);
        }

        public async Task<Category> GetCategories(int id)
        {
            var response = await _httpService.Get<Category>($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task DeleteCategory(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}