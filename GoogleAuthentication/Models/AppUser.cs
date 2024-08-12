using Microsoft.AspNetCore.Identity;

namespace GoogleAuthentication.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
