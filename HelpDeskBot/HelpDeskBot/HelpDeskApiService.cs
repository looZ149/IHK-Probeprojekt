namespace HelpDeskBot;

//Gonna use it for DI

public class HelpDeskApiService
{
    private string apiKey;
    private string apiUrl;
    private HttpClient httpClient; // For the sake of simplicity ill go that way instead of using the IHttpClientFactory way. Its fine for this projects scope

    public HelpDeskApiService(string apiKey, string apiUrl, HttpClient httpClient)
    {
        this.apiKey = apiKey;
        this.apiUrl = apiUrl;
        this.httpClient = httpClient;
    }

    public async Task<string> GetFaq()
    {
        var url = this.apiUrl + "/faq";
        HttpRequestMessage getMessage = new HttpRequestMessage(HttpMethod.Get, url);
        getMessage.Headers.Add("X-Api-Key", apiKey);
        HttpResponseMessage getMessageBody = await httpClient.SendAsync(getMessage);
        getMessageBody.EnsureSuccessStatusCode(); //Do proper error handling later ?
        return await getMessageBody.Content.ReadAsStringAsync();
    }

    public async Task<string> SearchFiles(string query)
    {
        var url = this.apiUrl += "/docs/search?q=" + query;
        HttpRequestMessage getMessage = new HttpRequestMessage(HttpMethod.Get, url);
        getMessage.Headers.Add("X-Api-Key", apiKey);
        HttpResponseMessage getMessageBody = await httpClient.SendAsync(getMessage);
        getMessageBody.EnsureSuccessStatusCode();
        return await getMessageBody.Content.ReadAsStringAsync();
    }
}