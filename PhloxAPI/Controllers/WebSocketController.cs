using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace PhloxAPI.Controllers
{
  public class WebSocketController : ControllerBase
  {

    //handle request to connect to the websocket. uses CONNECT instead of GET requests
    [Route("/ws")]
    public async Task Get()
    {
      if (HttpContext.WebSockets.IsWebSocketRequest)
      {
        using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync(); //upgrades TCP to WebSocket connection. WebSocket object used to send/recieve msgs
        await Echo(webSocket);
      }
      else
      {
        HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
      }
    }

    //method that receives client message and then sends in back in a loop until connection is closed
    //TODO: Handle client disconnects (see documentation); client sends ping every X interval, server closes connection if no msg within 2*X
    private static async Task Echo(WebSocket webSocket)
    {
      var buffer = new byte[1024 * 4];
      var receiveResult = await webSocket.ReceiveAsync(
          new ArraySegment<byte>(buffer), CancellationToken.None);

      while (!receiveResult.CloseStatus.HasValue)
      {
        await webSocket.SendAsync(
            new ArraySegment<byte>(buffer, 0, receiveResult.Count),
            receiveResult.MessageType,
            receiveResult.EndOfMessage,
            CancellationToken.None);

        receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);
      }

      await webSocket.CloseAsync(
          receiveResult.CloseStatus.Value,
          receiveResult.CloseStatusDescription,
          CancellationToken.None);
    }

  }
}