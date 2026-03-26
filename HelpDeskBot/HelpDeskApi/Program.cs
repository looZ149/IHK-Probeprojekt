
namespace HelpDeskApi;

class ApiProgram
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

// Read the expected API key from appsettings.json
        var apiKey = builder.Configuration["ASP:KEY"];

// GET /faq — returns the contents of faq.json
        app.MapGet("/faq", (HttpContext context) =>
        {
            // Check if the request contains the correct API key in the header
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey) || providedKey != apiKey)
                return Results.Unauthorized();

            // Build the path to the faq.json file (relative to the project root)
            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "faq.json");

            if (!File.Exists(filePath))
                return Results.NotFound("FAQ file not found.");

            // Read and return the file contents as plain text
            var content = File.ReadAllText(filePath);
            return Results.Content(content, "application/json");
        });

        app.Run();

    }
}

