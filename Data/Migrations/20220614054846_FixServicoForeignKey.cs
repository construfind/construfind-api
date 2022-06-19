using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FixServicoForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_User_UsuarioCPF",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "UsuarioCPF",
                schema: "Identity",
                table: "Servicos",
                newName: "UsuarioForeignID");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_UsuarioCPF",
                schema: "Identity",
                table: "Servicos",
                newName: "IX_Servicos_UsuarioForeignID");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_User_UsuarioForeignID",
                schema: "Identity",
                table: "Servicos",
                column: "UsuarioForeignID",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_User_UsuarioForeignID",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "UsuarioForeignID",
                schema: "Identity",
                table: "Servicos",
                newName: "UsuarioCPF");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_UsuarioForeignID",
                schema: "Identity",
                table: "Servicos",
                newName: "IX_Servicos_UsuarioCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_User_UsuarioCPF",
                schema: "Identity",
                table: "Servicos",
                column: "UsuarioCPF",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
