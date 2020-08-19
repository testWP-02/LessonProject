using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsProject.Server.Helpers;
using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
        public PeopleController(ApplicationDbContext context, IFileStorageService fileStorageService, IMapper mapper)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Person person)
        {
            if (!string.IsNullOrWhiteSpace(person.Photo)) //img save in db realese in BlazorProject.Server/Helper/IFileStorageService
            {
                var personPhoto = Convert.FromBase64String(person.Photo);
                person.Photo = await _fileStorageService.SaveFile(personPhoto, "jpg", "people");
            }

            _context.Add(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Person>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.People.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage); //InsertPaginationParametersInResponse located in BlazorProject.Server/Helpers/HttpContextExtensions

            return await queryable.Paginate(paginationDTO).ToListAsync();
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<Person>> Get(int id)
        //{
        //    var person = await _context.People.Include(o => o.NewsManagers).
        //                                       ThenInclude(o => o.News).
        //                                       FirstOrDefaultAsync(o => o.Id == id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }
        //    return person;
        //}

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _context.People.FirstOrDefaultAsync(o => o.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        //This method needing for getting product details 
        //[HttpGet("{id}")]
        //public async Task<ActionResult<DetailsPersonDTO>> GetForDetails(int id) //This model located in BlazorProject.Shared/DetailsProductDTO
        //{
        //    var item = await _context.People.Where(o => o.Id == id)
        //        .FirstOrDefaultAsync();

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new DetailsPersonDTO();
        //    model.Person = item;

        //    return model;
        //}

        //The method using in PersonRepository locate in: BlazorProject.Client/Repository/PersonRepository
        [HttpGet("search/{searchText}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Person>>> FilterByName(string searchText) //This method using in People/Employees/Managers filter methods like CreateProduct where we choose Manager
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Person>();
            }

            return await _context.People.Where(o => o.Name.Contains(searchText)).Take(5).ToListAsync();
        }

        [HttpPost("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Person>>> Filter(FilterItemDTO filterItemDTO)
        {
            var modelQueryable = _context.People.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterItemDTO.Title))
            {
                modelQueryable = modelQueryable.Where(o => o.Name.Contains(filterItemDTO.Title));
            }
            if (filterItemDTO.InWork)
            {
                modelQueryable = modelQueryable.Where(o => o.InWork);
            }

            await HttpContext.InsertPaginationParametersInResponse(modelQueryable, filterItemDTO.RecordsPerPage);

            var model = await modelQueryable.Paginate(filterItemDTO.Pagination).ToListAsync();

            return model;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Person person)
        {
            var personDb = await _context.People.FirstOrDefaultAsync(o => o.Id == person.Id);
            if (personDb == null)
            {
                return NotFound();
            }

            personDb = _mapper.Map(person, personDb);

            if (!string.IsNullOrWhiteSpace(person.Photo))
            {
                var personPicture = Convert.FromBase64String(person.Photo);
                personDb.Photo = await _fileStorageService.EditFile(personPicture, "jpg", "people", personDb.Photo);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.People.FirstOrDefaultAsync(o => o.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
