﻿// <auto-generated />
using System;
using Base.Server.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Base.Server.EFCore.Migrations
{
    [DbContext(typeof(EFCoreContext))]
    partial class EFCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Base.Server.Models.AutoColor", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("color_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ColorId"));

                    b.Property<string>("ColorName")
                        .HasColumnType("longtext")
                        .HasColumnName("color_name");

                    b.HasKey("ColorId");

                    b.ToTable("base_license_color");
                });

            modelBuilder.Entity("Base.Server.Models.AutoRegister", b =>
                {
                    b.Property<int>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("auto_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AutoId"));

                    b.Property<int>("AutoColorId")
                        .HasColumnType("int")
                        .HasColumnName("auto_color_id");

                    b.Property<string>("AutoLicense")
                        .HasColumnType("longtext")
                        .HasColumnName("auto_license");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<int>("FeeModeId")
                        .HasColumnType("int")
                        .HasColumnName("fee_mode_id");

                    b.Property<int>("LicenseColorId")
                        .HasColumnType("int")
                        .HasColumnName("license_color_id");

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("state");

                    b.Property<int>("ValidCount")
                        .HasColumnType("int")
                        .HasColumnName("valid_count");

                    b.Property<DateTime>("ValidEndTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("valid_end_time");

                    b.Property<DateTime>("ValidStartTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("valid_start_time");

                    b.HasKey("AutoId");

                    b.ToTable("auto_register");
                });

            modelBuilder.Entity("Base.Server.Models.FeeMode", b =>
                {
                    b.Property<int>("FeeModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("fee_model_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FeeModelId"));

                    b.Property<string>("FeeModelName")
                        .HasColumnType("longtext")
                        .HasColumnName("fee_model_name");

                    b.HasKey("FeeModelId");

                    b.ToTable("base_fee_model");
                });

            modelBuilder.Entity("Base.Server.Models.LicenseColor", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("color_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ColorId"));

                    b.Property<string>("ColorName")
                        .HasColumnType("longtext")
                        .HasColumnName("color_name");

                    b.HasKey("ColorId");

                    b.ToTable("base_auto_color");
                });

            modelBuilder.Entity("Base.Server.Models.MenuModel", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("menu_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MenuId"));

                    b.Property<int>("Index")
                        .HasColumnType("int")
                        .HasColumnName("index");

                    b.Property<string>("MenuHeader")
                        .HasColumnType("longtext")
                        .HasColumnName("menu_header");

                    b.Property<string>("MenuIcon")
                        .HasColumnType("longtext")
                        .HasColumnName("menu_icon");

                    b.Property<int>("MenuType")
                        .HasColumnType("int")
                        .HasColumnName("menu_type");

                    b.Property<int>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("state");

                    b.Property<string>("TargetView")
                        .HasColumnType("longtext")
                        .HasColumnName("target_view");

                    b.HasKey("MenuId");

                    b.ToTable("menus");
                });

            modelBuilder.Entity("Base.Server.Models.RecordInfo", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("record_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RecordId"));

                    b.Property<string>("AutoLicense")
                        .HasColumnType("longtext")
                        .HasColumnName("auto_license");

                    b.Property<double>("Discount")
                        .HasColumnType("double")
                        .HasColumnName("discount");

                    b.Property<DateTime>("EnterTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("enter_time");

                    b.Property<int>("FeeModelId")
                        .HasColumnType("int")
                        .HasColumnName("fee_mode_id");

                    b.Property<DateTime>("LeaveTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("leave_time");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint")
                        .HasColumnName("order_id");

                    b.Property<int>("PayType")
                        .HasColumnType("int")
                        .HasColumnName("pay_type");

                    b.Property<double>("Payable")
                        .HasColumnType("double")
                        .HasColumnName("payable");

                    b.Property<double>("Payment")
                        .HasColumnType("double")
                        .HasColumnName("payment");

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("state");

                    b.HasKey("RecordId");

                    b.ToTable("record_info");
                });

            modelBuilder.Entity("Base.Server.Models.RoleInfo", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext")
                        .HasColumnName("role_name");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.HasKey("RoleId");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Base.Server.Models.RoleMenu", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("MenuId")
                        .HasColumnType("int")
                        .HasColumnName("menu_id");

                    b.HasKey("RoleId", "MenuId");

                    b.ToTable("role_menu");
                });

            modelBuilder.Entity("Base.Server.Models.UpgradeFileModel", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("file_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FileId"));

                    b.Property<string>("FileMd5")
                        .HasColumnType("longtext")
                        .HasColumnName("file_md5");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext")
                        .HasColumnName("file_name");

                    b.Property<string>("FilePath")
                        .HasColumnType("longtext")
                        .HasColumnName("file_path");

                    b.Property<int>("Length")
                        .HasColumnType("int")
                        .HasColumnName("length");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("upload_time");

                    b.Property<int>("state")
                        .HasColumnType("int")
                        .HasColumnName("state");

                    b.HasKey("FileId");

                    b.ToTable("upgrade_file");
                });

            modelBuilder.Entity("Base.Server.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("age");

                    b.Property<string>("Password")
                        .HasColumnType("longtext")
                        .HasColumnName("password");

                    b.Property<string>("RealName")
                        .HasColumnType("longtext")
                        .HasColumnName("real_name");

                    b.Property<string>("UserIcon")
                        .HasColumnType("longtext")
                        .HasColumnName("user_icon");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext")
                        .HasColumnName("user_name");

                    b.Property<int>("ViewType")
                        .HasColumnType("int")
                        .HasColumnName("view_type");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("system_users");
                });

            modelBuilder.Entity("Base.Server.Models.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("RoleId", "UserId");

                    b.ToTable("user_role");
                });
#pragma warning restore 612, 618
        }
    }
}