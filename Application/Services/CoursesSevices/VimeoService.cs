using Adros.Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Adros.Application.Services.CoursesSevices
{
    public class VimeoService : IVimeoService
    {
        private readonly HttpClient _httpClient;

        public VimeoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.vimeo.com/");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "8036a09f3b15547e948a5977cdd0d70d");
        }

        public async Task<int> GetDurationAsync(string videoId)
        {
            var res = await _httpClient.GetAsync($"videos/{videoId}");
            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            return doc.RootElement.GetProperty("duration").GetInt32();
        }
    }

}
