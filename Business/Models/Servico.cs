using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Servico
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string Local { get; set; }

        public string TipoServico { get; set; }

        public string Descricao { get; set; }

        public string UsuarioContratanteForeignID { get; set; }

        public string UsuarioPrestadorCPF { get; set; }

        public Usuario UsuarioContratante { get; set; }
    }
}
