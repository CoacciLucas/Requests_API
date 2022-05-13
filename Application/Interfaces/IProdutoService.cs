using Application.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProdutoService
    {
        Task<List<VisualizarProduto>> GetAll();
        Task Add(CadastrarProduto produto);
        Task<VisualizarProduto> Get(Guid id);
        Task Update(Guid id, AtualizarProduto produto);
        Task Delete(Guid id);
    }
}
