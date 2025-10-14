using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510141545)]
public class _202510141545_CreateRefreshToken : Migration
{
    public override void Up()
    {
        Create.Table("RefreshTokens")
             .WithColumn("Id").AsInt64().PrimaryKey().Identity()
             .WithColumn("Token").AsString(550).NotNullable()
             .WithColumn("ExpireAt").AsDateTime2().NotNullable()
             .WithColumn("IsRevoked").AsBoolean()
             .WithColumn("UserId").AsString().NotNullable();
        Create.ForeignKey("FK_User_RefreshToken")
            .FromTable("RefreshTokens")
            .ForeignColumn("UserId")
            .ToTable("Users")
            .PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("RefreshTokens");
    }
}
