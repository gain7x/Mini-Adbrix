using Programmers.Enums;
using Programmers.Utils;
using System.Text.Json.Serialization;

namespace Programmers.Events
{
    public class Problem
    {
        [JsonPropertyName("problem_id")]
        public string ProblemId { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("category")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AlgorithmCategory Category { get; set; } = RandomHelper.Get<AlgorithmCategory>();

        [JsonPropertyName("level")]
        public int Level { get; set; } = RandomHelper.Get(1, 10);
    }
}
