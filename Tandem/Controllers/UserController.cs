using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public GetUserResponse GetUser(string emailAddress)
        {
            var user =  _userRepository.GetByEmail(emailAddress);
            var response = Mapper.MapFrom<User, GetUserResponse>(user);

            return response;
        }

        [HttpPost]
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var user = Mapper.MapFrom<CreateUserRequest, User>(request);
            var id =  _userRepository.CreateUser(user);

            return new CreateUserResponse
            {
                Id = id
            };
        }


    }
}
