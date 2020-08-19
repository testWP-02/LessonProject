using NewsProject.Client.Helpers;
using NewsProject.Shared.Models;
using System;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IHttpService httpService;

        private readonly string urlBase = "api/rating";

        public RatingRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task Vote(NewsRating newsRating)
        {
            var httpResponse = await httpService.Post(urlBase, newsRating);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }
    }
}
