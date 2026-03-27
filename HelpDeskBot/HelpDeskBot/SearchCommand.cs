using DSharpPlus.Commands;
namespace HelpDeskBot;

public class SearchCommand (HelpDeskApiService apiService)
{
    private HelpDeskApiService apiService = apiService;

    [Command("search")]
    public async ValueTask SearchAsync(CommandContext context, string query)
    {
        var fileNames = await apiService.SearchFiles(query);
    }
}