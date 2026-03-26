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
        this.apiUrl = apiUrl + "/faq";
        this.httpClient = httpClient;
    }

    public async Task<string> GetFaq()
    {
        HttpRequestMessage getMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);
        getMessage.Headers.Add("X-Api-Key", apiKey);
        HttpResponseMessage getMessageBody = await httpClient.SendAsync(getMessage);
        getMessageBody.EnsureSuccessStatusCode(); //Do proper error handling later ?
        return await getMessageBody.Content.ReadAsStringAsync();
    }
}