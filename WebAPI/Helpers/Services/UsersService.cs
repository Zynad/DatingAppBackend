using System.Collections;
using WebAPI.Helpers.Repositories;
using WebAPI.Models.Entities;

namespace WebAPI.Helpers.Services;

public class UsersService
{
    private readonly UsersRepo _repo;

    public UsersService(UsersRepo repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {
        var users = await _repo.GetAllAsync();
        return users;
    }

    public async Task<AppUser> GetUserAsync(int id)
    {
        var user = await _repo.GetAsync(x => x.Id == id);
        if(user != null)
        {
            return user;
        }
        return null!;
    }
}
