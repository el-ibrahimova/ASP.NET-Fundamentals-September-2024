using CinemaApp.Services.Data.Interfaces;

namespace CinemaApp.Services.Data
{
    public class BaseService:IBaseService
    {
        public bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            // non-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            // invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(id, out parsedGuid);

            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
