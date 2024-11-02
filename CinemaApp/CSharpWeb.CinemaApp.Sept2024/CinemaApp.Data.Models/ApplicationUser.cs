using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Data.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id= Guid.NewGuid();
        }

        public virtual ICollection<ApplicationUserMovie> ApplicationUserMovies { get; set; } =
            new HashSet<ApplicationUserMovie>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
