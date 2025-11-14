using ClaudeQuestions.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeQuestions.Configuration
{
    public class AppSettings
    {
        public string? BaseUrl { get; set; }
        public string? ApiKey { get; set; }
        public string? Version { get; set; }
        public int MaxTokens { get; set; }
        public string? Model { get; set; }

        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(BaseUrl))
                errors.Add("BaseUrl is required");
            if (string.IsNullOrEmpty(ApiKey))
                errors.Add("ApiKey is required");
            if (string.IsNullOrEmpty(Version))
                errors.Add("Version is required");
            if (MaxTokens <= 0)
                errors.Add("MaxTokens must be greater than 0");
            if (string.IsNullOrEmpty(Model))
                errors.Add("Model is required");

            return errors;
        }
    }
}
