// See https://aka.ms/new-console-template for more information
using ClaudeQuestions.Configuration;
using ClaudeQuestions.Models;
using ClaudeQuestions.Services;
using Microsoft.Extensions.Configuration;

// Use clients
using HttpClient httpClient = new();
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// User message
string prompt = "Hello, Claude!";

// Check MaxTokens from appsettings
int standardTokens = 1024;
bool success = int.TryParse(config["AnthropicApi:MaxTokens"], out int tokens);
if (!success)
    tokens = standardTokens;

// Create new object with data from appsettings.json
AppSettings appSettings = new()
{
    BaseUrl = config["AnthropicApi:BaseUrl"],
    ApiKey = config["AnthropicApi:ApiKey"],
    Version = config["AnthropicApi:AnthropicVersion"],
    MaxTokens = tokens,
    Model = config["AnthropicApi:Model"]
};

// Check for errors
var errors = appSettings.GetValidationErrors();
if (errors.Count > 0)
{
    Console.WriteLine($"Configuration validation failed with {errors.Count} error(s):");
    foreach (var error in errors)
    {
        Console.WriteLine($" - {error}");
    }
    Console.WriteLine("Please check your appsettings.json file");
    return;
}

// Add headers
httpClient.DefaultRequestHeaders.Add("x-api-key", appSettings.ApiKey);
httpClient.DefaultRequestHeaders.Add("anthropic-version", appSettings.Version);

// Create request object
List<Message> messages = [];
messages.Add(new Message { Content = prompt });
Request request = new() 
{ 
    Model = appSettings.Model,
    MaxTokens = tokens,
    Messages = messages
};

// Send request and receive response from Claude
Response reply = await ClaudeApiService.SendRequest(httpClient, appSettings.BaseUrl, request);

// The conversation with Claude in the console
Console.WriteLine($"User says: {prompt}");

// Null and count check
if(reply != null && reply.Content.Count > 0)
    Console.WriteLine($"Claude says: {reply.Content[0].Text}");