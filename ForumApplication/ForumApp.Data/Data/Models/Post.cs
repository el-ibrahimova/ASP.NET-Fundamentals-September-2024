using System.ComponentModel.DataAnnotations;
using static ForumApp.Infrastructure.Constants.ValidationConstants;

namespace ForumApp.Infrastructure.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMaxLength)]

        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
