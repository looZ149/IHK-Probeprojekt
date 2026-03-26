using Microsoft.Extensions.Configuration;

namespace HelpDeskBot;

public static class GetConfig
{
    public static (DiscordConfig discord, AspConfig asp) GetConfigJson()
    {
        var basePath = AppContext.BaseDirectory;
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(basePath)
        .AddJsonFile("appsettings.json", optional: false,
        reloadOnChange: true)
        .Build();

        //Each JSON OBJ is called a Section, we only have 1 in the config, anyway.
        var section = config.GetSection("Discord");
        var section2 = config.GetSection("ASP");


        DiscordConfig discordConfig = new DiscordConfig();
        AspConfig aspConfig = new AspConfig();
        
        section.Bind(discordConfig);
        section2.Bind(aspConfig);
        return (discordConfig, aspConfig);
    }
}