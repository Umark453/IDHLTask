using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeveloperAssessment.Web.Models
{
    public class Comment
    {
        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Message { get; set; }

        public List<Comment> Replies { get; set; }
    }
}
