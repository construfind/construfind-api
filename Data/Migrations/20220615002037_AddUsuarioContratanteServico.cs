using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddUsuarioContratanteServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_User_UsuarioForeignID",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "UsuarioForeignID",
                schema: "Identity",
                table: "Servicos",
                newName: "UsuarioPrestadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_UsuarioForeignID",
                schema: "Identity",
                table: "Servicos",
                newName: "IX_Servicos_UsuarioPrestadorId");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioContratanteForeignID",
                schema: "Identity",
                table: "Servicos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioPrestadorCPF",
                schema: "Identity",
                table: "Servicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_UsuarioContratanteForeignID",
                schema: "Identity",
                table: "Servicos",
                column: "UsuarioContratanteForeignID");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_User_UsuarioContratanteForeignID",
                schema: "Identity",
                table: "Servicos",
                column: "UsuarioContratanteForeignID",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_User_UsuarioPrestadorId",
                schema: "Identity",
                table: "Servicos",
                column: "UsuarioPrestadorId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_User_UsuarioContratanteForeignID",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_User_UsuarioPrestadorId",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_UsuarioContratanteForeignID",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "UsuarioContratanteForeignID",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "UsuarioPrestadorCPF",
                schema: "Identity",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "UsuarioPrestadorId",
                schema: "Identity",
                table: "Servicos",
                newName: "UsuarioForeignID");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_UsuarioPrestadorId",
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
    }
}
