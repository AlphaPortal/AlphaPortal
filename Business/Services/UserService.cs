using Authentication.Contexts;
using Authentication.Factories;
using Authentication.Models;
using Business.Interfaces;
using Domain.Models;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationContext context) : IUserService
{
    private readonly AuthenticationContext _context = context;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<UserResult> AddUserAsync(AppUser user, string role)
    {
        var password = "BytMig123!";
        if (user != null)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    return new UserResult { Succeeded = true, StatusCode = 200 };
                }
            }
        }

        return new UserResult { Succeeded = false, StatusCode = 400, Error = "Something went wrong." };
   }
    public async Task<UserResult<IEnumerable<User>>> GetUsersAsync()
    {
        var entities = await _context.Users
            .Include(u => u.Profile)
            .Include(u => u.Address)
            .ToListAsync();

        var users = UserFactory.Create(entities);

        return new UserResult<IEnumerable<User>> { Succeeded = true, StatusCode = 200, Result = users};
    }

    public async Task<UserResult<User>> GetUserByIdAsync(string id)
    {
        var entity = await _context.Users
            .Include(u => u.Profile)
            .Include(u => u.Address)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity != null)
        {
            var user = UserFactory.Create(entity);
            return new UserResult<User> { Succeeded = true, StatusCode = 200, Result = user };
        }

        return new UserResult<User> { Succeeded = false, StatusCode = 404, Error = $"User with id '{id}' was not found." };
    }

    public async Task<UserResult> UserExistsByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email)) 
        {
            return new UserResult { Succeeded = false, StatusCode = 400, Error = $"Email must be provided." };
        }

        var entity = await _context.Users
            .Include(u => u.Profile)
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (entity != null)
        {
            return new UserResult { Succeeded = true, StatusCode = 200, Error = "A user with the specified email address already exists." };
        }

        return new UserResult { Succeeded = false, StatusCode = 404, Error = $"User was not found." };
    }

    public async Task<UserResult> AddUserToRoleAsync(AppUser user, string roleName)
    {
        var result = await _roleManager.RoleExistsAsync(roleName);
        if (result)
        {
            await _userManager.AddToRoleAsync(user, roleName);
            return new UserResult { Succeeded = true, StatusCode = 200 };
        }

        return new UserResult { Succeeded = false };
    }

    public async Task<string> GetDisplayNameAsync(string userId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
                return user == null ? "" : $"{user.Profile?.FirstName} {user.Profile?.LastName}";
        }

        return "";
    }
}
