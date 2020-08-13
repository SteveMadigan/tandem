using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Tandem;
using Tandem.Responses;
using Xunit;
using FluentValidation;
using Tests.Validators;
using System.Net.Http;
using System.Text;
using Shouldly;

namespace Tests
{
    public partial class IntegrationTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetShouldReturnGetUserResponse()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/user/someone@somewhere.org");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<GetUserResponse>(content);

            var validator = new GetUserResponseValidator();

            validator.ValidateAndThrow(user);
        }

        [Fact]
        public async Task GetShouldReturnNotFound_IfUserDoesNotExist()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/user/fool@hardy.com");

            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostShouldReturnSuccess()
        {
            var client = _factory.CreateClient();

            var payload = "{ \"firstName\":\"Jason\", \"middleName\":\"Q\", \"lastName\":\"Public\", \"emailAddress\":\"jason@email.com\", \"phoneNumber\":\"123-456\" }";
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/user", content);

            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location.ToString();

            location.ShouldBe("http://localhost/api/user/jason@email.com");
        }
    }
}
