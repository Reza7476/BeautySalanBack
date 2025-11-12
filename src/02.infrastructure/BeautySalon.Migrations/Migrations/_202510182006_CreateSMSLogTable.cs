using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510182006)]
public class _202510182006_CreateSMSLogTable : Migration
{

    public override void Up()
    {
        Create.Table("SMSLogs")

            .WithColumn("Id").AsString().NotNullable().PrimaryKey()

            .WithColumn("Title").AsString().NotNullable()

            .WithColumn("Content").AsString().NotNullable()

            .WithColumn("ReceiverNumber").AsString().NotNullable()

            .WithColumn("RecId").AsInt64()

            .WithColumn("Status").AsByte()

            .WithColumn("ResponseContent").AsString().Nullable()

            .WithColumn("CreatedAt").AsDateTime2();
    }
    public override void Down()
    {
        Delete.Table("SMSLogs");
    }
}
