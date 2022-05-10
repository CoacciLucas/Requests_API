using Application.Commands.ProdutoCmd;
using Application.Services;
using Domain.Entities;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserCRUD_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly Context _context;

        public ProdutoController(Context context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<VisualizarProduto> GetProduto(Guid id)
        {
            ProdutoService service = new ProdutoService(_context);
            return await service.Get(id);
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(Guid id, AtualizarProduto usuarioCommand)
        {
            try
            {
                ProdutoService service = new ProdutoService(_context);
                await service.Update(id, usuarioCommand);
                return Created("", null);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Produtos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostProduto(CadastrarProduto usuario)
        {
            try
            {
                ProdutoService service = new ProdutoService(_context);
                await service.Add(usuario);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(Guid id)
        {
            try
            {
                ProdutoService service = new ProdutoService(_context);
                await service.Delete(id);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
