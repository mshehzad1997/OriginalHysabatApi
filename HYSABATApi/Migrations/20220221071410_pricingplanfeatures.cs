using Microsoft.EntityFrameworkCore.Migrations;

namespace HYSABATApi.Migrations
{
    public partial class pricingplanfeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "planfeaturesR",
                table: "pricingPlans");

            migrationBuilder.DropColumn(
                name: "planfeaturesS",
                table: "pricingPlans");

            migrationBuilder.DropColumn(
                name: "planfeaturesT",
                table: "pricingPlans");

            migrationBuilder.DropColumn(
                name: "planfeaturesU",
                table: "pricingPlans");

            migrationBuilder.RenameColumn(
                name: "planfeaturesZ",
                table: "pricingPlans",
                newName: "PricingPlanFeatures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricingPlanFeatures",
                table: "pricingPlans",
                newName: "planfeaturesZ");

            migrationBuilder.AddColumn<string>(
                name: "planfeaturesR",
                table: "pricingPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "planfeaturesS",
                table: "pricingPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "planfeaturesT",
                table: "pricingPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "planfeaturesU",
                table: "pricingPlans",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
