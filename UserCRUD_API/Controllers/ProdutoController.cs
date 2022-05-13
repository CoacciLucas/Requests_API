using Application.Commands;
using Application.Interfaces;
using Application.Services;
using Infra;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserCRUD_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisualizarProduto>>> GetAll()
        {
            return await _produtoService.GetAll();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<VisualizarProduto> GetProduto(Guid id)
        {
            return await _produtoService.Get(id);
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(Guid id, AtualizarProduto usuarioCommand)
        {
            try
            {
                await _produtoService.Update(id, usuarioCommand);
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
                await _produtoService.Add(usuario);
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
                await _produtoService.Delete(id);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
