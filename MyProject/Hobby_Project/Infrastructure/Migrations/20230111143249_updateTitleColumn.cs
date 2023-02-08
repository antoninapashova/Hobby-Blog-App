﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTitleColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "HobbyComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "HobbyComments");
        }
    }
}