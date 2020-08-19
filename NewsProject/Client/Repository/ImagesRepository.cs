using NewsProject.Client.Helpers;
using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/images";

        public ImagesRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Create(Image item)
        {
            var response = await _httpService.Post(url, item);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task Update(Image item)
        {
            var response = await _httpService.Put(url, item);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task Delete(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<List<Image>> GetImageByName(string name) //This metod using in BlazorProject.Client/Pages/News/NewsForm
        {
            var response = await _httpService.Get<List<Image>>($"{url}/search/{name}");

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<Image>>> GetImages(PaginationDTO paginationDTO)//PaginatedResponse locating in BlazorProject.Shared/DataTransferObjects/PaginatedResponse
        {
            return await _httpService.GetHelper<List<Image>>(url, paginationDTO);
        }

        public async Task<Image> GetItemById(int id)
        {
            return await _httpService.GetHelper<Image>($"{url}/{id}");
        }
    }
}
