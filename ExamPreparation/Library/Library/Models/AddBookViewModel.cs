using System.ComponentModel.DataAnnotations;
using static Library.Common.DataConstants;

namespace Library.Models
{
    public class AddBookViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitleMaxLength), MinLength(BookTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(BookAuthorMaxLength), MinLength(BookAuthorMinLength)]
        public string Author { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Url { get; set; } = null!;

        [Required] 
        public string Rating { get; set; } = null!;

        [Required]
        [MaxLength(BookDescriptionMaxLength), MinLength(BookDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
