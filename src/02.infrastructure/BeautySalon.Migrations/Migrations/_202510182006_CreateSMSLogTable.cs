using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510182006)]
public class _202510182006_CreateSMSLogTable : Migration
{

    public override void Up()
    {
        Create.Table("SMSLogs")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("ReceiverNumber").AsString().NotNullable()
            .WithColumn("Message").AsString().NotNullable()
            .WithColumn("Status").AsByte()
            .WithColumn("ErrorMessage").AsString().Nullable()
            .WithColumn("CreatedAt").AsDateTime2()
            .WithColumn("RecId").AsInt64()
            .WithColumn("ProviderNumber").AsString().NotNullable();
    }
    public override void Down()
    {
        Delete.Table("SMSLogs");
    }
}
