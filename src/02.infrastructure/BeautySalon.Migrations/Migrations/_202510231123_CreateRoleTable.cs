using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510231123)]
public class _202510231123_CreateRoleTable : Migration
{
    public override void Up()
    {
        Create.Table("Roles")
             .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
             .WithColumn("RoleName").AsString().NotNullable()
             .WithColumn("CreationDate").AsDateTime2();
    }
    public override void Down()
    {
        Delete.Table("Roles");
    }
}
