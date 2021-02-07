using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using yu_pi.Features.Ngrok;
using yu_pi.Features.User;

namespace yu_pi.Services
{
    public class YupiBackgroundService : IHostedService, IDisposable
    {
        private readonly AblyClientService ably;
        private readonly IMediator mediator;
        public YupiBackgroundService(AblyClientService _ably, IMediator _meditor)
        {
            ably = _ably;
            mediator = _meditor;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => RegisterChannels());
        }

        private void RegisterChannels()
        {
            var ngrokChannel = ably.Client.Channels.Get("ngrok-tunnels");
            ngrokChannel.Subscribe("register", async (message) =>
            {
                var data = JsonConvert.DeserializeObject<List<Ngrok.TunnelModel>>(message.Data.ToString());
                // var result = await mediator.Send(new Login.LoginCommand()
                // {
                //     Email = "yunuskorkmaz95@gmail.com",
                //     Password = "123123123"
                // });
                // Console.WriteLine(JsonConvert.SerializeObject(result));
                await mediator.Send(new Ngrok.TunnelRegisterCommand{ Tunnels = data });
                Console.WriteLine(JsonConvert.SerializeObject(message.Data));
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}