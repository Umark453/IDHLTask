using DeveloperAssessment.Web.Models;

namespace DeveloperAssessment.Web.Services
{
    public interface IBlogPostService
    {
        List<BlogPost> GetAllBlogPosts();
        BlogPost GetBlogPost(int id);
        bool AddComment(int postId, Comment comment);
    }
}
