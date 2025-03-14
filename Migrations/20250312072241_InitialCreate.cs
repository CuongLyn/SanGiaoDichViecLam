using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_cong_viec",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nha_tuyen_dung = table.Column<int>(type: "int", nullable: false),
                    tieu_de = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    dia_diem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    muc_luong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    loai_hinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ngay_dang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NhaTuyenDungId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_cong_viec", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_cong_viec_tbl_cong_ty_NhaTuyenDungId",
                        column: x => x.NhaTuyenDungId,
                        principalTable: "tbl_cong_ty",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_cong_viec_tbl_nguoi_dung_id_nha_tuyen_dung",
                        column: x => x.id_nha_tuyen_dung,
                        principalTable: "tbl_nguoi_dung",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cong_viec_id_nha_tuyen_dung",
                table: "tbl_cong_viec",
                column: "id_nha_tuyen_dung");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cong_viec_NhaTuyenDungId",
                table: "tbl_cong_viec",
                column: "NhaTuyenDungId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_cong_viec");
        }
    }
}
