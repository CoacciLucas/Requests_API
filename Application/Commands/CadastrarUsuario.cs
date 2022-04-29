using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class CadastrarUsuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
