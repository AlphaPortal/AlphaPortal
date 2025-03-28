using Microsoft.AspNetCore.Identity;

namespace Authentication.Models;

public class AppUser : IdentityUser
{
    public virtual AppUserProfile? Profile { get; set; }
    public virtual AppUserAddress? Address { get; set; }
}
