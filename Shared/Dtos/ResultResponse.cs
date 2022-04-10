using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ResultResponse<T> : BaseResponse
    {
        public T Result { get; set; }

        public ResultResponse(bool isSuccess, T result)
            :base(isSuccess)
        {
            Result = result;
        }
    }
}
