using Amazon.DynamoDBv2.DataModel;
using System;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    [DynamoDBTable("User")]
    public class User
    {
        public User(string nome, string email, string cpf, DateTime datanascimento)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Email = email;
            Cpf = cpf;
            DataNascimento = datanascimento;
            DataCadastro = DateTime.Now;
            Ativo = true;
            Validar();
        }
        public User() { }

        [DynamoDBHashKey("UserId")]
        public string Id { get; private set; }
        [DynamoDBProperty("Name")]
        public string Nome { get; private set; }
        [DynamoDBProperty("Email")]
        public string Email { get; private set; }
        [DynamoDBProperty("CPF")]
        public string Cpf { get; private set; }
        [DynamoDBProperty("Date_Birth")]
        public DateTime DataNascimento { get; private set; }
        [DynamoDBProperty("Date_Register")]
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

        public void Validar()
        {
            ValidarCpf(Cpf);
            ValidarEmail(Email);
            ValidarNome(Nome);
            ValidarAtivo(Ativo);
        }
        private void ValidarEmail(string email)
        {

            int indexArr = email.IndexOf('@');
            if (!(indexArr > 0))
                throw new InvalidOperationException("Email Inválido");

            int indexDot = email.IndexOf('.', indexArr);
            if (!(indexDot - 1 > indexArr))
                throw new InvalidOperationException("Email Inválido");

            if (!(indexDot + 1 < email.Length))
                throw new InvalidOperationException("Email Inválido");

            string indexDot2 = email.Substring(indexDot + 1, 1);
            if (!(indexDot2 != "."))
                throw new InvalidOperationException("Email Inválido");
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome) || nome.Length > 60)
                throw new InvalidOperationException("Nome Inválido");

        }

        private void ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                throw new InvalidOperationException("CPF Inválido");
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            digito = Regex.Replace(digito, "[^0-9]", "");
        }

        private void ValidarAtivo(bool ativo)
        {
            if (ativo != true && ativo != false)
                throw new InvalidOperationException("O ativo deve ser somente true ou false!");

        }
    }

}
