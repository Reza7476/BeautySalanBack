using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510111952)]
public class _202510111952_CreateUserTable : Migration
{
    public override void Up()
    {
        AddUserTable();
        AddRoleTable();
        AddUserRoleTable();

    }
    public override void Down()
    {
        Delete.Table("UserRoles");
        Delete.Table("Roles");
        Delete.Table("Users");
    }


    private void AddRoleTable()
    {
        Create.Table("Roles")
             .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
             .WithColumn("RoleName").AsString().NotNullable()
             .WithColumn("CreationDate").AsDateTime2();
    }

    private void AddUserRoleTable()
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


    private void AddUserTable()
    {
        Create.Table("Users")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString().Nullable()
            .WithColumn("LastName").AsString().Nullable()
            .WithColumn("Mobile").AsString().Nullable()
            .WithColumn("UserName").AsString().Nullable()
            .WithColumn("HashPass").AsString().Nullable()
            .WithColumn("Email").AsString().Nullable()
            .WithColumn("IsActive").AsBoolean()
            .WithColumn("CreationDate").AsDateTime2().NotNullable()
            .WithColumn("BirthDate").AsDateTime2().Nullable()
            .WithColumn("UniqueName").AsString().Nullable()
            .WithColumn("ImageName").AsString().Nullable()
            .WithColumn("Extension").AsString().Nullable()
            .WithColumn("URL").AsString().Nullable();
    }

}
