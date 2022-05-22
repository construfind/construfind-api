using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RemoveEnderecoStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Bairros_bairroEnderecocodigoBairro",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.DropTable(
                name: "Bairros",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Cidades",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Estados",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_bairroEnderecocodigoBairro",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "bairroEnderecocodigoBairro",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.AddColumn<string>(
                name: "Sigla",
                schema: "Identity",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nomeBairro",
                schema: "Identity",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nomeCidade",
                schema: "Identity",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nomeEstado",
                schema: "Identity",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sigla",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "nomeBairro",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "nomeCidade",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "nomeEstado",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.AddColumn<Guid>(
                name: "bairroEnderecocodigoBairro",
                schema: "Identity",
                table: "Enderecos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Estados",
                schema: "Identity",
                columns: table => new
                {
                    codigoEstado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nomeEstado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.codigoEstado);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                schema: "Identity",
                columns: table => new
                {
                    codigoCidade = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estadoCidadecodigoEstado = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nomeCidade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.codigoCidade);
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_estadoCidadecodigoEstado",
                        column: x => x.estadoCidadecodigoEstado,
                        principalSchema: "Identity",
                        principalTable: "Estados",
                        principalColumn: "codigoEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bairros",
                schema: "Identity",
                columns: table => new
                {
                    codigoBairro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cidadeBairrocodigoCidade = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nomeBairro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bairros", x => x.codigoBairro);
                    table.ForeignKey(
                        name: "FK_Bairros_Cidades_cidadeBairrocodigoCidade",
                        column: x => x.cidadeBairrocodigoCidade,
                        principalSchema: "Identity",
                        principalTable: "Cidades",
                        principalColumn: "codigoCidade",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_bairroEnderecocodigoBairro",
                schema: "Identity",
                table: "Enderecos",
                column: "bairroEnderecocodigoBairro");

            migrationBuilder.CreateIndex(
                name: "IX_Bairros_cidadeBairrocodigoCidade",
                schema: "Identity",
                table: "Bairros",
                column: "cidadeBairrocodigoCidade");

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_estadoCidadecodigoEstado",
                schema: "Identity",
                table: "Cidades",
                column: "estadoCidadecodigoEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Bairros_bairroEnderecocodigoBairro",
                schema: "Identity",
                table: "Enderecos",
                column: "bairroEnderecocodigoBairro",
                principalSchema: "Identity",
                principalTable: "Bairros",
                principalColumn: "codigoBairro",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
