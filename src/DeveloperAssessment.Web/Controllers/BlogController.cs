using DeveloperAssessment.Web.Models;
using DeveloperAssessment.Web.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("blog/{id}/comment")]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(int id, Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment?.Name) ||
                string.IsNullOrWhiteSpace(comment?.EmailAddress) ||
                string.IsNullOrWhiteSpace(comment?.Message))
            {
                var details = _blogPostService.GetBlogPost(id);
                if (details == null)
                {
                    return NotFound();
                }
                return View("Details", details);
            }

            var ok = _blogPostService.AddComment(id, comment);
            if (!ok)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
