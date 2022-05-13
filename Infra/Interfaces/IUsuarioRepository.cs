using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IUsuarioRepository
    {
        Task Create(Usuario usuario);
        Task<Usuario> Get(Guid id);
        Task Update(Usuario usuario);
        Task Delete(Usuario usuario);
    }
}
