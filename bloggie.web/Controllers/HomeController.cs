using bloggie.web.Models;
using bloggie.web.Models.viewModels;
using bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bloggie.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagrepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository  blogPostRepository,
            ITagRepository tagrepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagrepository = tagrepository;
        }
        
        public async Task< IActionResult> Index()
        {
            //getting all blogs
            var blogposts =await blogPostRepository.GetAllAsync();

            //getting all tags 
            var tags = await tagrepository.GetAllASync();

            var model = new HomeView
            {
                BlogPosts = blogposts,
                Tags = tags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
