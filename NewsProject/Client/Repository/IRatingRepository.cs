using NewsProject.Shared.Models;
using System.Threading.Tasks;

namespace NewsProject.Client.Repository
{
    public interface IRatingRepository
    {
        Task Vote(NewsRating newsRating);
    }
}
