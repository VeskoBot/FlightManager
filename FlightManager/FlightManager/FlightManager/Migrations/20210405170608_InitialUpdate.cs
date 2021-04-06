using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Migrations
{
    public partial class InitialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UsersSet",
                type: "nvarchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UsersSet",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "UsersSet",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar");

            migrationBuilder.AlterColumn<string>(
                name: "AirplaneCode",
                table: "FlightsSet",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UsersSet",
                type: "nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UsersSet",
                type: "nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "UsersSet",
                type: "nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "AirplaneCode",
                table: "FlightsSet",
                type: "nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");
        }
    }
}
