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
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public ImagesController(ApplicationDbContext context, IFileStorageService fileStorageService, IMapper mapper)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Image item)
        {
            if (!string.IsNullOrWhiteSpace(item.Picture)) //img save in db realese in BlazorProject.Server/Helper/IFileStorageService
            {
                var personPhoto = Convert.FromBase64String(item.Picture);
                item.Picture = await _fileStorageService.SaveFile(personPhoto, "jpg", "images");
            }

            _context.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Image>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Images.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage); //InsertPaginationParametersInResponse located in BlazorProject.Server/Helpers/HttpContextExtensions

            return await queryable.Paginate(paginationDTO).ToListAsync();
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<Image>> Get(int id)
        //{
        //    var person = await _context.Images.FirstOrDefaultAsync(o => o.Id == id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }
        //    return person;
        //}

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Image>> Get(int id)
        {
            var person = await _context.Images.Include(o => o.DatabaseImages).
                                               ThenInclude(o => o.News).
                                               FirstOrDefaultAsync(o => o.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        //The method using in PersonRepository locate in: BlazorProject.Client/Repository/ImageRepository
        [HttpGet("search/{searchText}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Image>>> FilterByName(string searchText) //This method using in People/Employees/Managers filter methods like CreateProduct where we choose Manager
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<Image>();
            }

            //return await _context.Images.Where(o => o.Name.Contains(searchText)).Take(5).ToListAsync();
            return await _context.Images.Where(o => o.Name.Contains(searchText)).ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Image item)
        {
            var itemDb = await _context.Images.FirstOrDefaultAsync(o => o.Id == item.Id);
            if (itemDb == null)
            {
                return NotFound();
            }

            itemDb = _mapper.Map(item, itemDb);

            if (!string.IsNullOrWhiteSpace(item.Picture))
            {
                var personPicture = Convert.FromBase64String(item.Picture);
                itemDb.Picture = await _fileStorageService.EditFile(personPicture, "jpg", "images", itemDb.Picture);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Images.FirstOrDefaultAsync(o => o.Id == id);

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
