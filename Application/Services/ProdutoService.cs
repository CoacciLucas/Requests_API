using Application.Commands;
using Domain;
using Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
    public class ProdutoService
    {
        private readonly Context _context;

        public ProdutoService(Context context)
        {
            _context = context;
        }
        public async Task PostProduto(CadastrarProduto produtoCommand)
        {
            var produto = new Produto(produtoCommand.Descricao, produtoCommand.Valor, produtoCommand.Ativo, produtoCommand.QuantidadeNoEstoque);

            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

        }
        public async Task<VisualizarProduto> GetProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            var viewProduto = new VisualizarProduto(produto.Nome, produto.Email, produto.Cpf, produto.DataNascimento);
            if (produto == null)
            {
                throw new InvalidOperationException("Usuário não encontrado!");
            }
            return viewProduto;
        }
        public async Task PutProduto(Guid id, AtualizarProduto produtoCommand)
        {
            if (!ProdutoExists(id))
                throw new ArgumentNullException("Usuário não foi encontrado");

            var produto = await _context.Produtos.FindAsync(id);
            produto.DefinirNome(produtoCommand.Nome);
            produto.DefinirCpf(produtoCommand.Cpf);
            produto.DefinirDataNascimento(produtoCommand.DataNascimento);

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
