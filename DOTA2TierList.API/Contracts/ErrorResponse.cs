using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DOTA2TierList.API.Contracts
{
    public record ErrorResponse(int StatusCode, string Message)
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
