using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202510271730)]
public class _202510271730_CreateAppointmentTable : Migration
{

    public override void Up()
    {
        AddAppointmentTable();
    }


    public override void Down()
    {
        Delete.Table("Appointments");
    }

    private void AddAppointmentTable()
    {
        Create.Table("Appointments")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("ClientId").AsString().NotNullable()
            .WithColumn("TechnicianId").AsString().NotNullable()
            .WithColumn("TreatmentId").AsInt64()
            .WithColumn("AppointmentDate").AsDateTime2()
            .WithColumn("EndTime").AsDateTime2()
            .WithColumn("Duration").AsInt32()
            .WithColumn("Status").AsByte()
            .WithColumn("Description").AsString(1000).Nullable()
            .WithColumn("CancelledBy").AsString().Nullable()
            .WithColumn("CancelledAt").AsDateTime2().Nullable()
            .WithColumn("CreatedAt").AsDateTime2();

        Create.ForeignKey("Fk_Appointments_Clients")
            .FromTable("Appointments")
            .ForeignColumn("ClientId")
            .ToTable("Clients")
            .PrimaryColumn("Id");

        Create.ForeignKey("FK_Appointments_Treatments")
            .FromTable("Appointments")
            .ForeignColumn("TreatmentId")
            .ToTable("Treatments")
            .PrimaryColumn("Id");

        Create.ForeignKey("Fk_Appointments_Technicians")
            .FromTable("Appointments")
            .ForeignColumn("TechnicianId")
            .ToTable("Technicians")
            .PrimaryColumn("Id");
    }

}
