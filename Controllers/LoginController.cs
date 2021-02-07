using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yu_pi.Domain.Entities;
using yu_pi.Features.Ngrok;
using yu_pi.Features.User;
using yu_pi.Infrastructure.Context;

namespace yu_pi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly YupiContext context;
        private readonly IMapper mapper;

        public LoginController(IMediator _mediator, YupiContext _context, IMapper _mapper)
        {
            mediator = _mediator;
            context = _context;
            mapper = _mapper;
        }

        [HttpPost]
        public async Task<Login.LoginResult> Index([FromBody] Login.LoginCommand command)
        {
           return await mediator.Send(command);
        }
        
        [HttpGet]
        public async Task<ActionResult> Test()
        {
            var data =  JsonConvert.DeserializeObject<List<Ngrok.TunnelModel>>("[{\"name\": \"default\",\"publicUrl\": \"http://98408f015a47.ngrok.io\",\"proto\": \"http\"}]");
            var entity = mapper.Map<List<Tunnel>>(data);
            await context.Tunnels.AddRangeAsync(entity.ToArray());
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}