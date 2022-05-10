using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task<Pedido> Get(Guid id)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);
            if (pedido == null)
                throw new InvalidOperationException("Pedido não encontrado!");
            return pedido;
        }
        public async Task<Item> GetItem(Guid id)
        {
            var item = await _context.Itens.FindAsync(id);
            if (item == null)
                throw new InvalidOperationException("Pedido não encontrado!");
            return item;
        }
        public async Task<IEnumerable<Pedido>> GetAll()
        {
            return await _context.Pedidos.Include(x => x.Itens).ToListAsync();
        }

        public async Task Update(Pedido pedido)
        {
            var item = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == pedido.Id);
            _context.Entry(item).State = EntityState.Modified;
            await Task.FromResult(_context.Set<Pedido>().Update(item));
        }

        public async Task Delete(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Deleted;
            await Task.FromResult(_context.Set<Pedido>().Remove(pedido));
        }
        public bool PedidoExists(Guid id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
