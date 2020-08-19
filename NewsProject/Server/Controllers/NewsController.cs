using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsProject.Server.Helpers;
using NewsProject.Shared.DataTransferObjects;
using NewsProject.Shared.Models;

namespace NewsProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public NewsController(ApplicationDbContext context, IFileStorageService fileStorageService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(News news)
        {
            if (!string.IsNullOrWhiteSpace(news.Poster)) //img save in db realese in BlazorProject.Server/Helper/IFileStorageService
            {
                var productPhoto = Convert.FromBase64String(news.Poster);
                news.Poster = await _fileStorageService.SaveFile(productPhoto, "jpg", "news");
            }

            if (news.NewsManagers != null)
            {
                for (int i = 0; i < news.NewsManagers.Count; i++)
                {
                    news.NewsManagers[i].Order = i + 1;
                }
            }

            if (news.NewsImages != null)
            {
                for (int i = 0; i < news.NewsImages.Count; i++)
                {
                    news.NewsImages[i].Order = i + 1;
                }
            }

            _context.Add(news);
            await _context.SaveChangesAsync();
            return news.Id;
        }

        //This method needing for getting product details 
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DetailsNewsDTO>> Get(int id) //This model located in BlazorProject.Shared/DetailsProductDTO
        {
            var news = await _context.News.Where(o => o.Id == id)
                .Include(o => o.NewsCategories).ThenInclude(o => o.Category)
                .Include(o => o.NewsManagers).ThenInclude(o => o.Person)
                .Include(o => o.NewsImages).ThenInclude(o => o.Image)
                .FirstOrDefaultAsync();

            if (news == null)
            {
                return NotFound();
            }

            var voteAverage = 0.0;
            var uservote = 0;

            if (await _context.NewsRatings.AnyAsync(x => x.NewsId == id))
            {
                voteAverage = await _context.NewsRatings.Where(x => x.NewsId == id)
                    .AverageAsync(x => x.Rate);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    var userId = user.Id;

                    var userVoteDB = await _context.NewsRatings
                        .FirstOrDefaultAsync(x => x.NewsId == id && x.UserId == userId);

                    if (userVoteDB != null)
                    {
                        uservote = userVoteDB.Rate;
                    }
                }
            }


            news.NewsManagers = news.NewsManagers.OrderBy(o => o.Order).ToList();

            var model = new DetailsNewsDTO();

            model.News = news;
            model.Categories = news.NewsCategories.Select(o => o.Category).ToList();

            model.Managers = news.NewsManagers.Select(o =>
                new Person
                {
                    Id = o.Person.Id,
                    Name = o.Person.Name,
                    Photo = o.Person.Photo,
                    Position = o.Person.Position,
                    Cabinet = o.Person.Cabinet,
                    Department = o.Person.Department,
                    Phone = o.Person.Phone,
                    DateOfWork = o.Person.DateOfWork
                }).ToList();

            model.Images = news.NewsImages.Select(o =>
                new Image
                {
                    Id = o.Image.Id,
                    Name = o.Image.Name,
                    Picture = o.Image.Picture,
                    DateOfUpload = o.Image.DateOfUpload,
                    ImagesCharacter = o.Image.ImagesCharacter
                }).ToList();

            model.UserVote = uservote;
            model.AverageVote = voteAverage;

            return model;
        }

        //This method realese filter logic
        [HttpPost("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<News>>> Filter(FilterNewsDTO filterNewsDTO)
        {
            var modelQueryable = _context.News.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterNewsDTO.Title))
            {
                modelQueryable = modelQueryable.Where(o => o.Title.Contains(filterNewsDTO.Title));
            }

            if (filterNewsDTO.CategoryId != 0)
            {
                modelQueryable = modelQueryable.
                    Where(o => o.NewsCategories.
                    Select(x => x.CategoryId).
                    Contains(filterNewsDTO.CategoryId));
            }

            await HttpContext.InsertPaginationParametersInResponse(modelQueryable, filterNewsDTO.RecordsPerPage);

            var model = await modelQueryable.Paginate(filterNewsDTO.Pagination).ToListAsync();

            return model;
        }

        [HttpGet("update/{id}")]
        public async Task<ActionResult<NewsUpdateDTO>> PutGet(int id)
        {
            var model = await Get(id);

            if (model.Result is NotFoundResult)
            {
                return NotFound();
            }

            var modelDetailDTO = model.Value;
            var selectedCategoriesIds = modelDetailDTO.Categories.Select(o => o.Id).ToList();
            var notSelectedCategories = await _context.Categories.Where(o => !selectedCategoriesIds.Contains(o.Id)).ToListAsync();

            var newsModel = new NewsUpdateDTO();

            newsModel.News = modelDetailDTO.News;
            newsModel.SelectedCategories = modelDetailDTO.Categories;
            newsModel.NotSelectedCategories = notSelectedCategories;
            newsModel.Managers = modelDetailDTO.Managers;
            newsModel.Images = modelDetailDTO.Images;

            return newsModel;
        }

        [HttpPut]
        public async Task<ActionResult> Put(News news)
        {
            var newsDb = await _context.News.FirstOrDefaultAsync(o => o.Id == news.Id);

            if (newsDb == null)
            {
                return NotFound();
            }

            newsDb = _mapper.Map(news, newsDb);

            if (!string.IsNullOrWhiteSpace(news.Poster))
            {
                var newsPicture = Convert.FromBase64String(news.Poster);
                newsDb.Poster = await _fileStorageService.EditFile(newsPicture, "jpg", "news", newsDb.Poster);
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"delete from NewsManagers where NewsId = {news.Id}; delete from NewsImages where NewsId = {news.Id}; delete from NewsCategories where NewsId = {news.Id}");

            if (news.NewsManagers != null)
            {
                for (int i = 0; i < news.NewsManagers.Count; i++)
                {
                    news.NewsManagers[i].Order = i + 1;
                }
            }

            if (news.NewsImages != null)
            {
                for (int i = 0; i < news.NewsImages.Count; i++)
                {
                    news.NewsImages[i].Order = i + 1;
                }
            }

            newsDb.NewsManagers = news.NewsManagers;
            newsDb.NewsCategories = news.NewsCategories;
            newsDb.NewsImages = news.NewsImages;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.News.FirstOrDefaultAsync(o => o.Id == id);

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
