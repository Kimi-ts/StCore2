using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Razor_VS_Code_test.Data.Migrations
{
    public partial class AddSiteConfigSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteConfig",
                columns: table => new
                {
                    SiteConfigId = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    ClientEmails = table.Column<string>(nullable: false),
                    ClientPhones = table.Column<string>(nullable: false),
                    CloseHours = table.Column<string>(nullable: false),
                    Copyright = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OfficialInfo = table.Column<string>(nullable: false),
                    OfficialLicence = table.Column<string>(nullable: false),
                    OpenHours = table.Column<string>(nullable: false),
                    PartnerEmails = table.Column<string>(nullable: false),
                    PartnerPhones = table.Column<string>(nullable: false),
                    PostCode = table.Column<string>(nullable: false),
                    Street = table.Column<string>(nullable: false),
                    WorkingDays = table.Column<string>(nullable: false),
                    WorkingDaysString = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteConfig", x => x.SiteConfigId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteConfig");
        }
    }
}
