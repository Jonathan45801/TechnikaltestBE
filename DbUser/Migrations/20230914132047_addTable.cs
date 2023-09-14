using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbUser.Migrations
{
    /// <inheritdoc />
    public partial class addTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id_user = table.Column<decimal>(type: "numeric", nullable: false),
                    nama_user = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    no_hp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    noktp = table.Column<string>(type: "text", nullable: true),
                    tgl_input = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id_user);
                });

            migrationBuilder.CreateTable(
                name: "ms_Vouchers",
                columns: table => new
                {
                    id_voucher = table.Column<decimal>(type: "numeric", nullable: false),
                    id_user = table.Column<decimal>(type: "numeric", nullable: true),
                    kodegenerate = table.Column<string>(type: "character varying(88)", maxLength: 88, nullable: true),
                    tgl_generate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.id_voucher);
                    table.ForeignKey(
                        name: "FK_ms_Vouchers_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateTable(
                name: "Userlogins",
                columns: table => new
                {
                    id_userlogin = table.Column<decimal>(type: "numeric", nullable: false),
                    user_id = table.Column<decimal>(type: "numeric", nullable: true),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userlogin", x => x.id_userlogin);
                    table.ForeignKey(
                        name: "FK_Userlogins_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ms_Vouchers_id_user",
                table: "ms_Vouchers",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Userlogins_user_id",
                table: "Userlogins",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ms_Vouchers");

            migrationBuilder.DropTable(
                name: "Userlogins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
