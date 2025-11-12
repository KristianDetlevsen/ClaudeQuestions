// See https://aka.ms/new-console-template for more information
using ClaudeQuestions.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

// Use clients
var httpClient = new HttpClient();
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// User message
string promt = "Hello, Claude!";
string noAnswer = "Nothing";

// Get api data from appsettings
string? baseUrl = config["AnthropicApi:BaseUrl"];
string? apiKey = config["AnthropicApi:ApiKey"];
string? anthropicVersion = config["AnthropicApi:AnthropicVersion"];
string? contentType = config["AnthropicApi:ContentType"];
string? model = config["AnthropicApi:Model"];
string? maxTokens = config["AnthropicApi:MaxTokens"];

// Add headers
httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
httpClient.DefaultRequestHeaders.Add("anthropic-version", anthropicVersion);
httpClient.DefaultRequestHeaders.Add("content-type", contentType);

// The conversation with Claude
Console.WriteLine($"User says: {promt}");

List<Message> messages = [];

Message message = new()
{
    Content = promt,
};

messages.Add(message);

Request request = new() 
{ 
    Model = model,
    MaxTokens = int.Parse(maxTokens),
    Messages = messages
};

// TODO make sure to check for null
Response reply = await SendRequest(request);

Console.WriteLine($"Claude says: {reply.Content[0]}");

async Task<Response> SendRequest(Request request)
{
    try
    {
        var response = await httpClient.PostAsJsonAsync(baseUrl, request);
        
        // Write status code for debugging
        Console.WriteLine(response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            var reply = await response.Content.ReadFromJsonAsync<Response>();
            return reply;
        }
        return null;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return null;
    }
}
