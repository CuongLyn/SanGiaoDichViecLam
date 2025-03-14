using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_nha_tuyen_dung_tbl_nguoi_dung_IdNguoiDung",
                table: "tbl_nha_tuyen_dung");

            migrationBuilder.DropTable(
                name: "tbl_ung_vien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_nha_tuyen_dung",
                table: "tbl_nha_tuyen_dung");

            migrationBuilder.RenameTable(
                name: "tbl_nha_tuyen_dung",
                newName: "tbl_cong_ty");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "tbl_nguoi_dung",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbl_nguoi_dung",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SoDienThoai",
                table: "tbl_nguoi_dung",
                newName: "so_dien_thoai");

            migrationBuilder.RenameColumn(
                name: "NgayTao",
                table: "tbl_nguoi_dung",
                newName: "ngay_tao");

            migrationBuilder.RenameColumn(
                name: "MatKhau",
                table: "tbl_nguoi_dung",
                newName: "mat_khau");

            migrationBuilder.RenameColumn(
                name: "HoTen",
                table: "tbl_nguoi_dung",
                newName: "ho_ten");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "tbl_cong_ty",
                newName: "website");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "tbl_cong_ty",
                newName: "logo");

            migrationBuilder.RenameColumn(
                name: "TenCongTy",
                table: "tbl_cong_ty",
                newName: "ten_cong_ty");

            migrationBuilder.RenameColumn(
                name: "MoTaCongTy",
                table: "tbl_cong_ty",
                newName: "mo_ta_cong_ty");

            migrationBuilder.RenameColumn(
                name: "IdNguoiDung",
                table: "tbl_cong_ty",
                newName: "nguoi_dung_id");

            migrationBuilder.AlterColumn<string>(
                name: "website",
                table: "tbl_cong_ty",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "logo",
                table: "tbl_cong_ty",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "mo_ta_cong_ty",
                table: "tbl_cong_ty",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "tbl_cong_ty",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "dia_chi",
                table: "tbl_cong_ty",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_cong_ty",
                table: "tbl_cong_ty",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cong_ty_nguoi_dung_id",
                table: "tbl_cong_ty",
                column: "nguoi_dung_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_cong_ty_tbl_nguoi_dung_nguoi_dung_id",
                table: "tbl_cong_ty",
                column: "nguoi_dung_id",
                principalTable: "tbl_nguoi_dung",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_cong_ty_tbl_nguoi_dung_nguoi_dung_id",
                table: "tbl_cong_ty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_cong_ty",
                table: "tbl_cong_ty");

            migrationBuilder.DropIndex(
                name: "IX_tbl_cong_ty_nguoi_dung_id",
                table: "tbl_cong_ty");

            migrationBuilder.DropColumn(
                name: "id",
                table: "tbl_cong_ty");

            migrationBuilder.DropColumn(
                name: "dia_chi",
                table: "tbl_cong_ty");

            migrationBuilder.RenameTable(
                name: "tbl_cong_ty",
                newName: "tbl_nha_tuyen_dung");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "tbl_nguoi_dung",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tbl_nguoi_dung",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "so_dien_thoai",
                table: "tbl_nguoi_dung",
                newName: "SoDienThoai");

            migrationBuilder.RenameColumn(
                name: "ngay_tao",
                table: "tbl_nguoi_dung",
                newName: "NgayTao");

            migrationBuilder.RenameColumn(
                name: "mat_khau",
                table: "tbl_nguoi_dung",
                newName: "MatKhau");

            migrationBuilder.RenameColumn(
                name: "ho_ten",
                table: "tbl_nguoi_dung",
                newName: "HoTen");

            migrationBuilder.RenameColumn(
                name: "website",
                table: "tbl_nha_tuyen_dung",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "logo",
                table: "tbl_nha_tuyen_dung",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "ten_cong_ty",
                table: "tbl_nha_tuyen_dung",
                newName: "TenCongTy");

            migrationBuilder.RenameColumn(
                name: "mo_ta_cong_ty",
                table: "tbl_nha_tuyen_dung",
                newName: "MoTaCongTy");

            migrationBuilder.RenameColumn(
                name: "nguoi_dung_id",
                table: "tbl_nha_tuyen_dung",
                newName: "IdNguoiDung");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "tbl_nha_tuyen_dung",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "tbl_nha_tuyen_dung",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MoTaCongTy",
                table: "tbl_nha_tuyen_dung",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_nha_tuyen_dung",
                table: "tbl_nha_tuyen_dung",
                column: "IdNguoiDung");

            migrationBuilder.CreateTable(
                name: "tbl_ung_vien",
                columns: table => new
                {
                    IdNguoiDung = table.Column<int>(type: "int", nullable: false),
                    CV = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GioiThieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KinhNghiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KyNang = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_nha_tuyen_dung_tbl_nguoi_dung_IdNguoiDung",
                table: "tbl_nha_tuyen_dung",
                column: "IdNguoiDung",
                principalTable: "tbl_nguoi_dung",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
