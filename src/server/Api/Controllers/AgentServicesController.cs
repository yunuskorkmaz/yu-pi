using System.Collections.Generic;
using System.Threading.Tasks;
using App.Api.Controllers;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AgentServicesController : BaseController
    {
        
        public AgentServicesController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok();
        }
    }
}