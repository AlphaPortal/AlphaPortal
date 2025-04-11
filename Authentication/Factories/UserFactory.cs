using Authentication.Models;
using Domain.Models;
using Microsoft.Identity.Client;

namespace Authentication.Factories;

public static class UserFactory
{
    public static User Create(AppUser appUser)
    {
        if (appUser == null)
            return new User();

        var user = new User
        {
            Id = appUser.Id,
            Email = appUser.Email ?? "",
            FirstName = appUser.Profile?.FirstName ?? "",
            LastName = appUser.Profile?.LastName ?? "",
            FullName = $"{appUser.Profile?.FirstName} {appUser.Profile?.LastName}",
            PhoneNumber = appUser.Profile?.PhoneNumber ?? "",
            JobTitle = appUser.Profile?.JobTitle ?? "",
            ImageUrl = appUser.Profile?.ImageUrl ?? "",
            StreetName = appUser.Address?.StreetName ?? "",
            PostalCode = appUser.Address?.PostalCode ?? "",
            City = appUser.Address?.City ?? "",
            Country = appUser.Address?.Country ?? ""
        };

        return user;
    }

    // Took help from ChatGpt
    public static IEnumerable<User> Create(IEnumerable<AppUser> appUsers)
    {
        var users = appUsers.Where(u => u.Profile != null).Select(Create).ToList() ?? [];
        return users;
    }
}