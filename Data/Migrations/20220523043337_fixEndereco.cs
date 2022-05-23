using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "Identity");

            migrationBuilder.AddColumn<string>(
                name: "CodigoCEP",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeBairro",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeCidade",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeEstado",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeLogradouro",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroEndereco",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiglaEstado",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoCEP",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NomeBairro",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NomeCidade",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NomeEstado",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NomeLogradouro",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NumeroEndereco",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SiglaEstado",
                schema: "Identity",
                table: "User");

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "Identity",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeBairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeCidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeLogradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroEndereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiglaEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioRef = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.IdEndereco);
                    table.ForeignKey(
                        name: "FK_Endereco_User_UsuarioRef",
                        column: x => x.UsuarioRef,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_UsuarioRef",
                schema: "Identity",
                table: "Endereco",
                column: "UsuarioRef",
                unique: true,
                filter: "[UsuarioRef] IS NOT NULL");
        }
    }
}
