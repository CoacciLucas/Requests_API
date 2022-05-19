using System;
using System.Text.RegularExpressions;

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
            Validar();
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

        public void Validar()
        {
            ValidarCpf(Cpf);
            ValidarEmail(Email);
            ValidarNome(Nome);
            ValidarAtivo(Ativo);
            ValidarDataNascimento(DataNascimento);
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



            /* if (indexArr > 0)
             {
                 int indexDot = email.IndexOf('.', indexArr);
                 if (indexDot - 1 > indexArr)
                 {
                     if (indexDot + 1 < email.Length)
                     {
                         string indexDot2 = email.Substring(indexDot + 1, 1);
                         if (indexDot2 != ".")
                         {

                         }
                         else
                         {
                             throw new InvalidOperationException("Email Inválido");
                         }

                     }
                     else
                     {
                         throw new InvalidOperationException("Email Inválido");
                     }
                 }
                 else
                 {
                     throw new InvalidOperationException("Email Inválido");
                 }
             }
             else
                 throw new InvalidOperationException("Email Inválido");
            */
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

        public void ValidarDataNascimento(DateTime date)
        {
            if (!BeAValidDate(date) && !NotBeAFutureDate(date))
                throw new InvalidOperationException("Data de nascimento inválida!");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }

        private bool NotBeAFutureDate(DateTime date)
        {
            return !(date.Date > DateTime.Now.Date);
        }
    }

}
