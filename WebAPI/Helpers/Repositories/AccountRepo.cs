using WebAPI.Contexts;
using WebAPI.Models.Entities;

namespace WebAPI.Helpers.Repositories;

public class AccountRepo : Repo<AppUser>
{
    public AccountRepo(DataContext context) : base(context)
    {
    }
}
