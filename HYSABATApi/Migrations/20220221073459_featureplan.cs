using Microsoft.EntityFrameworkCore.Migrations;

namespace HYSABATApi.Migrations
{
    public partial class featureplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricingPlanFeatures",
                table: "pricingPlans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PricingPlanFeatures",
                table: "pricingPlans",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
