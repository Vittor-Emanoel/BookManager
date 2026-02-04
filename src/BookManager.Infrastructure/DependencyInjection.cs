using System.Data;
using Book_manager.src.BookManager.Application.Interfaces;
using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Infrastructure.repositories;
using Book_manager.src.BookManager.Infrastructure.Security;
using FluentMigrator.Runner;
using Infrastructure.Migrations;
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
