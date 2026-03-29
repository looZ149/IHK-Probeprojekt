
namespace HelpDeskApi;

class ApiProgram
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        var apiKey = builder.Configuration["ASP:KEY"];
        
        
        
        // GET /faq — returns the contents of faq.json
        app.MapGet("/faq", (HttpContext context) =>
        {
            
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey) || providedKey != apiKey)
                return Results.Unauthorized();

            
            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "faq.json");

            if (!File.Exists(filePath))
                return Results.NotFound("FAQ file not found.");

            
            var content = File.ReadAllText(filePath);
            return Results.Content(content, "application/json");
        });
        //GET Request for search command - Returns all files with matching strings.. Obviously?
        app.MapGet("/docs/search", (HttpContext context) =>
        {
            List<string> fileNames = new List<string>();
            
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey) || providedKey != apiKey)
                return Results.Unauthorized();


            var query = context.Request.Query["q"].ToString(); // Lovely that ASP.NET strips the query for us. Cool
            if (string.IsNullOrWhiteSpace(query))
                return Results.BadRequest("Query parameter is missing.");
            

            var docsPath = Path.Combine(AppContext.BaseDirectory, "Data", "docs");
            var filesInDocs = Directory.GetFiles(docsPath, "*.md");
 
            
            foreach(var file in filesInDocs)
            {
                var content = File.ReadAllText(file);
                bool containsKeyword = content.Contains(query, StringComparison.OrdinalIgnoreCase);

                if (containsKeyword)
                {
                    string matchedFileName = "";
                    matchedFileName = Path.GetFileNameWithoutExtension(file);
                    fileNames.Add(matchedFileName);
                }
                    
                
            }
            
            //Gotta build up a string and assign numbers so user can choose to which file we actually want to open?

            string output = "";
            foreach (string fileName in fileNames)
            {
                output += fileName + "\n"; //Uh.. ugly af? could maybe use StringBuilder ?
            }
            
            return Results.Content(output);
        });

        app.MapGet("/docs/find", (HttpContext context) =>
        {
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey) || providedKey != apiKey)
                return Results.Unauthorized();

            var query = context.Request.Query["q"].ToString();
            if (string.IsNullOrWhiteSpace(query))
                return Results.BadRequest("Query parameter is missing");

            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "docs", $"{query}.md");

            if (!File.Exists(filePath))
                return Results.NotFound("404 File not found");

            var content = File.ReadAllText(filePath);
            
            return Results.Content(content);
        });

        app.Run();

    }
}

