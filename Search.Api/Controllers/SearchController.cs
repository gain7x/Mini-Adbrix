using Microsoft.AspNetCore.Mvc;
using Search.Api.Repositories;
using Shared.Dtos;
using Shared.Models;

namespace Search.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IEventRepository _eventRepository;

        public SearchController(ILogger<SearchController> logger, IEventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// 이벤트 조회 결과를 최근순으로 반환합니다.
        /// </summary>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<BaseResponse> Post(SearchDto searchDto)
        {
            List<Event> events;

            try
            {
                events = await _eventRepository.findEventByUserAsync(searchDto.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to find event. User- {searchDto.UserId}: {ex.Message}");
                return new ResultResponse<List<EventDto>>(false, null);
            }

            var result = events.ConvertAll(ei => new EventDto()
            {
                EventId = ei.Guid,
                EventName = ei.Type,
                Parameters = ei.Content,
                CreatedDate = ei.CreatedDate
            }).OrderByDescending(ei => ei.CreatedDate).ToList();

            return new ResultResponse<List<EventDto>>(true, result);
        }
    }
}