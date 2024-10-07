using Homies.Data;
using Microsoft.Extensions.Primitives;

namespace Homies.Models
{
    public class EventInfoViewModel
    {

        public EventInfoViewModel(
            int id,
            string name,
            DateTime startingTime,
            string type, string organiser)
        {
            Id = id;
            Name=name;
            Type=type;
            Organiser = organiser;
            Start = startingTime.ToString(DataConstants.DateFormat);
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Start { get; set; }

        public string Type { get; set; }

        public string Organiser { get; set; }
    }
}
