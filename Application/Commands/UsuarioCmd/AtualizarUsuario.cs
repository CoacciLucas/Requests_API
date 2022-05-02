using System;
namespace Application.Commands.UsuarioCmd
{
    public class AtualizarUsuario
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
