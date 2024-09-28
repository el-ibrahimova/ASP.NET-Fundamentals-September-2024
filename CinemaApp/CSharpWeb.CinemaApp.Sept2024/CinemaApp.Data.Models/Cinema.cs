namespace CinemaApp.Data.Models
{
    public class Cinema
    {
        // it is better to make inline initialization, not with constructor (for Guid and ICollection)

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;

        public virtual ICollection<CinemaMovie> MovieCinemas { get; set; } = new HashSet<CinemaMovie>();
    }
}
