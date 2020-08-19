using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public interface IPersonRepository
    {
        Task Create(Person item);
        Task DeletePerson(int id);
        Task<List<Person>> GetPeopleByName(string name);
        Task<PaginatedResponse<List<Person>>> GetPeopleFiltered(FilterItemDTO filterItemDTO);
        Task<Person> GetPersonById(int id);
        Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO);
        Task UpdatePerson(Person person);
    }
}
