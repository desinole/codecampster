using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace codecampster.Migrations
{
    public partial class SpeakerDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorDetails",
                table: "Speakers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MVPDetails",
                table: "Speakers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteToOrganizers",
                table: "Speakers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorDetails",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "MVPDetails",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "NoteToOrganizers",
                table: "Speakers");
        }
    }
}
