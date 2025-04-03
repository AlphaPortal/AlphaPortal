using Authentication.Models;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class UserService(IUserRepository userRepository, UserManager<AppUser> userManager, RoleManager<AppUser> roleManager)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<AppUser> _roleManager = roleManager;

    public async Task<UserResult<IEnumerable<User>>> GetUsersAsync()
    {
        var repositoryResult = await _userRepository.GetAllAsync(orderByDescending: false, sortByColumn: x => x.Profile!.FirstName!);
        var entities = repositoryResult.Result;
        if (entities != null)
        {
            var users = entities.Select(x => x.MapTo<User>()) ?? [];
            return new UserResult<IEnumerable<User>> { Succeeded = true, StatusCode = 200, Result = users };
        }

        return new UserResult<IEnumerable<User>> { Succeeded = false, StatusCode = 400, Error = repositoryResult.Error };
    }

    
}
