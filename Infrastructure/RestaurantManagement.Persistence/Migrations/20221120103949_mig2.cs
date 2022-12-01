using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "TopMenus");

            migrationBuilder.RenameColumn(
                name: "ActionName",
                table: "SubMenus",
                newName: "Link");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "SubMenus",
                newName: "ActionName");

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "TopMenus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
