using IO.Ably;
using Microsoft.Extensions.Configuration;

namespace yu_pi.Services
{
    public class AblyClientService
    {
        public readonly AblyRealtime Client;
        public AblyClientService(IConfiguration _configuration)
        {
            var realtime =  new AblyRealtime(_configuration.GetValue<string>("ably"));
            realtime.Connect();
            Client = realtime;
        }
    }
}