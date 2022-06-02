using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailProcessingApp.Persistence.Migrations
{
    public partial class change_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResponseEmail",
                table: "ResponseEmail");

            migrationBuilder.RenameTable(
                name: "ResponseEmail",
                newName: "SendEmails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SendEmails",
                table: "SendEmails",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SendEmails",
                table: "SendEmails");

            migrationBuilder.RenameTable(
                name: "SendEmails",
                newName: "ResponseEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResponseEmail",
                table: "ResponseEmail",
                column: "Key");
        }
    }
}
