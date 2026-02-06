using FluentMigrator;

namespace Infrastructure.Migrations._2026._02;

[Migration(202602061001)]
public class AddUserIdToBooks : Migration
{
    public override void Up()
    {
        Alter.Table("Books")
            .AddColumn("UserId").AsGuid().NotNullable().SetExistingRowsTo(Guid.Empty);

        Create.ForeignKey("FK_Books_Users_UserId")
            .FromTable("Books").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");

        Create.Index("IX_Books_UserId")
            .OnTable("Books")
            .OnColumn("UserId");
    }

    public override void Down()
    {
        Delete.Index("IX_Books_UserId");
        Delete.ForeignKey("FK_Books_Users_UserId");
        Delete.Column("UserId").FromTable("Books");
    }
}
