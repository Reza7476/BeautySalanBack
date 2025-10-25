using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202508191502)]
public class _202508191502_CreateTreatmentTable : Migration
{

    public override void Up()
    {
        Create.Table("Treatments")
            .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Identity()
            .WithColumn("Description").AsString(1000).NotNullable()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Duration").AsInt32().WithDefaultValue(180)
            .WithColumn("CreateDate").AsDateTime2();
        Create.Table("TreatmentImages")
            .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Identity()
            .WithColumn("TreatmentId").AsInt64().NotNullable()
            .WithColumn("ImageUniqueName").AsString().NotNullable()
            .WithColumn("ImageName").AsString().NotNullable()
            .WithColumn("Extension").AsString().NotNullable()
            .WithColumn("URL").AsString().NotNullable()
            .WithColumn("CreateDate").AsDateTime2();
        Create.ForeignKey("FK_Treatments_TreatmentImages")
            .FromTable("TreatmentImages")
            .ForeignColumns("TreatmentId")
            .ToTable("Treatments")
            .PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("TreatmentImages");
        Delete.Table("Treatments");
    }
}
