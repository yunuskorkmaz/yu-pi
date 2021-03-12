using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IO.Ably.Realtime;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using yu_pi.Domain.Commands.Tunnels;
using yu_pi.Domain.Entities;
using yu_pi.Domain.Enums;
using yu_pi.Features.Ngrok;

namespace yu_pi.Services
{
    public class YupiBackgroundService : IHostedService, IDisposable
    {
        private readonly AblyClientService ably;
        private readonly IMediator mediator;

        private IRealtimeChannel tunnelChannel { get; set; }
        public YupiBackgroundService(AblyClientService _ably, IMediator _meditor)
        {
            ably = _ably;
            mediator = _meditor;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            RegisterChannels();
            InitializeSubscribers();
            return Task.CompletedTask;
        }

        private void InitializeSubscribers()
        {
            tunnelChannel.Subscribe("tunnelOpened", async (message) =>
            {
                var tunnel = JsonConvert.DeserializeObject<Tunnel>(message.Data.ToString());
                Console.WriteLine(tunnel);
                await mediator.Send(new OpenedTunnelCommand()
                {
                    Id = tunnel.Id,
                    PublicUrl = tunnel.PublicUrl,
                    Status = tunnel.Status
                });
            });

            tunnelChannel.Subscribe("agentDisconnected", async (message) => {
                var tunnels = await mediator.Send(new GetAllTunnelsQuery());
                foreach(var tunnel in tunnels){
                    await mediator.Send(new OpenedTunnelCommand(){
                        Id = tunnel.Id,
                        PublicUrl = "",
                        Status =  (int) TunnelStatus.Passive
                    });
                }
            });

            tunnelChannel.Subscribe("agentConnected",async (message) => {
                var tunnels = await mediator.Send(new GetAllTunnelsQuery());
                tunnelChannel.Publish("onAgentConnected", JsonConvert.SerializeObject(tunnels));
            });

            // tunnelChannel.Subscribe("register", async (message) => {
            //     var data = JsonConvert.DeserializeObject<CreateTunnelCommand>(message.Data.ToString());
            //     await mediator.Send(data);
            // });
        }

        private void RegisterChannels()
        {
            tunnelChannel = ably.Client.Channels.Get("tunnels");
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            ably.Client.Close();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            ably.Client.Close();
        }
    }
}