using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Adros.Infrastructure.External
{
    public class BunnyVideoService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private const string ApiKey = "19c71478-e8de-46b8-848ee8c1b358-b357-4ac8";  
        private const string LibraryId = "378479";

        public async Task<string?> GetVideoUrlAsync(string videoId)
        {
            var requestUri = $"https://video.bunnycdn.com/library/{LibraryId}/videos/{videoId}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

            var response = await _httpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var videoData = JsonSerializer.Deserialize<BunnyVideoResponse>(jsonResponse);

            return $"https://iframe.mediadelivery.net/embed/{LibraryId}/{videoId}";
        }

        public async Task<string?> UploadVideoAsync(string filePath, string videoTitle)
        {
            var requestUri = $"https://video.bunnycdn.com/library/{LibraryId}/videos";

            // Step 1: Create a Video Record on Bunny.net
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
            requestMessage.Headers.Add("AccessKey", ApiKey);  

            var content = new StringContent(JsonSerializer.Serialize(new { title = videoTitle }), Encoding.UTF8, "application/json");
            requestMessage.Content = content;

            var response = await _httpClient.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error creating video record: {response.StatusCode} - {errorResponse}");
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var videoData = JsonSerializer.Deserialize<BunnyVideoResponse>(jsonResponse);
            string videoId = videoData.guid.ToString();

            // Step 2: Upload the video file
            var uploadUri = $"https://video.bunnycdn.com/library/{LibraryId}/videos/{videoId}";
            using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");

            using var uploadRequest = new HttpRequestMessage(HttpMethod.Put, uploadUri)
            {
                Content = fileContent
            };
            uploadRequest.Headers.Add("AccessKey", ApiKey);  // Correct Authorization Header

            var uploadResponse = await _httpClient.SendAsync(uploadRequest);
            return uploadResponse.IsSuccessStatusCode ? videoId : null;
        }



    }




    public class BunnyVideoResponse
    {
        public Guid guid { get; set; }
    }

}
