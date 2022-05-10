﻿using Application.Commands;
using Application.Services;
using Domain.Entities;
using Infra;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserCRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly Context _context;
        private readonly PedidoRepository _pedidoRepository;

        public PedidosController(Context context)
        {
            _context = context;
            _pedidoRepository = new PedidoRepository(_context);
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<IEnumerable<Pedido>> GetAll()
        {
            return await _pedidoRepository.GetAll();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(Guid id)
        {
            var pedido = await _pedidoRepository.Get(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]

        public async Task<IActionResult> PutPedido(Guid id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_pedidoRepository.PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pedidos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostPedido(CadastrarPedido pedido)
        {

            try
            {
                PedidoService service = new PedidoService(_context);
                await service.Add(pedido);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> Delete(Guid id)
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
        [HttpPost("{id}/itens")]
        public async Task<ActionResult> PostItemPedido(Guid id, ItemPedido pedidoCommand)
        {
            try
            {
                PedidoService service = new PedidoService(_context);
                await service.Add(id, pedidoCommand);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}/itens")]
        public async Task<ActionResult> DeleteItem(Guid id, Guid idItem)
        {
            try
            {
                PedidoService service = new PedidoService(_context);
                await service.DeleteItem(id, idItem);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
