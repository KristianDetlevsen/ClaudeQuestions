// See https://aka.ms/new-console-template for more information
using ClaudeQuestions.Configuration;
using ClaudeQuestions.Models;
using ClaudeQuestions.Services;
using Microsoft.Extensions.Configuration;

// Use clients
using HttpClient httpClient = new();
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Create AppSettings object and headers
AppSettings? appSettings = CreateAppSettings();
if (appSettings == null)
{
    Console.WriteLine("Failed to load configuration. Exiting.");
    return;
}
CreateHeaders(appSettings);

// User message - use precise prompt engineering
string userPrompt = "Hello, Claude!";

// Run Main Menu
do
{
    Console.WriteLine("Claude Questions --- Main menu");
    Console.WriteLine("Press 1 to get 5 questions about LLM's with increasing difficulty");
    Console.WriteLine("Press 2 to close the application");

    string? menuChoice = Console.ReadLine();

    switch(menuChoice)
    {
        case "1":
            Request request = CreateRequest(appSettings, userPrompt);
            await RunClaudeQuestions(appSettings, request);         
            break;
        case "2":
            return;
        default:
            Console.WriteLine("Sorry wrong input - please try again");
            break;
    }
} while (true);

async Task RunClaudeQuestions(AppSettings appSettings, Request request)
{
    Console.Clear();
    Console.WriteLine("Fetching questions... please stand by");

    // Send request and receive response from Claude
    Response reply = await ClaudeApiService.SendRequest(httpClient, appSettings.BaseUrl, request);

    if (reply == null || reply.Content.Count == 0)
    {
        Console.WriteLine("Api call failed - no content received");
        return;
    }

    // Deserialize json response
    List<QuizQuestion> questions = ClaudeApiService.ParseQuizResponse(reply);
    if (questions == null  || questions.Count == 0)
    {
        Console.WriteLine("Couldn't load questions");
        return;
    }

    int score = 0;

    // Show questions and validate answers
    for (int i = 0; i < questions.Count; i++)
    {
        Console.WriteLine($"Question #{i}");
        Console.WriteLine($"Difficulty: {questions[i].Difficulty}");
        Console.WriteLine($"Question: {questions[i].QuestionText}");
        List<string> optionLetters = ["A", "B", "C", "D"];
        for (int j = 0; j < questions[i].Options.Count; j++)
        {
            Console.WriteLine($"{optionLetters[j]}: {questions[i].Options[j]}");
        }

        do
        {
            Console.WriteLine("Please type your answer and press enter: ");

            string? answer = Console.ReadLine();

            if (string.IsNullOrEmpty(answer) || !optionLetters.Contains(answer.ToUpper()))
            {
                Console.WriteLine("Please choose a valid option");
            }
            else if (answer.Equals(optionLetters[questions[i].CorrectAnswerIndex], StringComparison.CurrentCultureIgnoreCase))
            {
                score++;
                Console.WriteLine("Correct! Press any key to continue");
                Console.ReadLine();
                break;
            }
            else
            {
                Console.WriteLine($"Incorrect. The correct answer was {optionLetters[questions[i].CorrectAnswerIndex]}. Press any key for next question");
                Console.ReadLine();
                break;
            }
        }
        while (true);        
    }

    string finalScore = $"Your final score is {score}";

    if (score == 0)
    {
        Console.WriteLine($"{finalScore}. Read more about LLM's - then come back and try again!");
    }
    else if (score > 0 && score < 5)
    {
        Console.WriteLine($"{finalScore}! Well done - think you can ace Claude's questions next time?");
    }
    else if (score == 5)
    {
        Console.WriteLine($"{finalScore}! Congratulations! You are now ready for your exam!");
    }

    Console.WriteLine("Press any key to return to the main menu");
    Console.ReadLine();
    Console.Clear();
}

AppSettings? CreateAppSettings()
{
    // Check MaxTokens from appsettings
    int defaultTokens = 1024;
    bool success = int.TryParse(config["AnthropicApi:MaxTokens"], out int tokens);
    if (!success)
        tokens = defaultTokens;

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
        return null;
    }

    return appSettings;
}

void CreateHeaders(AppSettings appSettings)
{
    httpClient.DefaultRequestHeaders.Add("x-api-key", appSettings.ApiKey);
    httpClient.DefaultRequestHeaders.Add("anthropic-version", appSettings.Version);
}

Request CreateRequest(AppSettings appSettings, string prompt)
{
    Request request = new()
    {
        Model = appSettings.Model,
        MaxTokens = appSettings.MaxTokens,
        Messages = [new Message { Content = prompt }]
    };

    return request;
}

// Old test code: The conversation with Claude in the console
//Console.WriteLine($"User says: {prompt}");
//Console.WriteLine($"Claude says: {reply.Content[0].Text}");