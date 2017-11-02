using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MathProject.Migrations
{
    public partial class changedanswerdecimaltodouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CorrectAnswer",
                table: "Question",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CorrectAnswer",
                table: "Question",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
