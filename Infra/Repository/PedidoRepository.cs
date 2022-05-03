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
        public async Task Add(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();
        }

        public async Task<Pedido> Get(Guid id)
        {
            var Pedido = await _context.Pedidos.Include(x=>x.Itens).FirstOrDefaultAsync(x => x.Id==id);
            if (Pedido == null)
                throw new InvalidOperationException("Pedido não encontrado!");
            return Pedido;
        }

        public async Task Update(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Pedido Pedido)
        {
            _context.Pedidos.Remove(Pedido);
            await _context.SaveChangesAsync();
        }
    }
}
