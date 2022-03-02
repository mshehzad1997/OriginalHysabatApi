using Microsoft.EntityFrameworkCore.Migrations;

namespace HYSABATApi.Migrations
{
    public partial class initialdbss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanFeature");

            migrationBuilder.AddColumn<string>(
                name: "PricingPlanFeatures",
                table: "pricingPlans",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricingPlanFeatures",
                table: "pricingPlans");

            migrationBuilder.CreateTable(
                name: "PlanFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricingPlanFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricingPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanFeature_pricingPlans_PricingPlanId",
                        column: x => x.PricingPlanId,
                        principalTable: "pricingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanFeature_PricingPlanId",
                table: "PlanFeature",
                column: "PricingPlanId");
        }
    }
}
