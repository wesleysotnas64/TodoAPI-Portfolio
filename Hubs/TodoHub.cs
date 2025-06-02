using Microsoft.AspNetCore.SignalR;

namespace TodoAPI_Portfolio.Hubs
{
    public class TodoHub : Hub
    {
        public async Task NotifyTodoUpdated()
        {
            await Clients.All.SendAsync("TodoUpdated");
        }
    }
}
