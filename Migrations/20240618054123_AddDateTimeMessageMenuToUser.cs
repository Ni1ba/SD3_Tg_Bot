using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SD3_Tg_Bot.Migrations
{
    public partial class AddDateTimeMessageMenuToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Добавление новой колонки
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeMessageMenu",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow); // или другое значение по умолчанию
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаление колонки при откате миграции
            migrationBuilder.DropColumn(
                name: "DateTimeMessageMenu",
                table: "Users");
        }
    }
}
