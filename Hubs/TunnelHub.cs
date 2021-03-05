using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using yu_pi.Domain.Commands.Tunnels;
using yu_pi.Domain.Entities;
using yu_pi.Infrastructure.Context;

namespace yu_pi.Hubs
{
    public class TunnelHub : Hub
    {

        private readonly YupiContext context;
        private readonly IMediator mediator;

        public TunnelHub(YupiContext context,IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }
        
        public override Task OnConnectedAsync()
        {
            var data = context.Tunnels.ToList();
            return Clients.Caller.SendAsync("onConnected",data);
        }

        
        public async Task AddTunnel(CreateTunnelCommand tunnel){
            Console.WriteLine(JsonConvert.SerializeObject(tunnel));
            var result = await mediator.Send(tunnel);
            await Clients.All.SendAsync("tunnelAdded",result);
        }
    }
}