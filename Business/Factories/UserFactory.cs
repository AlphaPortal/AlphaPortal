using Authentication.Models;
using Domain.Models;

namespace Business.Factories;

public static class UserFactory
{
    public static AppUser Create(User user)
    {
        var entity = new AppUser
        {
            Profile = new AppUserProfile
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JobTitle = user.JobTitle,
                PhoneNumber = user.PhoneNumber,
            },

           Address = new AppUserAddress
           {
               StreetName = user.StreetName,
               PostalCode = user.PostalCode,
               City = user.City,
               Country = user.Country
           }
        };

        return entity;
    }
}