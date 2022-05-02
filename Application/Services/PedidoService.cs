using Application.Commands.PedidoCmd;
using Domain.Entities;
using Infra;
using Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly Context _context;

        public PedidoService(Context context)
        {
            _context = context;
            _pedidoRepository = new PedidoRepository(_context);
            _produtoRepository = new ProdutoRepository(_context);
        }
        public async Task PostPedido(CadastrarPedido pedidoCommand)
        {

            var pedido = new Pedido(pedidoCommand.IdUsuario,
                pedidoCommand.Descricao,
                pedidoCommand.Status,
                pedidoCommand.ValorTotal);

            await _pedidoRepository.CreatePedidoDB(pedido);
        }
        public async Task PostItemPedido(InserirItemPedido pedidoCommand)
        {

            var produto = await _produtoRepository.GetProdutoDb(pedidoCommand.ProdutoId);
            var pedido = await _pedidoRepository.GetPedidoDb(pedidoCommand.PedidoId);

            await _pedidoRepository.InserirItemPedidoDb(produto, pedido);
        }
        public async Task<VisualizarPedido> GetPedido(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedidoDb(id);
            var viewPedido = new VisualizarPedido(pedido.IdUsuario, pedido.Descricao, pedido.Itens, pedido.Status, pedido.ValorTotal);

            return viewPedido;
        }
       /* public async Task PutPedido(Guid id, AtualizarPedido pedidoCommand)
        {
            if (!PedidoExists(id))
                throw new ArgumentNullException("Pedido não foi encontrado");

            var pedido = await _pedidoRepository.GetPedidoDb(id);
            pedido.DefinirDescricao(pedidoCommand.Descricao);
            pedido.DefinirValor(pedidoCommand.Valor);
            pedido.DefinirAtivo(pedidoCommand.Ativo);
            pedido.DefinirQuantidadeNoEstoque(pedidoCommand.QuantidadeNoEstoque);

            await _dpedidoRepository.UpdatePedidoDb(pedido);
        }*/
        public async Task DeletePedido(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedidoDb(id);

            await _pedidoRepository.DeletePedidoDb(pedido);
        }

        private bool PedidoExists(Guid id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
