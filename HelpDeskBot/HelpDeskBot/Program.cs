using DSharpPlus;
using Microsoft.Extensions.Configuration;

namespace HelpDeskBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            Configuration botToken = GetConfig.GetConfigJson();
            DiscordClient client = BotSetup.BuildClient(botToken.Token);

            await client.ConnectAsync();
            await Task.Delay(-1); //Prevent console window from closing
        }
    }
}