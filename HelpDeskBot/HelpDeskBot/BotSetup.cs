using DSharpPlus;
using DSharpPlus.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskBot;

public static class BotSetup
{
    public static DiscordClient BuildClient(string token, string key, string url)
    {
        DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(token, DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents);
        // We don't need serviceProvider, its part of dependency injection. Its part of the method signature tho so we have to keep it there.
        // extension is our CommandsExtension obj from DSharpPlus, this is what we actually interact with to register the command class

        //Build the DI container. Register the ApiService into the DI Container IServiceCollection from DSharpPlus
        builder.ConfigureServices(service => service.AddSingleton<HelpDeskApiService>(_ => new HelpDeskApiService(key, url, new HttpClient())));
        
        // DSharpPlus automatically injects registered services, so we only need to interact with extension to register command classes
        builder.UseCommands((serviceProvider, extension) =>
        {
            extension.AddCommands([typeof(FaqCommand)]);
        }, new CommandsConfiguration()
        {
            // Better register commands to our specific server for debugging purpose
            DebugGuildId = 1486010770356437083,
            // This is usually set default but who knows, right?
            RegisterDefaultCommandProcessors = true
        });
        
        DiscordClient client = builder.Build();
        return client;
    }
}