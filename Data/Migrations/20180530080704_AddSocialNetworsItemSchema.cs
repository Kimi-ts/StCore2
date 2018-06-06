using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Razor_VS_Code_test.Data.Migrations
{
    public partial class AddSocialNetworsItemSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialNetworkItems",
                columns: table => new
                {
                    SocialNetworkItemId = table.Column<string>(nullable: false),
                    IconClass = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworkItems", x => x.SocialNetworkItemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialNetworkItems");
        }
    }
}
