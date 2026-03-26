using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskBot
{
    class Program
    {
        static async Task Main(string[] args)
        {

            ServiceCollection serviceCollection = new ServiceCollection();
            
            var (discord, asp) = GetConfig.GetConfigJson();
            
            serviceCollection.AddSingleton<HttpClient>();
            serviceCollection.AddSingleton<HelpDeskApiService>(provider => new HelpDeskApiService(asp.Key, asp.Url, provider.GetRequiredService<HttpClient>()));
            serviceCollection.BuildServiceProvider();
            //discord.Token should never be null here anyway so.. Kinda ignore the warning? Atleast for now
            DiscordClient client = BotSetup.BuildClient(discord.Token);
            
           

            await client.ConnectAsync();
            await Task.Delay(-1); //Prevent console window from closing
            
        }
    }
}