using Microsoft.AspNetCore.Identity;
using Phlox_API.Data;

namespace Phlox_API.Models
{
    public class User : IdentityUser
    {

        public User()
        {
            Disabilities = new HashSet<Disability>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDisabled { get; set; }
        public ICollection<Disability> Disabilities { get; set; }

    }
}
