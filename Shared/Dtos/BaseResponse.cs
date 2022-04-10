using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class BaseResponse
    {
        [JsonPropertyOrder(-2)]
        [JsonPropertyName("is_success")]
        public bool IsSuccess { get; set; }

        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
