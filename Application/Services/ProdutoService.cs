using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly Context _context;

        public ProdutoService(Context context)
        {
            _context = context;
            _produtoRepository = new ProdutoRepository(_context);
        }
        public async Task<List<VisualizarProduto>> GetAll()
        {
            var produtosParaVisualizacao = new List<VisualizarProduto>();
            var produtos = await _context.Produtos.ToListAsync();

            foreach (var produto in produtos)
                produtosParaVisualizacao.Add(new VisualizarProduto(produto.Descricao, produto.Valor, produto.QuantidadeNoEstoque));

            return produtosParaVisualizacao;
        }
        public async Task Add(CadastrarProduto produtoCommand)
        {
            var produto = new Produto(produtoCommand.Descricao, produtoCommand.Valor, produtoCommand.QuantidadeNoEstoque);

            await _produtoRepository.Create(produto);
            await _context.SaveChangesAsync();
        }
        public async Task<VisualizarProduto> Get(Guid id)
        {
            var produto = await _produtoRepository.Get(id);
            var visualizarProduto = new VisualizarProduto(produto.Descricao, produto.Valor, produto.QuantidadeNoEstoque);

            return visualizarProduto;
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

            await _produtoRepository.Update(produto);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var produto = await _produtoRepository.Get(id);

            await _produtoRepository.Delete(produto);
            await _context.SaveChangesAsync();
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }

        public void ValidarProduto(Produto produto)
        {
            ValidarDescricao(produto.Descricao);
            ValidarValor(produto.Valor);
            ValidarQuantidadeNoEstoque(produto.QuantidadeNoEstoque);
            ValidarAtivo(produto.Ativo);
        }
        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao) || descricao.Length > 200)
            {
                throw new InvalidOperationException("Descricao invalida!");
            }
        }

        private void ValidarValor(decimal valor)
        {
            if (valor <= 0 || valor.Equals(null))
            {
                throw new InvalidOperationException("Valor invalido!");
            }
        }

        private void ValidarAtivo(bool ativo)
        {
            if (ativo != true && ativo != false)
                throw new InvalidOperationException("O ativo deve ser apenas true or false!");
        }

        private void ValidarQuantidadeNoEstoque(int quantidadeNoEstoque)
        {
            if (!(quantidadeNoEstoque % 1 == 0) || quantidadeNoEstoque < 0)
            {
                throw new InvalidOperationException("Quantidade no estoque invalida!");
            }
        }
    }
}
