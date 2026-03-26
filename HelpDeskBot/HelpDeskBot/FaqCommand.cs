using System.Text.Json;
using DSharpPlus.Commands;
namespace HelpDeskBot;

public class Faq
{
    public string? question { get; set; }
    public string? answer { get; set; }
}

public class FaqCommand (HelpDeskApiService apiService)
{
    private HelpDeskApiService apiService = apiService;
    
    [Command("faq")]
    public async ValueTask FaqAsync(CommandContext context)
    {
        var faq = await apiService.GetFaq();
        List<Faq>? FAQ = JsonSerializer.Deserialize<List<Faq>>(faq);
        string complete = "**FAQ**\n\n";
        
        foreach (var question in FAQ)
        {
            string questionText = "**Question:**" + question.question + "\n\n";
            string answerText = "**Answer:**" + question.answer + "\n\n";
            complete += questionText + answerText;
        }

        await context.RespondAsync(complete); 
    }


}