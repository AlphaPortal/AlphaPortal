using Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Contexts;

public class AuthenticationContext(DbContextOptions<AuthenticationContext> options) : IdentityDbContext<AppUser>(options)
{
    public virtual DbSet<AppUserProfile> UserProfiles { get; set; }
    public virtual DbSet<AppUserAddress> UserAddresses { get; set; }
}