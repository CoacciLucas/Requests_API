using Application.Interfaces;
using System;

namespace Application.Commands
{
    public class CadastrarUsuarioCommand : ICommand
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
