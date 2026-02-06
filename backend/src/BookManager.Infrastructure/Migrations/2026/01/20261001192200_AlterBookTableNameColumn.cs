using FluentMigrator;

namespace Book_manager.Migrations;

[Migration(20261001192200)]
public class AlterBookTableNameColumn : Migration
{

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

  public override void Down()
  {
    if (Schema.Table("Books").Exists())
    {
      Delete.Table("Books");
    }
  }


}