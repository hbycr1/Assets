using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Jobs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CustomerId",
                table: "Jobs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CompanyId",
                table: "Customer",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Customer_CustomerId",
                table: "Jobs",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Customer_CustomerId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CustomerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Jobs");
        }
    }
}
