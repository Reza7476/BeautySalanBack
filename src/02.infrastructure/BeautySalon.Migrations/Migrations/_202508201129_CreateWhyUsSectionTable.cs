using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202508201129)]
public class _202508201129_CreateWhyUsSectionTable : Migration
{
    public override void Up()
    {
        Create.Table("Why_Us_Sections")
            .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Identity()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Description").AsString(500).NotNullable()
            .WithColumn("ImageUniqueName").AsString().NotNullable()
            .WithColumn("ImageName").AsString().NotNullable()
            .WithColumn("Extension").AsString().NotNullable()
            .WithColumn("URL").AsString().NotNullable()
            .WithColumn("CreateDate").AsDateTime2();
        Create.Table("Why_Us_Questions")
            .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Identity()
            .WithColumn("SectionId").AsInt64().NotNullable()
            .WithColumn("Question").AsString(500).NotNullable()
            .WithColumn("Answer").AsString(500).NotNullable()
            .WithColumn("CreateDate").AsDateTime2().NotNullable();
        Create.ForeignKey("FK_Why_Us_Sections_Why_Us_Questions")
            .FromTable("Why_Us_Questions")
            .ForeignColumn("SectionId")
            .ToTable("Why_Us_Sections")
            .PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("Why_Us_Questions");
        Delete.Table("Why_Us_Sections");
    }
}
