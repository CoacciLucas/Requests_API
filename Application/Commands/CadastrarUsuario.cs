using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class CadastrarUsuario
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
    }
}
