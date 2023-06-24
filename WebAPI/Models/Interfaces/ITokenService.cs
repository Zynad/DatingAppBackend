using WebAPI.Models.Entities;

namespace WebAPI.Models.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
