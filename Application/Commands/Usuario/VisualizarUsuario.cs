using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class VisualizarUsuario
    {
        public VisualizarUsuario(Guid id, string nome, string email, string cpf, bool ativo, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Ativo = ativo;
            DataNascimento = dataNascimento;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
