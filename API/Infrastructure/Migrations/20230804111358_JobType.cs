using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "Jobs",
                newName: "JobTypeId");

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobType_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobType_CompanyId",
                table: "JobType",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobTypeId",
                table: "Jobs",
                newName: "JobType");
        }
    }
}
