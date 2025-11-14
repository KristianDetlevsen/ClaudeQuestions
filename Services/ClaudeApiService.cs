using ClaudeQuestions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClaudeQuestions.Services
{
    static class ClaudeApiService
    {
        public static async Task<Response?> SendRequest(HttpClient httpClient, string baseUrl, Request request)
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
    }
}
