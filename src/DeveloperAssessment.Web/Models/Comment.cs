using System.ComponentModel.DataAnnotations;

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
    }
}
