using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsProject.Shared.Models;

namespace NewsProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RatingController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Rate(NewsRating newsRating)
        {
            var user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            var currentRating = await _context.NewsRatings
                .FirstOrDefaultAsync(x => x.NewsId == newsRating.NewsId &&
                x.UserId == userId);

            if (currentRating == null)
            {
                newsRating.UserId = userId;
                newsRating.RatingDate = DateTime.Today;
                _context.Add(newsRating);
                await _context.SaveChangesAsync();
            }
            else
            {
                currentRating.Rate = newsRating.Rate;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
