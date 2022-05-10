using Application.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<VisualizarUsuario>> GetAll();
        Task Add(CadastrarUsuario usuarioCommand);
        Task<VisualizarUsuario> Get(Guid id);
        Task Update(Guid id, AtualizarUsuario usuarioCommand);
        Task Delete(Guid id);

    }
}
