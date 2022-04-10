
using Shared.Dtos.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class EventDto
    {
        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        [JsonPropertyName("event")]
        public string EventName { get; set; }

        [JsonPropertyName("parameters")]
        [JsonConverter(typeof(JsonDocumentJsonConverter))]
        public JsonDocument Parameters { get; set; }

        [JsonPropertyName("event_datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
