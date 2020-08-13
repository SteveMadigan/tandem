using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tandem.Model;
using Tandem.Requests;
using Tandem.Responses;
using Tandem.Storage;
using Tandem.Utility;

namespace Tandem.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{emailAddress}")]
        public async Task<ActionResult<GetUserResponse>> GetUser(string emailAddress)
        {
            var user =  await _userRepository.GetByEmail(emailAddress);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var response = Mapper.MapFrom<User, GetUserResponse>(user);
                return Ok(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserRequest>> CreateUser(CreateUserRequest request)
        {
            var user = Mapper.MapFrom<CreateUserRequest, User>(request);
            var id =  await _userRepository.CreateUser(user);

            if (id == null)
            {
                return Conflict();
            }
            else
            {
                return CreatedAtAction(nameof(GetUser), new {emailAddress = request.EmailAddress}, request);
            }
        }


    }
}
