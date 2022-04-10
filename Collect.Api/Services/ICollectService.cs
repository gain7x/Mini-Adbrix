namespace Collect.Api.Services
{
    public interface ICollectService
    {
        /// <summary>
        /// 메시지를 수집합니다.
        /// </summary>
        /// <exception cref="Exception">수집 실패</exception>
        public Task Collect(string message);
    }
}