using FluentMigrator;

namespace BeautySalon.Migrations.Migrations;


[Migration(202508121631)]
public class _202508121631_CreateBannerTable : Migration
{
    public override void Up()
    {
        Create.Table("Banners")
              .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Identity()
              .WithColumn("Title").AsString().NotNullable()
              .WithColumn("ImageName").AsString().NotNullable()
              .WithColumn("ImageUniqueName").AsString().NotNullable()
              .WithColumn("Extension").AsString().NotNullable()
              .WithColumn("URL").AsString().NotNullable()
              .WithColumn("CreateDate").AsDateTime2();
    }

    public override void Down()
    {
        Delete.Table("Banners");
    }
}
