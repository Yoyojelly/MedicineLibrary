using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NewProject.Hubs
{
    public class myHub : Hub
    {
        public async Task alterMessage()
        {
            await Clients.All.SendAsync("alterPowerTree");
        }
        //把请求的用户添加入一个组
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,groupName);
            
            await Clients.Group(groupName).SendAsync("send",$"{Context.ConnectionId} has joined the group {groupName}.");
        }
        //把请求的用户移出一个组
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId,groupName);
            
            await Clients.Group(groupName).SendAsync("Send","message");
        }
        //给一个组内的所有成员发送信息(接收到订单)
        public async Task SendMessageToGroup(string message,string groupName)
        {
            await Clients.Group(groupName).SendAsync("Receive",message);
        }
    }

    public class IdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.GetHttpContext()?.Request?.Cookies?["userid"];
        }
    }
}