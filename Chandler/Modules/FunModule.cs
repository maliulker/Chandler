using System;
using System.Threading.Tasks;
using Discord.Interactions;


namespace Chandler.Modules;

public class FunModule : InteractionModuleBase
{
    [SlashCommand("echo", "Echo an input")]
    public async Task Echo(string input)
    {
        await RespondAsync(input);
    }
}