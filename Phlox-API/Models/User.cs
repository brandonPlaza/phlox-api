using Microsoft.AspNetCore.Identity;
using Phlox_API.Data;

namespace Phlox_API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDisabled { get; set; }
        public List<Disability> Disabilities { get; set; }

    }
}
