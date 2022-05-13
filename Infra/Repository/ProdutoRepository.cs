using Domain.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        protected readonly Context _context;

        public ProdutoRepository(Context context)
        {
            _context = context;
        }
        public async Task Create(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
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
            await Task.FromResult(_context.Set<Produto>().Update(produto));
        }

        public async Task Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
            _context.Entry(produto).State = EntityState.Deleted;
            await Task.FromResult(_context.Set<Produto>().Remove(produto));
        }
    }
}
