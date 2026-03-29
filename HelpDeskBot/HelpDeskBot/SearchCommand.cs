using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

namespace HelpDeskBot;

public class SearchCommand (HelpDeskApiService apiService)
{
    private HelpDeskApiService apiService = apiService;



    [Command("search")]
    public async ValueTask SearchAsync(CommandContext context, string query)
    {
        var raw = await apiService.SearchFiles(query);
        var fileNames = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries); //Returns a string array but should work with foreach, probably?
        var options = new List<DiscordSelectComponentOption>();

        foreach (var file in fileNames)
        {
            options.Add(new DiscordSelectComponentOption($"{file}", $"{file}"));
        }

        var dropdown = new DiscordSelectComponent("doc_select", "Pick a document", options);
        var response = new DiscordMessageBuilder()
            .WithContent("Found these docs:")
            .AddActionRowComponent(new DiscordActionRowComponent([dropdown]));

        await context.RespondAsync(response);
        var message = await context.GetResponseAsync();

        var result = await message.WaitForSelectAsync(
            context.User,
            "doc_select",
            TimeSpan.FromSeconds(30)
        );

        if (!result.TimedOut) // Unsure if that handles it properly tbh
        {
            string chosenFile = result.Result.Values[0];
            var fileContent = await apiService.GetDoc(chosenFile);
            await context.FollowupAsync(fileContent); // Woa.. Sending whole file content feels rather stupid ?
                                                     // Maybe trim the Response to the section where the Keyword was found??
                                                     // Will probably also exceed the char limit of discord msgs? 
        }
           

    }
}
