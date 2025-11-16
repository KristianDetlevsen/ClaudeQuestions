# ClaudeQuestions

A C# console application that generates AI-powered quiz questions using Anthropic's Claude API.

## About
This project demonstrates API integration with Anthropic's Claude for a school assignment. The application generates 5 multiple-choice questions about Large Language Models (LLMs) with increasing difficulty levels.

## Features
- Interactive console-based quiz interface
- AI-generated questions with configurable difficulty
- Automatic answer validation
- Score tracking and feedback
- Clean error handling and input validation

## Prerequisites
- .NET 6.0 or higher
- Anthropic API key ([Get one here](https://console.anthropic.com))

## Setup
1. Clone the repository
2. Copy appsettings.example.json to appsettings.json
3. Add your Anthropic API key to appsettings.json
4. Run the application

## Configuration
The appsettings.json file contains:
- API key and endpoint configuration
- Model selection (defaults to Claude Sonnet 4)
- Token limits

## How It Works
1. User selects "Start Quiz" from the main menu
2. Application sends a prompt to Claude API requesting 5 questions
3. Claude returns questions in JSON format
4. User answers each question
5. Application shows final score and feedback

## Technologies Used
- C# / .NET
- System.Net.Http for API calls
- System.Text.Json for JSON handling
- Anthropic Claude API

## Acknowledgments
Developed with guidance from Claude AI. Code was written independently with Claude providing code reviews, suggesting improvements, and explaining C# best practices.
This README was written by Claude AI.

## License
MIT License
