using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IProdutoRepository
    {
        Task Create(Produto produto);
        Task<Produto> Get(Guid id);
        Task Update(Produto produto);
        Task Delete(Produto produto);
    }
}
