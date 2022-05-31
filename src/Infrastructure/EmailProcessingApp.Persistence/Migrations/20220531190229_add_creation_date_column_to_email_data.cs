using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailProcessingApp.Persistence.Migrations
{
    public partial class add_creation_date_column_to_email_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "EmailData",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "EmailData");
        }
    }
}
