using App.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new {
                name= "yunus"
            });
        }
    }
}