using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IPedidoRepository
    {
        Task Add(Pedido pedido);
        Task<Pedido> Get(Guid id);
        Task<Item> GetItem(Guid id);
        Task<IEnumerable<Pedido>> GetAll();
        Task Update(Pedido pedido);
        Task Delete(Pedido pedido);
    }
}
