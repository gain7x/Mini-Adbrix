using Shared.Dtos;
using System.Net.Http.Json;

namespace Programmers.Utils
{
    public static class ApiHelper
    {
        public static async Task PostEventAsync(UserEventDto userEvent)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Config.ApiBaseAddress);
            await client.PostAsJsonAsync("api/collect", userEvent);
        }
    }
}
