using Application.Commands.UsuarioCmd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task Add(CadastrarUsuario usuarioCommand);
        Task<VisualizarUsuario> Get(Guid id);
        Task Update(Guid id, AtualizarUsuario usuarioCommand);
        Task Delete(Guid id);

    }
}
