using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Responses
{
    public class CreateUserResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
