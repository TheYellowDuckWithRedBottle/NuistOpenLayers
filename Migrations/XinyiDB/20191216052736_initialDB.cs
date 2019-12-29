using Microsoft.EntityFrameworkCore.Migrations;

namespace XinYiThree.Migrations.XinyiDB
{
    public partial class initialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdCardNo",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 18,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdCardNo",
                table: "AspNetUsers",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
