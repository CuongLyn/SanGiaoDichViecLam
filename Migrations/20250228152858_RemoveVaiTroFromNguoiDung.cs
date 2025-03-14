using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVaiTroFromNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_nguoi_dung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_nguoi_dung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_nha_tuyen_dung",
                columns: table => new
                {
                    IdNguoiDung = table.Column<int>(type: "int", nullable: false),
                    TenCongTy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MoTaCongTy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_nha_tuyen_dung", x => x.IdNguoiDung);
                    table.ForeignKey(
                        name: "FK_tbl_nha_tuyen_dung_tbl_nguoi_dung_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "tbl_nguoi_dung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ung_vien",
                columns: table => new
                {
                    IdNguoiDung = table.Column<int>(type: "int", nullable: false),
                    GioiThieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KyNang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KinhNghiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CV = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ung_vien", x => x.IdNguoiDung);
                    table.ForeignKey(
                        name: "FK_tbl_ung_vien_tbl_nguoi_dung_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "tbl_nguoi_dung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_nha_tuyen_dung");

            migrationBuilder.DropTable(
                name: "tbl_ung_vien");

            migrationBuilder.DropTable(
                name: "tbl_nguoi_dung");
        }
    }
}
