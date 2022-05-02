using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infra;
using Application.Commands.PedidoCmd;
using Application.Services;
using Application.Commands.ProdutoCmd;

namespace UserCRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InserirItemPedidoController : ControllerBase
    {
        private readonly Context _context;

        public InserirItemPedidoController(Context context)
        {
            _context = context;
        }

        // POST: api/InserirItemPedido
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostItemPedido(InserirItemPedido pedidoCommand)
        {

            try
            {
                PedidoService service = new PedidoService(_context);
                await service.PostItemPedido(pedidoCommand);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/InserirItemPedido/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> DeletePedido(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }

        private bool PedidoExists(Guid id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
