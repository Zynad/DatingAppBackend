using System.Security.Cryptography;
using System.Text;
using WebAPI.Helpers.Repositories;
using WebAPI.Models.Dtos;
using WebAPI.Models.Entities;
using WebAPI.Models.Interfaces;

namespace WebAPI.Helpers.Services;

public class AccountService
{
    private readonly AccountRepo _accountRepo;
    private readonly ITokenService _tokenService;

    public AccountService(AccountRepo accountRepo, ITokenService tokenService)
    {
        _accountRepo = accountRepo;
        _tokenService = tokenService;
    }

    public async Task<UserDto> RegisterAsync(string username, string password)
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
            return new UserDto
            {
                Username = result.UserName,
                Token = _tokenService.CreateToken(result)
            };
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

    public async Task<UserDto> LoginAsync(AppUser user, string password)
    {
        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return null!;
        }
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }
}
