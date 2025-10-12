using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;

[Migration(202508280913)]
public class _202508280913_CreateAboutUsTable : Migration
{

    public override void Up()
    {
        Create.Table("AboutUs")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn("MobileNumber").AsString().NotNullable()
            .WithColumn("Telephone").AsString().Nullable()
            .WithColumn("Address").AsString().Nullable()
            .WithColumn("Latitude").AsDouble().Nullable()
            .WithColumn("Longitude").AsDouble().Nullable()
            .WithColumn("Description").AsString().Nullable()
            
            .WithColumn("Email").AsString().Nullable()
            .WithColumn("Instagram").AsString().Nullable()
            .WithColumn("UniqueName").AsString().Nullable()
            .WithColumn("ImageName").AsString().Nullable()
            .WithColumn("Extension").AsString().Nullable()
            .WithColumn("URL").AsString().Nullable()

            .WithColumn("CreateDate").AsDateTime2().NotNullable();
    }


    public override void Down()
    {
        Delete.Table("AboutUs");
    }
}
