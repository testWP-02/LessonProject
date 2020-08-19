using NewsProject.Client.Helpers;
using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/news";

        public NewsRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> Create(News item)
        {
            var response = await _httpService.Post<News, int>(url, item);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<DetailsNewsDTO> GetDetailsNewsDTO(int id) //this action needing for gettind object details. 
        {
            return await _httpService.GetHelper<DetailsNewsDTO>($"{url}/{id}");
        }

        public async Task<List<News>> GetNewses()
        {
            var response = await _httpService.Get<List<News>>(url);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<News>>> GetNewsForPagination(PaginationDTO paginationDTO)//PaginatedResponse locating in BlazorProject.Shared/DataTransferObjects/PaginatedResponse
        {
            return await _httpService.GetHelper<List<News>>(url, paginationDTO);
        }

        public async Task<PaginatedResponse<List<News>>> GetNewsesFiltered(FilterNewsDTO filterNewsDTO)
        {
            var responseHTTP = await _httpService.Post<FilterNewsDTO, List<News>>($"{url}/filter", filterNewsDTO);
            var totalAmountPages = int.Parse(responseHTTP.HttpResponseMessage.Headers.GetValues("totalAmountPages").FirstOrDefault());

            var paginatedResponse = new PaginatedResponse<List<News>>()
            {
                Response = responseHTTP.Response,
                TotalAmountPages = totalAmountPages
            };

            return paginatedResponse;
        }

        //This method need for getting data and update theme in DB
        public async Task<NewsUpdateDTO> GetNewsForUpdate(int id)
        {
            return await _httpService.GetHelper<NewsUpdateDTO>($"{url}/update/{id}");
        }

        public async Task UpdateNews(News model)
        {
            var response = await _httpService.Put(url, model);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteNews(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
