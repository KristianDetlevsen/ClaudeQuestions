// See https://aka.ms/new-console-template for more information
using ClaudeQuestions.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

// Use clients
var httpClient = new HttpClient();
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// User message
string promt = "Hello, Claude!";

// Get api data from appsettings
string? baseUrl = config["AnthropicApi:BaseUrl"];
string? apiKey = config["AnthropicApi:ApiKey"];
string? anthropicVersion = config["AnthropicApi:AnthropicVersion"];
string? contentType = config["AnthropicApi:ContentType"];

// Add headers
httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
httpClient.DefaultRequestHeaders.Add("anthropic-version", anthropicVersion);

List<Message> messages = [];
messages.Add(new Message { Content = promt });

Request request = new() 
{ 
    Model = config["AnthropicApi:Model"],
    MaxTokens = int.Parse(config["AnthropicApi:MaxTokens"]),
    Messages = messages
};

// TODO make sure to check for null
Response reply = await SendRequest(request);

// The conversation with Claude
Console.WriteLine($"User says: {promt}");
if(reply != null)
    Console.WriteLine($"Claude says: {reply.Content[0].Text}");

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
