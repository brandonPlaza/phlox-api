using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace PhloxAPI.Controllers
{
  public class WebSocketController : ControllerBase
  {

    //handle request to connect to the websocket. uses CONNECT instead of GET requests
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/ws")]
    public async Task Get()
    {
      if (HttpContext.WebSockets.IsWebSocketRequest)
      {
        using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync(); //upgrades TCP to WebSocket connection. WebSocket object used to send/recieve msgs
      }
      else
      {
        HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
      }
    }

  }
}