using Domain.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(Context context) : base(context)
        {

        }

        public async Task<Produto> Get(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                throw new InvalidOperationException("Produto não encontrado!");
            return produto;
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        }
    }
}
