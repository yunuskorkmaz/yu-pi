using App.Api.Controllers;
using Core.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        public IActionResult Login(LoginRequest loginModel)
        {
            return Ok(new LoginResponse(){});
        }
    }
}