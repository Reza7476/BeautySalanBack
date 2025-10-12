using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510111952)]
public class _202510111952_CreateUserTable : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString().Nullable()
            .WithColumn("LastName").AsString().Nullable()
            .WithColumn("Mobile").AsString().Nullable()
            .WithColumn("UserName").AsString().Nullable()
            .WithColumn("HassPass").AsString().Nullable()
            .WithColumn("Email").AsString().Nullable()
            .WithColumn("IsActive").AsBoolean()
            .WithColumn("CreationDate").AsDateTime2().NotNullable()
            .WithColumn("BirthDate").AsString().Nullable()
            .WithColumn("UniqueName").AsString().Nullable()
            .WithColumn("ImageName").AsString().Nullable()
            .WithColumn("Extension").AsString().Nullable()
            .WithColumn("URL").AsString().Nullable();
    }
    
    public override void Down()
    {
        Delete.Table("Users");
    }
}
