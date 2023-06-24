using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Entities;

public class AppUser
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public Byte[] PasswordHash { get; set; } = null!;
    public Byte[] PasswordSalt { get; set; } = null!;
}
