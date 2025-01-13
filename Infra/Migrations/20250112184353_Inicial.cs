using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NUMERO_CONTA = table.Column<long>(type: "bigint", nullable: false),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIPO_PESSOA = table.Column<int>(type: "int", nullable: false),
                    IDADE = table.Column<int>(type: "int", nullable: false),
                    GENERO = table.Column<int>(type: "int", nullable: false),
                    ENDERECO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUMERO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COMPLEMENTO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIDADE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAIS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CODIGO_POSTAL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BAIRRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTE");
        }
    }
}
