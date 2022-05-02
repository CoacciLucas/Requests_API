using Application.Commands.ProdutoCmd;
using Domain.Entities;
using Infra;
using Infra.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly Context _context;

        public ProdutoService(Context context)
        {
            _context = context;
            _produtoRepository = new ProdutoRepository(_context);
        }
        public async Task PostProduto(CadastrarProduto produtoCommand)
        {
            var produto = new Produto(produtoCommand.Descricao, produtoCommand.Valor, produtoCommand.QuantidadeNoEstoque);

            await _produtoRepository.CreateProdutoDB(produto);
        }
        public async Task<VisualizarProduto> GetProduto(Guid id)
        {
            var produto = await _produtoRepository.GetProdutoDb(id);
            var viewProduto = new VisualizarProduto(produto.Descricao, produto.Valor, produto.QuantidadeNoEstoque);
            
            return viewProduto;
        }
        public async Task PutProduto(Guid id, AtualizarProduto produtoCommand)
        {
            if (!ProdutoExists(id))
                throw new ArgumentNullException("Produto não foi encontrado");

            var produto = await _produtoRepository.GetProdutoDb(id);
            produto.DefinirDescricao(produtoCommand.Descricao);
            produto.DefinirValor(produtoCommand.Valor);
            produto.DefinirAtivo(produtoCommand.Ativo);
            produto.DefinirQuantidadeNoEstoque(produtoCommand.QuantidadeNoEstoque);

            await _produtoRepository.UpdateProdutoDb(produto);
        }
        public async Task DeleteProduto(Guid id)
        {
            var produto = await _produtoRepository.GetProdutoDb(id);

            await _produtoRepository.DeleteProdutoDb(produto);
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
