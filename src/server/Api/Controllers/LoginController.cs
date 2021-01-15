using System.Threading.Tasks;
using Api.Helpers;
using App.Api.Controllers;
using Core.Dtos.User;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        public LoginController(IConfiguration _configuration, IUserService _userService)
        {
            configuration = _configuration;
            userService = _userService;
        }

        [HttpPost("/[controller]/")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public async Task<ActionResult<LoginResponse>> Index(LoginRequest model)
        {
            var user = await userService.Login(model);
            var tokenHandler = new TokenHelper(configuration);
            var token = tokenHandler.CreateAccessToken(user);
            var response = new LoginResponse(){
                Token = token
            };
            return Ok(response);
        }
    }
}