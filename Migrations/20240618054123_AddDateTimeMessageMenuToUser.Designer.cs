﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SD3_Tg_Bot;

#nullable disable

namespace SD3_Tg_Bot.Migrations
{
    [DbContext(typeof(DB.ApplicationContext))]
    [Migration("20240618054123_AddDateTimeMessageMenuToUser")]
    partial class AddDateTimeMessageMenuToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SD3_Tg_Bot.DB+User", b =>
                {
                    b.Property<long?>("TgUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTimeMessageMenu")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("TgMenuMessageId")
                        .HasColumnType("bigint");

                    b.Property<string>("TgUserName")
                        .HasColumnType("text");

                    b.HasKey("TgUserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}