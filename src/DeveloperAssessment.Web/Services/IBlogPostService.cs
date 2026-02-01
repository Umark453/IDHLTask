using DeveloperAssessment.Web;
using System.Collections.Generic;

namespace DeveloperAssessment.Web.Services
{
    public interface IBlogPostService
    {
        List<BlogPost> GetAllBlogPosts();
        BlogPost GetBlogPost(int id);
    }
}
