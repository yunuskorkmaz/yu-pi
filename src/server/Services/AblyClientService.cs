using IO.Ably;

namespace Services
{
    public class AblyClientService
    {
        public AblyRealtime Client { get;set;}
        public AblyClientService()
        {
            var realTime = new AblyRealtime("qhodPQ.ykAXfg:JS7_JBudS_esbArY");
            Client = realTime;
        }
    }
}