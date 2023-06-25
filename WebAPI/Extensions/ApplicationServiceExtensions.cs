using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Helpers.Repositories;
using WebAPI.Helpers.Services;
using WebAPI.Models.Interfaces;

namespace WebAPI.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<UsersService>();
        services.AddScoped<UsersRepo>();
        services.AddScoped<AccountService>();
        services.AddScoped<AccountRepo>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
