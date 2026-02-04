using DeveloperAssessment.Web.Models;
using System.Text.Json;

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

        public bool AddComment(int postId, Comment comment)
        {
            if (!File.Exists(_jsonFilePath))
            {
                return false;
            }

            var json = File.ReadAllText(_jsonFilePath);
            var data = JsonSerializer.Deserialize<BlogPostData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new BlogPostData { BlogPosts = new List<BlogPost>() };

            var post = data.BlogPosts?.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return false;
            }

            post.Comments ??= new List<Comment>();
            comment.Date = comment.Date == default ? DateTime.UtcNow : comment.Date;
            post.Comments.Add(comment);

            var writeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var updatedJson = JsonSerializer.Serialize(data, writeOptions);
            File.WriteAllText(_jsonFilePath, updatedJson);
            return true;
        }
    }
}
