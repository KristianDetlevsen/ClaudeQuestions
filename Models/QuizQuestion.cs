using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClaudeQuestions.Models
{
    public class QuizQuestion
    {
        [JsonPropertyName("difficulty")]
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
