using bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace bloggie.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task< IActionResult> Index(string urlHandle)
        {
            var blogpost =await blogPostRepository.GetUrlHandleAsync(urlHandle);
            return View(blogpost);
        }
    }
}
