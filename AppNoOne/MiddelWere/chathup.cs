using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AppNoOne.MiddelWere
{
    public class chathup : Hub
    {
        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
