using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt_zaliczeniowy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class customTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cities__3213E83F62CBBC7D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__regions__3213E83F5995FCC2", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "restaurants_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    region = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__restaura__3213E83F87BAC261", x => x.id);
                    table.ForeignKey(
                        name: "FK__restauran__regio__3C69FB99",
                        column: x => x.region,
                        principalTable: "regions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    type = table.Column<int>(type: "int", nullable: true),
                    city = table.Column<int>(type: "int", nullable: true),
                    adult_only = table.Column<bool>(type: "bit", nullable: false),
                    rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__restaura__3213E83FCEB872CE", x => x.id);
                    table.ForeignKey(
                        name: "FK__restaurant__city__403A8C7D",
                        column: x => x.city,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__restaurant__type__3F466844",
                        column: x => x.type,
                        principalTable: "restaurants_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    restaurant = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comments__3213E83F9686E331", x => x.id);
                    table.ForeignKey(
                        name: "FK__comments__restau__4316F928",
                        column: x => x.restaurant,
                        principalTable: "restaurants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    restaurant = table.Column<int>(type: "int", nullable: true),
                    guest = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reservat__3213E83F42A20B71", x => x.id);
                    table.ForeignKey(
                        name: "FK__reservati__guest__46E78A0C",
                        column: x => x.guest,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__reservati__resta__45F365D3",
                        column: x => x.restaurant,
                        principalTable: "restaurants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_comments_restaurant",
                table: "comments",
                column: "restaurant");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_guest",
                table: "reservations",
                column: "guest");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_restaurant",
                table: "reservations",
                column: "restaurant");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_city",
                table: "restaurants",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_type",
                table: "restaurants",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_types_region",
                table: "restaurants_types",
                column: "region");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "restaurants_types");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
        }
    }
}
