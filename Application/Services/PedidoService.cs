using Application.Commands.PedidoCmd;
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
                pedidoCommand.DescricaoPedido);

            await _pedidoRepository.Add(pedido);
        }
        public async Task PostItemPedido(Guid id, InserirItemPedido pedidoCommand)
        {

            var produto = await _produtoRepository.GetProdutoDb(pedidoCommand.ProdutoId);
            var pedido = await _pedidoRepository.Get(id);

            pedido.AdicionarItem(produto, pedidoCommand.Quantidade);
            await _context.SaveChangesAsync();
            await _pedidoRepository.Update(pedido);
        }
        public async Task<Pedido> GetPedido(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            return pedido;
        }
        /*public async Task AdicionarItem(Produto produto, Pedido pedido ,int quantidade)
        {
            var item = new Item(produto, pedido, quantidade);
            pedido.
            ValorTotal = ValorTotal + (produto.Valor * quantidade);
        }*/
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
            var pedido = await _pedidoRepository.Get(id);

            await _pedidoRepository.Delete(pedido);
        }

        private bool PedidoExists(Guid id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
