using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Razor_VS_Code_test.Data.Migrations
{
    public partial class CreatePageDataSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageDataItems",
                columns: table => new
                {
                    PageDataId = table.Column<string>(nullable: false),
                    MetaDescription = table.Column<string>(nullable: false),
                    MetaKeywords = table.Column<string>(nullable: false),
                    PageName = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageDataItems", x => x.PageDataId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageDataItems");
        }
    }
}
