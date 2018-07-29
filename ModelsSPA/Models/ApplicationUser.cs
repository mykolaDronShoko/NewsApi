using Microsoft.AspNetCore.Identity;

namespace ModelsSPA.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string UserImg { get; set; }
    }
}
