using Authentication.Models;
using Domain.Models;

namespace Authentication.Factories;

public static class UserFactory
{
    public static User Create(AppUser appUser)
    {
        var user = new User
        {
            Email = appUser.Email ?? "",
            FirstName = appUser.Profile?.FirstName ?? "",
            LastName = appUser.Profile?.LastName ?? "",
            Image = appUser.Profile?.ImageUrl ?? ""
        };

        return user;
    }
}
