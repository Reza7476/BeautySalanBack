using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510181318)]
public class _202510181318_CreateOtpRequestTable : Migration
{

    public override void Up()
    {
        Create.Table("OtpRequests")
        .WithColumn("Id").AsString().NotNullable().PrimaryKey()
        .WithColumn("Mobile").AsString().NotNullable()
        .WithColumn("OtpCode").AsString().NotNullable()
        .WithColumn("IsUsed").AsBoolean()
        .WithColumn("ExpireAt").AsDateTime2()
        .WithColumn("CreatedAt").AsDateTime2()
        .WithColumn("Purpose").AsByte();

    }
    public override void Down()
    {
        Delete.Table("OtpRequests");
    }
}
