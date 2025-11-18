using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202511181019)]
public class _202511181019_CreateAppointmentReviewTable : Migration
{

    public override void Up()
    {
        Create.Table("AppointmentReviews")
            .WithColumn("id").AsString().NotNullable().PrimaryKey()
            .WithColumn("AppointmentId").AsString().NotNullable()
            .WithColumn("ClientId").AsString().NotNullable()
            .WithColumn("TechnicianId").AsString().NotNullable()
            .WithColumn("TreatmentId").AsInt64()
            .WithColumn("Rate").AsByte()
            .WithColumn("Description").AsString(10000).Nullable()
            .WithColumn("IsPublished").AsBoolean()
            .WithColumn("CreatedAt").AsDateTime2();

        Create.ForeignKey("FK_AppointmentReviews_Appointments")
            .FromTable("AppointmentReviews").ForeignColumn("AppointmentId")
            .ToTable("Appointments").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.UniqueConstraint("UQ_AppointmentReviews_Appointment")
             .OnTable("AppointmentReviews")
             .Column("AppointmentId");
    

    }
    public override void Down()
    {
            Delete.Table("AppointmentReviews");
    }
}
