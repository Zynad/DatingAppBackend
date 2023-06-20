using WebAPI.Contexts;
using WebAPI.Models.Entities;

namespace WebAPI.Helpers.Repositories;

public class UsersRepo : Repo<AppUser>
{
    public UsersRepo(DataContext context) : base(context)
    {
    }
}
