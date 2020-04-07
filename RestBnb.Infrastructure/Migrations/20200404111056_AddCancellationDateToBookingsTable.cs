using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestBnb.Infrastructure.Migrations
{
    public partial class AddCancellationDateToBookingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                table: "Bookings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationDate",
                table: "Bookings");
        }
    }
}
