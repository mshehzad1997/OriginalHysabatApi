using Microsoft.EntityFrameworkCore.Migrations;

namespace HYSABATApi.Migrations
{
    public partial class contactInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanFeaturesPricingPlan");

            migrationBuilder.DropTable(
                name: "planFeatures");

            migrationBuilder.AlterColumn<string>(
                name: "Postion",
                table: "contactInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumberOfSupliers",
                table: "contactInformation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NumberOfCustomer",
                table: "contactInformation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Postion",
                table: "contactInformation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfSupliers",
                table: "contactInformation",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfCustomer",
                table: "contactInformation",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "planFeatures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanFeaturesPricingPlan",
                columns: table => new
                {
                    FeaturesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pricingPlansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanFeaturesPricingPlan", x => new { x.FeaturesId, x.pricingPlansId });
                    table.ForeignKey(
                        name: "FK_PlanFeaturesPricingPlan_planFeatures_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "planFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanFeaturesPricingPlan_pricingPlans_pricingPlansId",
                        column: x => x.pricingPlansId,
                        principalTable: "pricingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanFeaturesPricingPlan_pricingPlansId",
                table: "PlanFeaturesPricingPlan",
                column: "pricingPlansId");
        }
    }
}
