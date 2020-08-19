using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public interface IImagesRepository
    {
        Task Create(Image item);
        Task Delete(int id);
        Task<List<Image>> GetImageByName(string name);
        Task<PaginatedResponse<List<Image>>> GetImages(PaginationDTO paginationDTO);
        Task<Image> GetItemById(int id);
        Task Update(Image item);
    }
}
