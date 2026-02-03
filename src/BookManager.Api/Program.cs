using System.Data;
using Book_manager.src.BookManager.Application;
using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Infrastructure.repositories;
using FluentMigrator.Runner;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//  registra MediatR dentro do Application
builder.Services.AddApplication();

// Repositórios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Conexão Dapper
builder.Services.AddScoped<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// FluentMigrator
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(config => config
        .AddPostgres()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(l => l.AddFluentMigratorConsole());

var app = builder.Build();

// Executar migrations
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
