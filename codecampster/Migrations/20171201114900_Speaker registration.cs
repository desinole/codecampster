using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace codecampster.Migrations
{
    public partial class Speakerregistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMvp",
                table: "Speakers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Speakers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Speakers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMvp",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Speakers");
        }
    }
}
