using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202512031713)]
public class _202512031713_CreateUserFCMTokenTable : Migration
{

    public override void Up()
    {
        Create.Table("UserFCMTokens")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("UserId").AsString().PrimaryKey()
            .WithColumn("FCMToken").AsString().NotNullable()
            .WithColumn("Role").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime2()
            .WithColumn("IsActive").AsBoolean();
        Create.ForeignKey("FK_Users_UserFCMTokens")
            .FromTable("UserFCMTokens")
            .ForeignColumn("UserId")
            .ToTable("Users")
            .PrimaryColumn("Id");

    }
    public override void Down()
    {
        Delete.Table("UserFCMTokens");
    }
}
