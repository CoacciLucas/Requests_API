using Domain.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    { 

        public PedidoRepository(Context context) : base(context)
        {
        }

        public async Task<Pedido> Get(Guid id)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);
            if (pedido == null)
                throw new InvalidOperationException("Pedido não encontrado!");
            return pedido;
        }
        public async Task<IEnumerable<Pedido>> GetAll()
        {
            return await _context.Pedidos.Include(x => x.Itens).ToListAsync();
        }

        public bool PedidoExists(Guid id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
