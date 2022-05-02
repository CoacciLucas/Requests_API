using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.UsuarioCmd
{
    public class VisualizarUsuario
    {
        public VisualizarUsuario(string nome, string email, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
