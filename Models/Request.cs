using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClaudeQuestions.Models
{
    public class Request
    {
        [JsonPropertyName("model")]
        public required string Model { get; set; }
        [JsonPropertyName("max_tokens")]
        public required int MaxTokens { get; set; }
        [JsonPropertyName("messages")]
        public required List<Message> Messages { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";
        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }

    
}
