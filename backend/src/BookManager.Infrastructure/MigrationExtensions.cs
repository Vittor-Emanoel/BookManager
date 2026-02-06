using FluentMigrator.Runner;
namespace Book_manager.src.BookManager.Infrastructure;

public static class MigrationExtensions
{
  public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();

    return app;
  }
}
