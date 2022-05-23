using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FixEnderecos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Enderecos_EnderecocodigoEndereco",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                schema: "Identity",
                table: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                schema: "Identity",
                newName: "Endereco",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                schema: "Identity",
                table: "Endereco",
                column: "codigoEndereco");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Endereco_EnderecocodigoEndereco",
                schema: "Identity",
                table: "User",
                column: "EnderecocodigoEndereco",
                principalSchema: "Identity",
                principalTable: "Endereco",
                principalColumn: "codigoEndereco",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Endereco_EnderecocodigoEndereco",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                schema: "Identity",
                table: "Endereco");

            migrationBuilder.RenameTable(
                name: "Endereco",
                schema: "Identity",
                newName: "Enderecos",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                schema: "Identity",
                table: "Enderecos",
                column: "codigoEndereco");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Enderecos_EnderecocodigoEndereco",
                schema: "Identity",
                table: "User",
                column: "EnderecocodigoEndereco",
                principalSchema: "Identity",
                principalTable: "Enderecos",
                principalColumn: "codigoEndereco",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
