using Microsoft.AspNetCore.Mvc;
using DeveloperAssessment.Web.Services;

namespace DeveloperAssessment.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostService _blogPostService;

        public BlogController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [Route("blog/{id}")]
        public IActionResult Details(int id)
        {
            var details = _blogPostService.GetBlogPost(id);
            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }
    }
}
