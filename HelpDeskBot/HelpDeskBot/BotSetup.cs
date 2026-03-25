using DSharpPlus;
using DSharpPlus.Commands;

namespace HelpDeskBot;

public static class BotSetup
{
    public static DiscordClient BuildClient(string token)
    {
        DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(token, DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents);
        // We don't need serviceProvider, its part of dependency injection. Its part of the method signature tho so we have to keep it there.
        // extension is our CommandsExtension obj from DSharpPlus, this is what we actually interact with to register the command class
        builder.UseCommands((serviceProvider, extension) =>
        {
            extension.AddCommands([typeof(FaqCommand)]);
        }, new CommandsConfiguration()
        {
            // Better register commands to our specific server for debugging purpose
            DebugGuildId = 1486010770356437083,
            // THis is usually set default but who knows, right?
            RegisterDefaultCommandProcessors = true
        });
        
        DiscordClient client = builder.Build();
        return client;
    }
}