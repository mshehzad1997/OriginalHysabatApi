using Microsoft.EntityFrameworkCore.Migrations;

namespace HYSABATApi.Migrations
{
    public partial class IdRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "pricingPlans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeatureId",
                table: "pricingPlans",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
