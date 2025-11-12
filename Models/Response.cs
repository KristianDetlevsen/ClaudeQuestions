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
    }
}
