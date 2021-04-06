using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightsSet",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureLoaction = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    ArrivalLocation = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "date", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "date", nullable: false),
                    AirplaneType = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    AirplaneCode = table.Column<string>(type: "nvarchar", nullable: false),
                    VacantSpots = table.Column<int>(type: "int", nullable: false),
                    VacantSpotsBussiness = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightsSet", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "UsersSet",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar", nullable: false),
                    Email = table.Column<string>(type: "nvarchar", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    EGN = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSet", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ReservationsSet",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Surename = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    EGN = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    TicketType = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationsSet", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_ReservationsSet_FlightsSet_FlightId",
                        column: x => x.FlightId,
                        principalTable: "FlightsSet",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationsSet_FlightId",
                table: "ReservationsSet",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationsSet");

            migrationBuilder.DropTable(
                name: "UsersSet");

            migrationBuilder.DropTable(
                name: "FlightsSet");
        }
    }
}
