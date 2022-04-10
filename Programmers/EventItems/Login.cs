using System.Text.Json.Serialization;

namespace Programmers.Events
{
    public class Login
    {
        [JsonPropertyName("region_id")]
        public string RegionId { get; set; } = Guid.NewGuid().ToString();
    }
}
