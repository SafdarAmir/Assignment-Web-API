using Microsoft.AspNetCore.Mvc;
using YouTubeApiProject.Services;
using YouTubeApiProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks; // Include this for async tasks

namespace YouTubeApiProject.Controllers
{
    public class YouTubeController : Controller
    {
        private readonly YouTubeApiService _youtubeService;
        public YouTubeController(YouTubeApiService youtubeService)
        {
            _youtubeService = youtubeService;
        }

        // Display Search Page
        public IActionResult Index()
        {
            return View(); // No need to pass a model initially, just render the search form
        }

        // Handle the search query
        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            var videos = await _youtubeService.SearchVideosAsync(query);
            ViewBag.Query = query;
            // Redirect to Results view with the search results
            return View("Results", videos);
        }
    }
}
