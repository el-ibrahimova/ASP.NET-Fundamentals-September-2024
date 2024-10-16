using System.ComponentModel.DataAnnotations;
using static Library.Common.DataConstants;

namespace Library.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitleMaxLength), MinLength(BookTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(BookAuthorMaxLength), MinLength(BookAuthorMinLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MinLength(5)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(BookRatingMinValue, BookRatingMaxValue )]
        public decimal Rating { get; set; }

        [Required]
        [MaxLength(BookDescriptionMaxLength), MinLength(BookDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; } 
    }
}
