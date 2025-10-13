using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510131202)]
public class _202510131202_CreateUserRole : Migration
{

    public override void Up()
    {
        Create.Table("UserRoles")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey().NotNullable()
            .WithColumn("RoleId").AsInt64().NotNullable()
            .WithColumn("UserId").AsString().NotNullable();
        Create.ForeignKey("Fk_User_UserRoles")
            .FromTable("UserRoles")
            .ForeignColumn("UserId")
            .ToTable("Users")
            .PrimaryColumn("Id");
        Create.ForeignKey("FK_Role_UserRoles")
            .FromTable("UserRoles")
            .ForeignColumn("RoleId")
            .ToTable("Roles")
            .PrimaryColumn("Id");
            
    }
    public override void Down()
    {
        Delete.Table("UserRoles");
    }
}
