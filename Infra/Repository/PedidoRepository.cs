using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class PedidoRepository
    {
        protected readonly Context _context;

        public PedidoRepository(Context context)
        {
            _context = context;
        }
        public async Task CreatePedidoDB(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();
        }

        public async Task<Pedido> GetPedidoDb(Guid id)
        {
            var Pedido = await _context.Pedidos.FindAsync(id);
            if (Pedido == null)
                throw new InvalidOperationException("Pedido não encontrado!");
            return Pedido;
        }

        public async Task UpdatePedidoDb(Pedido Pedido)
        {
            _context.Entry(Pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task InserirItemPedidoDb(Produto item, Pedido pedido)
        {
            pedido.Itens.Add(item);
            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePedidoDb(Pedido Pedido)
        {
            _context.Pedidos.Remove(Pedido);
            await _context.SaveChangesAsync();
        }
    }
}
