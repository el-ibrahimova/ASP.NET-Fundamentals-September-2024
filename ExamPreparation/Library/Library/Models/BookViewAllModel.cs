using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookViewAllModel
    {

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        
        public string Author { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
      
        public Category Category { get; set; } = null!;

       
    }
}
