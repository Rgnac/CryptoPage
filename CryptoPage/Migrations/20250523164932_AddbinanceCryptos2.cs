using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoPage.Migrations
{
    /// <inheritdoc />
    public partial class AddbinanceCryptos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "binanceCryptos",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BaseAsset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteAsset = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_binanceCryptos", x => x.Symbol);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "binanceCryptos");
        }
    }
}
