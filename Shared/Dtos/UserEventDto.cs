using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class UserEventDto
    {
        [Required]
        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        [Required]
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [Required]
        [JsonPropertyName("event")]
        public string EventType { get; set; }

        [Required]
        [JsonPropertyName("parameters")]
        public JsonDocument Parameters { get; set; }
    }
}
