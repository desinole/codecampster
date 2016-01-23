using System;
using System.Collections.Generic;
using codecampster.Models;
using Microsoft.Data.Entity.Migrations;

namespace codecampster.Migrations
{
    public partial class ProfileFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.AddColumn<string>("FirstName", "AspNetUsers", nullable: false);
			migrationBuilder.AddColumn<string>("LastName", "AspNetUsers", nullable: false);
			migrationBuilder.AddColumn<string>("Location", "AspNetUsers", nullable: true);
			migrationBuilder.AddColumn<string>("Twitter", "AspNetUsers", nullable: true);
			migrationBuilder.AddColumn<int?>("AvatarID", "AspNetUsers", nullable: true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
