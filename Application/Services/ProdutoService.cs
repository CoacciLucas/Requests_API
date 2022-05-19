using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infra;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly Context _context;

        public ProdutoService(Context context, IProdutoRepository produtoRepository)
        {
            _context = context;
            _produtoRepository = produtoRepository;
        }
        public async Task<List<VisualizarProduto>> GetAll()
        {
            var produtosParaVisualizacao = new List<VisualizarProduto>();
            var produtos = await _context.Produtos.ToListAsync();

            foreach (var produto in produtos)
                produtosParaVisualizacao.Add(EntityToVO(produto));

            return produtosParaVisualizacao;
        }

        public async Task<VisualizarProduto> Get(Guid id)
        {
            return EntityToVO(await _produtoRepository.Get(id));
        }
        public async Task Add(CadastrarProduto produtoCommand)
        {
            var produto = new Produto(produtoCommand.Descricao, produtoCommand.Valor, produtoCommand.QuantidadeNoEstoque);

            await _produtoRepository.AddAsync(produto);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Guid id, AtualizarProduto produtoCommand)
        {
            if (!ProdutoExists(id))
                throw new ArgumentNullException("Produto não foi encontrado");

            var produto = await _produtoRepository.Get(id);

            produto.DefinirDescricao(produtoCommand.Descricao);
            produto.DefinirValor(produtoCommand.Valor);
            produto.DefinirAtivo(produtoCommand.Ativo);
            produto.DefinirQuantidadeNoEstoque(produtoCommand.QuantidadeNoEstoque);
            produto.Validar();

            await _produtoRepository.UpdateAsync(produto);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var produto = await _produtoRepository.Get(id);

            await _produtoRepository.DeleteAsync(produto);
            await _context.SaveChangesAsync();
        }
        private static VisualizarProduto EntityToVO(Produto produto)
        {
            return new VisualizarProduto(produto.Id, produto.Descricao, produto.Valor, produto.QuantidadeNoEstoque);
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }


    }
}
