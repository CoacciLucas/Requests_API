using Application.Commands;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProdutoService
    {
        Task Add(CadastrarProduto produto);
        Task<VisualizarProduto> Get(Guid id);
        Task Update(Guid id, AtualizarProduto produto);
        Task Delete(Guid id);
        void ValidarProduto(Produto produto);
    }
}
