using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Punk.Application.Services;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Interfaces.Services;
using Punk.Domain.Models;
using Punk.Infrastructure;
using Punk.Infrastructure.Repositories;
using Punk.Services.Helpers;


namespace Punk.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // MediatR / Fluent
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        // Config
        services.AddSingleton<JwtConfig>(configuration.GetSection("JwtConfig").Get<JwtConfig>());
        // Services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IBeerApiService, BeerApiService>();
        services.AddScoped<IPasswordService, PasswordService>();
        //Repositories
        services.AddScoped<IBeerRepository, BeerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        //Persistence
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}