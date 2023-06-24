using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Schemas;

public class RegisterUserSchema
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
