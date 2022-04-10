using Programmers.Enums;
using Programmers.Utils;
using System.Text.Json.Serialization;

namespace Programmers.Events
{

    public class Lecture
    {
        [JsonPropertyName("lecture_id")]
        public string LectureId { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("category")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AlgorithmCategory Category { get; set; } = RandomHelper.Get<AlgorithmCategory>();

        [JsonPropertyName("access_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LectureAccessType AccessType { get; set; } = RandomHelper.Get<LectureAccessType>();
    }
}
