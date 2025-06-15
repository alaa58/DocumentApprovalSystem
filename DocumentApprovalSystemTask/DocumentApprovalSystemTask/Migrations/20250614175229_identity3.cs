using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentApprovalSystemTask.Migrations
{
    /// <inheritdoc />
    public partial class identity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileApprovals_Employees_ResponsibleEmployeeId",
                table: "FileApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Employees_SubmittedById",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "SubmittedById",
                table: "Files",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ResponsibleEmployeeId",
                table: "FileApprovals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId1",
                table: "Employees",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_UserId1",
                table: "Employees",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FileApprovals_Employees_ResponsibleEmployeeId",
                table: "FileApprovals",
                column: "ResponsibleEmployeeId",
                principalTable: "Employees",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Employees_SubmittedById",
                table: "Files",
                column: "SubmittedById",
                principalTable: "Employees",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_UserId1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_FileApprovals_Employees_ResponsibleEmployeeId",
                table: "FileApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Employees_SubmittedById",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "SubmittedById",
                table: "Files",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleEmployeeId",
                table: "FileApprovals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileApprovals_Employees_ResponsibleEmployeeId",
                table: "FileApprovals",
                column: "ResponsibleEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Employees_SubmittedById",
                table: "Files",
                column: "SubmittedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
