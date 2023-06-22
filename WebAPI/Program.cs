using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Helpers.Repositories;
using WebAPI.Helpers.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UsersRepo>();

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.MapControllers();
app.Run();
