using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202512042027)]
public class _202512042027_CreateNotificationTable : Migration
{

    public override void Up()
    {
        Create.Table("Notifications")
            .WithColumn("Id").AsString().PrimaryKey().NotNullable()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Body").AsString().NotNullable()
            .WithColumn("Receiver").AsString().NotNullable()
            .WithColumn("Type").AsString().NotNullable()
            .WithColumn("FCMToken").AsString().NotNullable()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("IsSent").AsBoolean()
            .WithColumn("CreatedAt").AsDateTime2()
            .WithColumn("SentAt").AsDateTime2();
    }

    public override void Down()
    {
        Delete.Table("Notifications");
    }
}
