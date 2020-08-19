using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsProject.Server.Helpers;
using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Categories.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage); //InsertPaginationParametersInResponse located in BlazorProject.Server/Helpers/HttpContextExtensions

            return await queryable.Paginate(paginationDTO).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(o => o.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Category category)
        {
            _context.Attach(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Categories.FirstOrDefaultAsync(o => o.Id == id);

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
