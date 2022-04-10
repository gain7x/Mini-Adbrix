using Programmers.Enums;
using Programmers.Events;
using Programmers.Utils;
using Shared.Dtos;
using System.Text.Json;

namespace Programmers
{
    public class UserBot
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public CodeLanguageType UserCodeLanguage { get; set; } = RandomHelper.Get<CodeLanguageType>();
        public int EventCount { get; set; } = 0;

        public UserBot()
        {
            Console.WriteLine($"Created programmers user bot! UserId: {UserId}");
        }

        /// <summary>
        /// 사용자가 프로그래머스를 이용하기 시작합니다.
        /// </summary>
        public async Task RunAsync()
        {
            await LoginAsync();

            bool isUserTired = false;
            
            // 사용자는 지칠 때 까지 문제를 풉니다.
            while (!isUserTired)
            {
                var problem = await EnterProblemAsync(new Problem());
                await SolveProblem(problem);

                isUserTired = RandomHelper.Get(60);
            }

            Console.WriteLine($"Finished! UserId: {UserId}");
        }

        /// <summary>
        /// 문제를 해결합니다.
        /// </summary>
        private async Task SolveProblem(Problem problem)
        {
            var solution = await SubmitSolutionAsync(problem);

            while (!solution.IsCorrectAnswer)
            {
                await LearnAlgorithm(problem.Category);

                solution = await SubmitSolutionAsync(problem);
            }
        }

        /// <summary>
        /// 알고리즘을 공부합니다.
        /// </summary>
        private async Task LearnAlgorithm(AlgorithmCategory algorithmCategory)
        {
            Lecture lecture = new Lecture()
            {
                Category = algorithmCategory
            };

            await WatchLectureAsync(lecture);
        }

        /// <summary>
        /// 이벤트를 발생시킵니다.
        /// 모든 이벤트는 0.1초 이상 소요됩니다.
        /// </summary>
        private async Task<T> EventAsync<T>(EventType eventType, T eventItem) where T : class
        {
            EventCount++;
            Console.WriteLine($"{eventType}, UserEventCount: {EventCount} User: {UserId}");
            var dto = CreateEventDto(eventType, eventItem);
            await ApiHelper.PostEventAsync(dto);
            await Task.Delay(100);

            return eventItem;
        }

        /// <summary>
        /// 사용자 이벤트에 대한 DTO를 생성합니다.
        /// </summary>
        private UserEventDto CreateEventDto<T>(EventType eventType, T eventItem) where T : class
        {
            return new()
            {
                EventId = Guid.NewGuid().ToString(),
                UserId = UserId,
                EventType = eventType.ToString(),
                Parameters = JsonSerializer.SerializeToDocument(eventItem)
            };
        }

        /// <summary>
        /// 로그인합니다.
        /// </summary>
        private Task<Login> LoginAsync() =>
            EventAsync(EventType.Login, new Login());

        /// <summary>
        /// 알고리즘 문제에 입장합니다.
        /// </summary>
        /// <param name="problem">NULL인 경우 새로운 문제에 입장합니다.</param>
        private Task<Problem> EnterProblemAsync(Problem problem) =>
            EventAsync(EventType.EnterProblem, problem);

        /// <summary>
        /// 문제에 대한 답안을 제출합니다.
        /// </summary>
        private Task<Solution> SubmitSolutionAsync(Problem problem) =>
            EventAsync(
                EventType.SubmitSolution,
                new Solution(problem.ProblemId) { CodeLanguage = UserCodeLanguage });

        /// <summary>
        /// 강의를 시청합니다.
        /// </summary>
        private Task<Lecture> WatchLectureAsync(Lecture lecture) =>
            EventAsync(EventType.WatchLecture, lecture);
    }
}
