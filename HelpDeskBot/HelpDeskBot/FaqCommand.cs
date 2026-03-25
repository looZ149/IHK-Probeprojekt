using DSharpPlus.Commands;
namespace HelpDeskBot;

public class FaqCommand
{
    [Command("faq")]
    public async ValueTask FaqAsync(CommandContext context)
    {
        //Gotta fetch the actual data from our VPS to display here. 
        context.RespondAsync("Frequently Asked Questions");
    }
}