namespace SeminarHub.Models
{
    public class AllSeminarViewModel
    {
        public int Id { get; set; }
        public required string Topic { get; set; } = null!;
        public required string Lecturer { get; set; } = null!;
        public required string Category { get; set; } = null!;
        public required string Organizer { get; set; } = null!;
        public string? DateAndTime { get; set; } = null!;
    }
}
