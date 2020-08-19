using NewsProject.Shared.Models;
using AutoMapper;

namespace NewsProject.Server.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            //These codes using in all controllers in BlazorProject.Server
            CreateMap<Person, Person>().ForMember(x => x.Photo, options => options.Ignore());

            CreateMap<Image, Image>().ForMember(x => x.Picture, option => option.Ignore());

            CreateMap<News, News>().ForMember(x => x.Poster, option => option.Ignore());
        }
    }
}
