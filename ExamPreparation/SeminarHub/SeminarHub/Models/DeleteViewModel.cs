namespace SeminarHub.Models
{
    public class DeleteViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; } = null!;
        public string DateAndTime { get; set; } = string.Format(Common.ApplicationConstants.DateFormat);
    }
}
