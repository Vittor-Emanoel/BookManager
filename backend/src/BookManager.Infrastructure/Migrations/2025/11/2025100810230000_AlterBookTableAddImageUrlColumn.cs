using FluentMigrator;

namespace Book_manager.Migrations;

[Migration(2025100810230000)]
public class AlterBookTableAddImageUrlColumn : Migration
{
  public override void Down()
  {
    Delete.Column("ImageUrl").FromTable("Books");
  }

  public override void Up()
  {
    Alter.Table("Books")
             .AddColumn("ImageUrl")
             .AsString(255)
             .Nullable();
  }
}