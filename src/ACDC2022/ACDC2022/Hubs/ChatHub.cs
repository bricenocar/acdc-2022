using Microsoft.AspNetCore.SignalR;

namespace ACDC2022.Hubs
{
    public class TelemetryHub : Hub
    {
        public Task RequestData()
        {
            var lastDate = DateTime.UtcNow;
            return Clients.All.SendAsync("responseData", lastDate);
        }
    }
}
