using Microsoft.AspNetCore.Identity;

namespace inventory_dot_core.Admin.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; }
    }
}