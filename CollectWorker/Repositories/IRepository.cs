using Shared.Models;

namespace CollectWorker.Services
{
    public interface IRepository
    {
        /// <summary>
        /// User를 저장합니다.
        /// </summary>
        /// <exception cref="Exception">저장 실패</exception>
        Task SaveAsync(User user);

        /// <summary>
        /// Event를 저장합니다.
        /// </summary>
        /// <exception cref="Exception">저장 실패</exception>
        Task SaveAsync(Event eventItem);
    }
}
