using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using YouTubeApiProject.Models;

namespace YouTubeApiProject.Services
{
    public class YouTubeApiService
    {
        private readonly string _apiKey;

        public YouTubeApiService(IConfiguration configuration)
        {
            _apiKey = configuration["YouTubeApiKey"]; // Fetch API key from appsettings.json
        }

        public async Task<List<YouTubeVideoModel>> SearchVideosAsync(string query)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "YoutubeProject"
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = query; // User's query from form input
            searchRequest.MaxResults = 10;

            var searchResponse = await searchRequest.ExecuteAsync();

            var videos = searchResponse.Items
                .Where(item => item.Id.Kind == "youtube#video") // Ensure it's a video
                .Select(item => new YouTubeVideoModel
                {
                    VideoId = item.Id.VideoId, // Extract VideoId
                    Title = item.Snippet.Title,
                    Description = item.Snippet.Description,
                    ThumbnailUrl = item.Snippet.Thumbnails.Medium.Url
                }).ToList();

            return videos;
        }
    }
}
