using Business.Utils;
using ConstruFindAPI.Business.Enums;
using ConstruFindAPI.Business.Models;
using ConstruFindAPI.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seed
{
    public class AdminUserSeed
    {
        private ConstrufindContext _context;

        public AdminUserSeed(ConstrufindContext context)
        {
            _context = context;
        }

        public async void SeedAdminUser()
        {
            var user = new Usuario
            {
                NormalizedUserName = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                PhoneNumber = "123456789",
                Documento = "12345678912",
                DataCriacao = DateTime.Now,
                DataUltimoAcesso = DateTime.Now,
                Endereco = new Endereco
                {
                    numeroEndereco = "144",
                    nomeLogradouro = "Praça 19 de Janeiro",
                    codigoCEP = "11346400",
                    bairroEndereco = new Bairro
                    {
                        nomeBairro = "Boqueirão",
                        cidadeBairro = new Cidade
                        {
                            nomeCidade = "Praia Grande",
                            estadoCidade = new Estado
                            {
                                nomeEstado = "São Paulo",
                                Sigla = EnderecoUtils.GetSiglaEstado("São Paulo")
                            }
                        }
                    }
                },
                TipoUsuario = TipoUsuario.Contratante
            };

            var roleStore = new RoleStore<UsuarioRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "admin"))
            {
                await roleStore.CreateAsync(new UsuarioRole { Name = "admin", NormalizedName = "admin" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<Usuario>();
                var hashed = password.HashPassword(user, "admin");
                user.PasswordHash = hashed;
                var userStore = new UserStore<Usuario>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "admin");
            }

            await _context.SaveChangesAsync();
        }
    }
}
