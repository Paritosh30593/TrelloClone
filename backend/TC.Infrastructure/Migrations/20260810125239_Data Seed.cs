using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string dataProjectPath = Path.GetFullPath(
                "../../../../TC.Infrastructure",
                AppContext.BaseDirectory);

            string sqlBoardPath = Path.GetFullPath("Scripts/InitialBoardSeed.sql", dataProjectPath);
            migrationBuilder.Sql(File.ReadAllText(sqlBoardPath));

            string sqlColumnPath = Path.GetFullPath("Scripts/InitialColumnSeed.sql", dataProjectPath);
            migrationBuilder.Sql(File.ReadAllText(sqlColumnPath));

            string sqlCardPath = Path.GetFullPath("Scripts/InitialCardSeed.sql", dataProjectPath);
            migrationBuilder.Sql(File.ReadAllText(sqlCardPath));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Card");
            migrationBuilder.Sql("DELETE FROM Column");
            migrationBuilder.Sql("DELETE FROM Board");
        }
    }
}
