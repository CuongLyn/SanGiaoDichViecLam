﻿// <auto-generated />
using System;
using BTL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250312072241_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BTL.Models.CongViec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaDiem")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("dia_diem");

                    b.Property<int>("IdNhaTuyenDung")
                        .HasColumnType("int")
                        .HasColumnName("id_nha_tuyen_dung");

                    b.Property<string>("LoaiHinh")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("loai_hinh");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("mo_ta");

                    b.Property<string>("MucLuong")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("muc_luong");

                    b.Property<DateTime>("NgayDang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ngay_dang");

                    b.Property<int?>("NhaTuyenDungId")
                        .HasColumnType("int");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("tieu_de");

                    b.HasKey("Id");

                    b.HasIndex("IdNhaTuyenDung");

                    b.HasIndex("NhaTuyenDungId");

                    b.ToTable("tbl_cong_viec");
                });

            modelBuilder.Entity("BTL.Models.NguoiDung", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ho_ten");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("mat_khau");

                    b.Property<DateTime>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ngay_tao");

                    b.Property<string>("SoDienThoai")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("so_dien_thoai");

                    b.HasKey("Id");

                    b.ToTable("tbl_nguoi_dung");
                });

            modelBuilder.Entity("BTL.Models.NhaTuyenDung", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("dia_chi");

                    b.Property<string>("Logo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("logo");

                    b.Property<string>("MoTaCongTy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("mo_ta_cong_ty");

                    b.Property<int>("NguoiDungId")
                        .HasColumnType("int")
                        .HasColumnName("nguoi_dung_id");

                    b.Property<string>("TenCongTy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ten_cong_ty");

                    b.Property<string>("Website")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("website");

                    b.HasKey("Id");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("tbl_cong_ty");
                });

            modelBuilder.Entity("BTL.Models.CongViec", b =>
                {
                    b.HasOne("BTL.Models.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("IdNhaTuyenDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BTL.Models.NhaTuyenDung", null)
                        .WithMany("CongViecs")
                        .HasForeignKey("NhaTuyenDungId");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("BTL.Models.NhaTuyenDung", b =>
                {
                    b.HasOne("BTL.Models.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("BTL.Models.NhaTuyenDung", b =>
                {
                    b.Navigation("CongViecs");
                });
#pragma warning restore 612, 618
        }
    }
}
