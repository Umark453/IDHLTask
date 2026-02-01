using System.Text.Json;
using DeveloperAssessment.Web;
using Microsoft.AspNetCore.Hosting;

namespace DeveloperAssessment.Web.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _jsonFilePath;

        public BlogPostService(IWebHostEnvironment environment)
        {
            _environment = environment;
            _jsonFilePath = Path.Combine(_environment.ContentRootPath, "Data", "Blog-Posts.json");
        }

        public List<BlogPost> GetAllBlogPosts()
        {
            if (!File.Exists(_jsonFilePath))
            {
                return new List<BlogPost>();
            }

            var json = File.ReadAllText(_jsonFilePath);
            var data = JsonSerializer.Deserialize<BlogPostData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data?.BlogPosts ?? new List<BlogPost>();
        }

        public BlogPost GetBlogPost(int id)
        {
            var posts = GetAllBlogPosts();
            return posts.FirstOrDefault(p => p.Id == id);
        }
    }
}
