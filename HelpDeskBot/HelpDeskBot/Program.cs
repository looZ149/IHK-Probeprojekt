using DSharpPlus;
using Microsoft.Extensions.Configuration;

namespace HelpDeskBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            var (discord, asp) = GetConfig.GetConfigJson();
            //discord.Token should never be null here anyway so.. Kinda ignore the warning? Atleast for now
            DiscordClient client = BotSetup.BuildClient(discord.Token);

            await client.ConnectAsync();
            await Task.Delay(-1); //Prevent console window from closing
        }
    }
}