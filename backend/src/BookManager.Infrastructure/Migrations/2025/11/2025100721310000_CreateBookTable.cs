using FluentMigrator;

namespace Book_manager.Migrations;

[Migration(2025100721310000)]
public class CreateBookTable : Migration
{
  public override void Down()
  {
    if (Schema.Table("Books").Exists())
    {
      Delete.Table("Books");
    }
  }

  public override void Up()
  {
    if (!Schema.Table("Books").Exists())
    {
      Create.Table("Books")
        .WithColumn("Id").AsInt32().Identity().PrimaryKey()
        .WithColumn("Name").AsAnsiString().NotNullable().PrimaryKey()
        .WithColumn("Author").AsAnsiString().NotNullable().PrimaryKey()
        .WithColumn("Rating").AsInt16().NotNullable()
        .WithColumn("Status").AsInt32().NotNullable()
        .WithColumn("Description").AsAnsiString();
    }
  }
}