using System.Collections.Generic;
using System.Threading.Tasks;
using App.Api.Controllers;
using AutoMapper;
using Core.Dtos.Ngrok;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class NgrokController : BaseController
    {
        private readonly INgrokService ngrokService;
        private readonly IMapper mapper;
        
        public NgrokController(INgrokService _ngrokService, IMapper _mapper)
        {
            ngrokService = _ngrokService;
            mapper = _mapper;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Update(List<NgrokModel> model)
        {
            var tunnel = mapper.Map<Tunnel>(model);
            var response = await ngrokService.Update(tunnel);
            return Ok(true);
        }

        [HttpGet]
        public async Task<ActionResult<List<NgrokModel>>> GetAll ()
        {
            var tunnels = await ngrokService.GetAll();
            var response = mapper.Map<List<NgrokModel>>(tunnels);
            return Ok(response);
        }

    }
}