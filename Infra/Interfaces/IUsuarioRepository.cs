using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IUsuarioRepository : IRepository<User>
    {
        Task DeleteByIdAsync(string id);
        Task<User> Get(string id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task SaveAsync(User item);
    }
}
