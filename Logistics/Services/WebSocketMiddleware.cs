using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Logistics.Models;
using Logistics.Services;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string? _accessToken;
    private readonly IOrderService _orderService;

    public WebSocketMiddleware(RequestDelegate next, IConfiguration options, IOrderService orderService)
    {
        _next = next;
        _accessToken = options["wss-access-token"];
        _orderService = orderService;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await HandleWebSocket(webSocket);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task HandleWebSocket(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        Console.WriteLine("Has connection");
        string token = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine("Checking token");
        if (token != _accessToken)
        {
            Console.WriteLine("Bad token");
            await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid access token", CancellationToken.None);
            return;
        }
        Console.WriteLine("Good token");

        while (!result.CloseStatus.HasValue)
        {
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message: {message}");

                try
                {
                    var parseData = ParseMessage(message);

                    await _orderService.AddNewAvailableOrderAsync(parseData);
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Data received")), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid access token", CancellationToken.None);
                    return;
                }
            }
        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    private Available ParseMessage(string message)
    {
        var data = JsonSerializer.Deserialize<ParseOrder>(message);
        if(data == null || data.IsDataInvalid())
            throw new ArgumentNullException("message");

        Console.WriteLine(data.ToString());

        return data.ToAvailable();
    }
}

public static class WebSocketMiddlewareExtensions
{
    public static IApplicationBuilder UseWebSocketMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<WebSocketMiddleware>();
    }
}