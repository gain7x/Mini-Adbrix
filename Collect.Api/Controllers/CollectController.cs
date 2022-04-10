using Collect.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.Text.Json;

namespace Collect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CollectController : ControllerBase
    {
        private readonly ILogger<CollectController> _logger;
        private readonly ICollectService _collectService;

        public CollectController(ILogger<CollectController> logger, ICollectService collectService)
        {
            _logger = logger;
            _collectService = collectService;
        }

        /// <summary>
        /// 사용자 이벤트를 SQS로 전송합니다.
        /// </summary>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<BaseResponse> Post(UserEventDto dto)
        {
            bool isSuccess = true;

            try
            {
                string json = JsonSerializer.Serialize(dto);
                await _collectService.Collect(json);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                _logger.LogError(ex.Message);
            }
            
            return new BaseResponse(isSuccess);
        }
    }
}