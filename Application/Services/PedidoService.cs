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
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Context _context;

        public PedidoService(Context context)
        {
            _context = context;
            _pedidoRepository = new PedidoRepository(_context);
            _produtoRepository = new ProdutoRepository(_context);
            _usuarioRepository = new UsuarioRepository(_context);
        }
        public async Task Post(CadastrarPedido pedidoCommand)
        {

            var pedido = new Pedido(pedidoCommand.IdUsuario);
            await ValidarUsuario(pedidoCommand.IdUsuario);
            await _pedidoRepository.Add(pedido);
        }
        public async Task Post(Guid id, ItemPedido pedidoCommand)
        {

            var produto = await _produtoRepository.Get(pedidoCommand.ProdutoId);
            var pedido = await _pedidoRepository.Get(id);

            pedido.Add(produto, pedidoCommand.Quantidade);
            await _context.SaveChangesAsync();
            await _pedidoRepository.Update(pedido);
        }
        public async Task DeleteItemPedido(Guid id, Guid idItem)
        {

            var pedido = await _pedidoRepository.Get(id);
            var itemPedido = await _pedidoRepository.GetItem(idItem);

            pedido.Delete(itemPedido);

            await _context.SaveChangesAsync();
            await _pedidoRepository.Update(pedido);
        }
        public async Task<Pedido> GetPedido(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            return pedido;
        }
        public async Task DeletePedido(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            await _pedidoRepository.Delete(pedido);
        }

        public async Task ValidarUsuario(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);
            if (usuario == null)
                throw new ArgumentNullException("Usuário inválido");
        }
        public void ValidarPedido(Pedido pedido)
        {
            ValidarStatus(pedido.Status);
            ValidarValorTotal(pedido.ValorTotal);
        }
        public void ValidarStatus(Status status)
        {
            bool success = Enum.IsDefined(typeof(Status), status);
            if (!success)
            {
                throw new ArgumentNullException("Status invalido!");
            }
        }

        public void ValidarValorTotal(decimal valorTotal)
        {
            if (valorTotal < 0)
                throw new ArgumentNullException("Valor total deve ser maior que ou igual a 0");
        }
        private bool PedidoExists(Guid id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
