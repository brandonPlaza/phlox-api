using Microsoft.AspNetCore.SignalR;

public class HelpRequestHub : Hub
{

  //send message to all connected clients; see documentation for creating and sending to SignalR Users group
  //TODO: probably remove this
  public async Task SendRequests(string msg) => await Clients.All.SendAsync("ReceiveRequest", msg);
}