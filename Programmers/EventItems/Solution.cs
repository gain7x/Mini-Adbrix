using Programmers.Enums;
using Programmers.Utils;
using System.Text.Json.Serialization;

namespace Programmers.Events
{
    public class Solution
    {
        [JsonPropertyName("solution_id")]
        public string SolutionId { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("problem_id")]
        public string ProblemId { get; set; }

        [JsonPropertyName("code_language")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CodeLanguageType CodeLanguage { get; set; } = RandomHelper.Get<CodeLanguageType>();

        [JsonPropertyName("code_length")]
        public int CodeLength { get; set; } = RandomHelper.Get(300, 2000);

        /// <summary>
        /// 정답 여부를 임의로 초기화합니다.
        /// </summary>
        [JsonPropertyName("is_correct_answer")]
        public bool IsCorrectAnswer { get; set; } = RandomHelper.Get(60);

        public Solution(string problemId)
        {
            ProblemId = problemId;
        }
    }
}
