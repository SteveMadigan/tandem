using System.Text.Json.Serialization;

namespace Tandem.Requests
{
    public class CreateUserRequest
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }
    }
}
