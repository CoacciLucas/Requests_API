using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ProdutoRepository
    {
        protected readonly Context _context;

        public ProdutoRepository(Context context)
        {
            _context = context;
        }
        public async Task Create(Produto produto)
        {
            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<Produto> Get(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                throw new InvalidOperationException("Produto não encontrado!");
            return produto;
        }

        public async Task Update(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}
