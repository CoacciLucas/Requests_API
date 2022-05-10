using Application.Commands.ProdutoCmd;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProdutoService
    {
        Task Add(CadastrarProduto produtoCommand);
        Task<VisualizarProduto> Get(Guid id);
        Task Update(Guid id, AtualizarProduto produtoCommand);
        Task Delete(Guid id);
        void ValidarProduto(Produto produto);
    }
}
