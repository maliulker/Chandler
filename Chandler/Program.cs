using System;
using System.IO;
using System.Threading.Tasks;
using Chandler.Modules;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Chandler;

public class Program
{
    private static DiscordSocketClient _socketClient = null!;
    private static InteractionService _interactionService = null!;
    private static IServiceProvider _serviceProvider = null!;
    public static async Task Main()
    {
        _socketClient = new DiscordSocketClient();
        _socketClient.Log += Log;
        _socketClient.InteractionCreated += InteractionCreated;
        _socketClient.Ready += Ready;
        _interactionService = new InteractionService(_socketClient.Rest);
        _interactionService.Log += Log;

        _serviceProvider = new ServiceCollection()
            .AddSingleton(_socketClient)
            .AddSingleton(_interactionService)
            .BuildServiceProvider();



        var token = File.ReadAllText(".env");

        await _socketClient.LoginAsync(TokenType.Bot, token);
        await _socketClient.StartAsync();

        await Task.Delay(-1);
    }

    private static async Task Ready()
    {
        await _interactionService.AddModuleAsync<FunModule>(_serviceProvider);
        await _interactionService.RegisterCommandsGloballyAsync();
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private static async Task InteractionCreated(SocketInteraction socketInteraction)
    {
        var ctx = new SocketInteractionContext(_socketClient, socketInteraction);
        await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
    }
}