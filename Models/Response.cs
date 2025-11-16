using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClaudeQuestions.Models
{
    public class Response
    {
        [JsonPropertyName("content")]
        public required List<Content> Content { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("text")]
        public required string Text { get; set; }
        [JsonPropertyName("type")]
        public required string Type { get; set; }
    }

    public class QuizResponse
    {
        [JsonPropertyName("questions")]
        public required List<QuizQuestion> Questions { get; set; }
    }

    public class QuizQuestion
    {
        [JsonPropertyName("difficulty")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required DifficultyLevel Difficulty { get; set; }
        [JsonPropertyName("questionText")]
        public required string QuestionText { get; set; }
        [JsonPropertyName("options")]
        public required List<string> Options { get; set; }
        [JsonPropertyName("correctAnswerIndex")]
        public required int CorrectAnswerIndex { get; set; }
    }

    public enum DifficultyLevel
    {
        Easy, Medium, Hard
    }
}
