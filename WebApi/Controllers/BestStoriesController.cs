using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private readonly ILogger<BestStoriesController> _logger;

        public BestStoriesController(ILogger<BestStoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBestStories")]
        public async Task<String> Get(int count)
        {
            return await HackerNewsClient.GetBestStories(count);
        }
    }
}