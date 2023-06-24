using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using WebAPI.Helpers.Services;
using WebAPI.Models.Schemas;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserSchema schema)
    {
        if(ModelState.IsValid)
        {
            var user = await _accountService.UserExistsAsync(schema.UserName);
            if (user == null)
            {
                var result = await _accountService.RegisterAsync(schema.UserName, schema.Password);
                if (result != null)
                {
                    return Created("",result);
                }
                return Problem();
            }           
            return Conflict();
        }
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser (LoginUserSchema schema)
    {
        if (ModelState.IsValid)
        {
            var user = await _accountService.UserExistsAsync(schema.UserName);
            if (user != null)
            {
                var result = await _accountService.LoginAsync(user,schema.Password);
                if (result == "Success")
                {
                    return Ok(user);
                }
            }
            return Unauthorized();
        }
        return BadRequest();
    }
}
