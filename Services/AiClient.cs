using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartNavigator.Services
{
    public class AiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _model;

        public AiClient(HttpClient httpClient, string token, string model = "gpt2")
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api-inference.huggingface.co/models/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _model = model;
        }

        public async Task<string> QueryAsync(string text)
        {
            var payload = JsonSerializer.Serialize(new { inputs = text });
            using var content = new StringContent(payload, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync(_model, content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var generated = doc.RootElement[0].GetProperty("generated_text").GetString();
            return generated ?? string.Empty;
        }
    }
}
