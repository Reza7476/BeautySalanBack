using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510271631)]
public class _202510271631_CreateClientTable : Migration
{

    public override void Up()
    {
        Create.Table("Clients")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("CreatedAt").AsDateTime2()
            .WithColumn("UserId").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Clients");
    }
}
