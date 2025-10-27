using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510271714)]
public class _202510271714_CreateTechnicianTable : Migration
{

    public override void Up()
    {
        Create.Table("Technicians")
             .WithColumn("Id").AsString().NotNullable().PrimaryKey()
             .WithColumn("CreatedDate").AsDateTime2()
             .WithColumn("UserId").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Technicians");
    }
}
