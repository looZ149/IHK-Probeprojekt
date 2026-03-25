using Microsoft.Extensions.Configuration;

namespace HelpDeskBot;

public static class GetConfig
{
    public static Configuration GetConfigJson()
    {
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false,
        reloadOnChange: true)
        .Build();

        //Each JSON OBJ is called a Section, we only have 1 in the config, anyway.
        var section = config.GetSection("Discord");

        Configuration botToken = new Configuration();
        section.Bind(botToken);
        return botToken;
    }
}