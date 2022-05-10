using Application.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPedidoService
    {
        Task<List<VisualizarPedido>> GetAll();
        Task Add(CadastrarPedido pedido);
        Task Add(Guid id, ItemPedido item);
        Task DeleteItem(Guid id, Guid idItem);
        Task<VisualizarPedido> Get(Guid id);
        Task Delete(Guid id);
    }
}
