using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Helpers.Repositories;
using WebAPI.Helpers.Services;
using WebAPI.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UsersRepo>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRepo>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
