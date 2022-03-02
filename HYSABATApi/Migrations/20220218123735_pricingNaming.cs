using Microsoft.EntityFrameworkCore.Migrations;

namespace HYSABATApi.Migrations
{
    public partial class pricingNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "planfeatures5",
                table: "pricingPlans",
                newName: "planfeaturesZ");

            migrationBuilder.RenameColumn(
                name: "planfeatures4",
                table: "pricingPlans",
                newName: "planfeaturesU");

            migrationBuilder.RenameColumn(
                name: "planfeatures3",
                table: "pricingPlans",
                newName: "planfeaturesT");

            migrationBuilder.RenameColumn(
                name: "planfeatures2",
                table: "pricingPlans",
                newName: "planfeaturesS");

            migrationBuilder.RenameColumn(
                name: "planfeatures1",
                table: "pricingPlans",
                newName: "planfeaturesR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "planfeaturesZ",
                table: "pricingPlans",
                newName: "planfeatures5");

            migrationBuilder.RenameColumn(
                name: "planfeaturesU",
                table: "pricingPlans",
                newName: "planfeatures4");

            migrationBuilder.RenameColumn(
                name: "planfeaturesT",
                table: "pricingPlans",
                newName: "planfeatures3");

            migrationBuilder.RenameColumn(
                name: "planfeaturesS",
                table: "pricingPlans",
                newName: "planfeatures2");

            migrationBuilder.RenameColumn(
                name: "planfeaturesR",
                table: "pricingPlans",
                newName: "planfeatures1");
        }
    }
}
