using NewsProject.Client.Helpers;
using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/people";

        public PersonRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Create(Person item)
        {
            var response = await _httpService.Post(url, item);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdatePerson(Person person)
        {
            var response = await _httpService.Put(url, person);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeletePerson(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<List<Person>> GetPeopleByName(string name) //This metod using in BlazorProject.Client/Pages/News/NewsForm
        {
            var response = await _httpService.Get<List<Person>>($"{url}/search/{name}", includeToken: false);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO)//PaginatedResponse locating in BlazorProject.Shared/DataTransferObjects/PaginatedResponse
        {
            return await _httpService.GetHelper<List<Person>>(url, paginationDTO, includeToken: false);
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _httpService.GetHelper<Person>($"{url}/{id}", includeToken: false);
        }

        public async Task<PaginatedResponse<List<Person>>> GetPeopleFiltered(FilterItemDTO filterItemDTO)
        {
            var responseHTTP = await _httpService.Post<FilterItemDTO, List<Person>>($"{url}/filter", filterItemDTO, includeToken: false);
            var totalAmountPages = int.Parse(responseHTTP.HttpResponseMessage.Headers.GetValues("totalAmountPages").FirstOrDefault());

            var paginatedResponse = new PaginatedResponse<List<Person>>()
            {
                Response = responseHTTP.Response,
                TotalAmountPages = totalAmountPages
            };

            return paginatedResponse;
        }

        //public async Task<DetailsPersonDTO> GetDetailsPersonDTO(int id) //this action needing for gettind object details. 
        //{
        //    return await _httpService.GetHelper<DetailsPersonDTO>($"{url}/{id}");
        //}
    }
}
