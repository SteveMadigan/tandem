using System.Text.Json.Serialization;

namespace Tandem.Responses
{
    public class GetUserResponse
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

    }
}
