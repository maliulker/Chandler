using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Chandler;

public class Program
{
    private static DiscordSocketClient _socketClient;
    public static async Task Main()
    {
        _socketClient = new DiscordSocketClient();
        _socketClient.Log += Log; 

        var token = File.ReadAllText(".env");

        await _socketClient.LoginAsync(TokenType.Bot, token);
        await _socketClient.StartAsync();

        await Task.Delay(-1); 
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
