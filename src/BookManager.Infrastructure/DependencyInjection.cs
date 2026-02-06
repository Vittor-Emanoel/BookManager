using System.Data;
using System.Text;
using Book_manager.src.BookManager.Application.Interfaces;
using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Infrastructure.Auth;
using Book_manager.src.BookManager.Infrastructure.repositories;
using Book_manager.src.BookManager.Infrastructure.Security;
using FluentMigrator.Runner;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace Book_manager.src.BookManager.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    // Dapper Connection
    services.AddScoped<IDbConnection>(_ =>
        new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

    // Repositórios
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IBookRepository, BookRepository>();
    services.AddSingleton<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwtService, JwtService>();

    // Unit of Work
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Current User Service
    services.AddHttpContextAccessor();
    services.AddScoped<ICurrentUserService, CurrentUserService>();

    // JWT Authentication
    var jwtKey = configuration["Jwt:Key"]!;
    var jwtIssuer = configuration["Jwt:Issuer"]!;
    var jwtAudience = configuration["Jwt:Audience"]!;

    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
      };
    });

    services.AddAuthorization();

    // FluentMigrator
    services.AddFluentMigratorCore()
        .ConfigureRunner(config => config
            .AddPostgres()
            .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
            .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
        .AddLogging(l => l.AddFluentMigratorConsole());

    return services;
  }
}

