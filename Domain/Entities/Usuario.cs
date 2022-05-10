using System;

namespace Domain.Entities
{
    public class Usuario
    {
        public Usuario(string nome, string email, string cpf, DateTime datanascimento)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Cpf = cpf;
            DataNascimento = datanascimento;
            DataCadastro = DateTime.Now;
            Ativo = true;
            /*Chamar o Validar()*/
        }
        protected Usuario() { }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        public void DefinirNome(string nome)
        {
            Nome = nome;
        }
        public void DefinirCpf(string cpf)
        {
            Cpf = cpf;
        }
        public void DefinirDataNascimento(DateTime dataNascimento)
        {
            DataNascimento = dataNascimento;
        }
        public void DefinirAtivo(bool ativo)
        {
            Ativo = ativo;
        }
    }

}
