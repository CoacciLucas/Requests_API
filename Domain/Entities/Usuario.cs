using System;
using Domain.Services;

namespace Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public DateTime DtNasc { get; set; }
        public DateTime DtCadastro { get; } = DateTime.UtcNow;
        public bool Ativo { get; set; }

        public bool IsValid()
        {
            return ValidaCPF.IsCpf(Cpf);
        }
    }

}
