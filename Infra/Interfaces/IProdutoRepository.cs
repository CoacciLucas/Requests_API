using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> Get(Guid id);
        Task<IEnumerable<Produto>> GetAll();
    }
}
