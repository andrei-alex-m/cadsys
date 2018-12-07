using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace CS.ImportExportWeb.Hubs
{
    public class MessagesHub:Hub 
    {
        //public async Task SendMessage(string message)
        //{
        //    await Clients.All.SendAsync("receivemessage", message);
        //}
    }
}
