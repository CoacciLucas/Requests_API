using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly PedidoService _pedidoService;
        private readonly ProdutoRepository _produtoRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Context _context;

        public PedidoService(Context context)
        {
            _context = context;
            _pedidoRepository = new PedidoRepository(_context);
            _pedidoService = new PedidoService(_context);
            _produtoRepository = new ProdutoRepository(_context);
            _usuarioRepository = new UsuarioRepository(_context);
        }

        public async Task<List<VisualizarPedido>> GetAll()
        {
            var pedidosParaVisualizacao = new List<VisualizarPedido>();
            var pedidos = await _context.Pedidos.ToListAsync();

            foreach (var pedido in pedidos)
                pedidosParaVisualizacao.Add(new VisualizarPedido(pedido.ValorTotal, pedido.Status));

            return pedidosParaVisualizacao;
        }
        public async Task Add(CadastrarPedido pedidoCommand)
        {
            var pedido = new Pedido(pedidoCommand.IdUsuario);

            await ValidarUsuario(pedidoCommand.IdUsuario);

            await _pedidoRepository.Add(pedido);
            await _context.SaveChangesAsync();
        }
        public async Task Add(Guid id, ItemPedido itemCommand)
        {
            var produto = await _produtoRepository.Get(itemCommand.ProdutoId);
            var pedido = await _pedidoRepository.Get(id);

            pedido.AdicionarItem(produto, itemCommand.Quantidade);

            await _pedidoRepository.Update(pedido);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItem(Guid id, Guid idItem)
        {
            var pedido = await _pedidoRepository.Get(id);
            var itemPedido = await _pedidoRepository.GetItem(idItem);

            pedido.RemoverItem(itemPedido);

            await _pedidoRepository.Update(pedido);
            await _context.SaveChangesAsync();
        }
        public async Task<VisualizarPedido> Get(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            return new VisualizarPedido(pedido.ValorTotal, pedido.Status);
        }
        public async Task Delete(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            await _pedidoRepository.Delete(pedido);
            await _context.SaveChangesAsync();
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
    }
}
