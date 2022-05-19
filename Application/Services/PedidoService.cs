using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infra;
using Infra.Interfaces;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.Commands.VisualizarPedido;

namespace Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly Context _context;

        public PedidoService(Context context, IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<VisualizarPedido>> GetAll()
        {
            var pedidosParaVisualizacao = new List<VisualizarPedido>();
            var pedidos = await _context.Pedidos.Include(x => x.Itens).ToListAsync();

            foreach (var pedido in pedidos)
                CriarPedidosParaVisualizacao(pedidosParaVisualizacao, pedido);

            return pedidosParaVisualizacao;
        }

        private static void CriarPedidosParaVisualizacao(List<VisualizarPedido> pedidosParaVisualizacao, Pedido pedido)
        {
            pedidosParaVisualizacao.Add(EntityToVO(pedido));
        }

        public async Task<VisualizarPedido> Get(Guid id)
        {
           return EntityToVO(await _pedidoRepository.Get(id));
        }
        public async Task AdicionarItem(Guid id, ItemPedido itemCommand)
        {
            var produto = await _produtoRepository.Get(itemCommand.ProdutoId);
            var pedido = await _pedidoRepository.Get(id);

            pedido.AdicionarItem(produto, itemCommand.Quantidade);
            pedido.Validar();

            await _pedidoRepository.UpdateAsync(pedido);
            await _context.SaveChangesAsync();
        }
        public async Task Add(CadastrarPedido pedidoCommand)
        {
            var pedido = new Pedido(pedidoCommand.IdUsuario);

            await ValidarUsuario(pedidoCommand.IdUsuario);
            await _pedidoRepository.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItem(Guid id, Guid idItem)
        {
            var pedido = await _pedidoRepository.Get(id);

            pedido.RemoverItem(idItem);

            await _pedidoRepository.UpdateAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            await _pedidoRepository.DeleteAsync(pedido);
            await _context.SaveChangesAsync();
        }
        private static VisualizarPedido EntityToVO(Pedido pedido)
        {
            var pedidoParaVisualizacao = new VisualizarPedido(pedido.Id, pedido.ValorTotal, pedido.Status);
            foreach (var item in pedido.Itens)
                pedidoParaVisualizacao.AdicionarItem(item.Id, item.Descricao, item.Quantidade);
            return pedidoParaVisualizacao;
        }
        public async Task ValidarUsuario(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);

            if (usuario == null)
                throw new InvalidOperationException("Usuário inválido");
        }
    }
}
