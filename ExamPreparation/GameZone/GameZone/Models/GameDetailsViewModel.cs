using GameZone.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class GameDetailsViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; } 

        public required string Description { get; set; } 
        
        public string? ImageUrl { get; set; }

        public required string ReleasedOn { get; set; }

        public required string Genre { get; set; } 

        public required string Publisher { get; set; }
    }
}

