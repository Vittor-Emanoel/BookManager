using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(2026020200000)]
public class CreateUsersTable : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString(150).NotNullable()
            .WithColumn("Email").AsString(150).NotNullable().Unique()
            .WithColumn("PasswordHash").AsString(500).NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}
