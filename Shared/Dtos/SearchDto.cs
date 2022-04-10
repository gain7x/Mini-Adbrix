using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class SearchDto
    {
        [Required]
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
