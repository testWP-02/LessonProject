using NewsProject.Shared.DataTransferObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Helpers
{
    public static class IHttpServiceExtensionMethods
    {
        public static async Task<T> GetHelper<T>(this IHttpService _httpService, string url, bool includeToken = true)
        {
            var response = await _httpService.Get<T>(url, includeToken);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        //This method working with /NewsProject/Server/Helpers/HttpContextExtensions
        public static async Task<PaginatedResponse<T>> GetHelper<T>(this IHttpService _httpService, string url, PaginationDTO paginationDTO, bool includeToken = true)
        {
            string newUrl = "";

            if (url.Contains("?"))
            {
                newUrl = $"{url}&page={paginationDTO.Page}&recordsPerPage={paginationDTO.RecordsPerPage}";
            }
            else
            {
                newUrl = $"{url}?page={paginationDTO.Page}&recordsPerPage={paginationDTO.RecordsPerPage}";
            }

            var httpResponse = await _httpService.Get<T>(newUrl, includeToken);

            var totalAmountPages = int.Parse(httpResponse.HttpResponseMessage.Headers.GetValues("totalAmountPages").FirstOrDefault());

            var paginatedResponse = new PaginatedResponse<T>//PaginatedResponse locating in BlazorProject.Shared/DataTransferObjects/PaginatedResponse
            {
                Response = httpResponse.Response,
                TotalAmountPages = totalAmountPages
            };

            return paginatedResponse;
        }
    }
}
