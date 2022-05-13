using System;
namespace Application.Commands
{
    public class AtualizarUsuario
    {
        public AtualizarUsuario(string nome, string cpf, DateTime dataNascimento, bool ativo)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Ativo = ativo;
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}
