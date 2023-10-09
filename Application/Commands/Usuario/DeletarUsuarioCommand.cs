using Application.Interfaces;
using System;

namespace Application.Commands.Usuario
{
    public class DeletarUsuarioCommand : ICommand
    {
        public string Id { get; set; }
    }
}
