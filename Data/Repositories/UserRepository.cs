using Authentication.Models;
using Data.Contexts;
using Data.Interfaces;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<AppUser>(context), IUserRepository
{
}
