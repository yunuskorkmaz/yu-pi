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

        public async Task TunnelClose(int id){
            Console.WriteLine("request deleted " +id.ToString());
            var result = await mediator.Send(new DeleteTunnelCommand(){
                Id = id
            });
            await Clients.All.SendAsync("onTunnelDeleted",result);
        }

        
        public async Task AddTunnel(Tunnel tunnel){

            Console.WriteLine(JsonConvert.SerializeObject(tunnel));
            var result = await mediator.Send(new CreateTunnelCommand{
                Name = tunnel.Name,
                Port = tunnel.Port,
                Protokol = tunnel.Protokol
            });
            Console.WriteLine(JsonConvert.SerializeObject(result));
            await Clients.All.SendAsync("onTunnelAdded",result);
        }
    }
}