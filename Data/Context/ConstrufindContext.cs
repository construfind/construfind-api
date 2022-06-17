using Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConstruFindAPI.Data.Context
{
    public class ConstrufindContext : IdentityDbContext<Usuario, UsuarioRole, string>
    {
        public ConstrufindContext(DbContextOptions<ConstrufindContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");

            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<Servico>(entity =>
            {
                entity.ToTable("Servicos");
            });

            builder.Entity<Servico>()
                   .HasOne<Usuario>(s => s.UsuarioContratante)
                   .WithMany(g => g.Servicos)
                   .HasForeignKey(s => s.UsuarioContratanteForeignID);
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Servico> Servicos { get; set; }
    }
}