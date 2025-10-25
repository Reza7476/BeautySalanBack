using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510251434)]
public class _202510251434_CreateWeeklyScheduleTable : Migration
{

    public override void Up()
    {
        Create.Table("WeeklySchedules")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("DayOfWeek").AsByte()
            .WithColumn("StartTime").AsDateTime2()
            .WithColumn("EndTime").AsDateTime2()
            .WithColumn("IsActive").AsBoolean();
    }

    public override void Down()
    {
        Delete.Table("WeeklySchedules");
    }
}
