using System;
using System.Threading.Tasks;
using Discord.Interactions;
using Discord.WebSocket;


namespace Chandler.Modules;

public class FunModule : InteractionModuleBase
{
    [SlashCommand("echo", "Echo an input")]
    public async Task Echo(string input)
    {
        await RespondAsync(input);
    }

    [SlashCommand("hello", "greet the user")]
    public async Task Hello()
    {
        var guildUser = (SocketGuildUser)Context.User;
        await RespondAsync($"Hello {guildUser.DisplayName}");
    }

}