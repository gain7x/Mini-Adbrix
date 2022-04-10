using Shared.Models;

namespace Search.Api.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> findEventByUserAsync(string userId);
    }
}
