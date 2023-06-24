using System.Security.Cryptography;
using System.Text;
using WebAPI.Helpers.Repositories;
using WebAPI.Models.Entities;

namespace WebAPI.Helpers.Services;

public class AccountService
{
    private readonly AccountRepo _accountRepo;

    public AccountService(AccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public async Task<AppUser> RegisterAsync(string username, string password)
    {
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };

        var result = await _accountRepo.AddAsync(user);
        if(result != null)
        {
            return result;
        }
        return null!;
    }

    public async Task<AppUser> UserExistsAsync(string username)
    {
        var result = await _accountRepo.GetAsync(x => x.UserName == username.ToLower());
        if(result != null)
        {
            return result;
        }

        return null!;
    }

    public async Task<string> LoginAsync(AppUser user, string password)
    {
        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return "Fail";
        }
        return "Success";
    }
}
