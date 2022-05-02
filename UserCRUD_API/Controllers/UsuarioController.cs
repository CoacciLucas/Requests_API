using Application.Commands;
using Application.Commands.UsuarioCmd;
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
    public class UsuariosController : ControllerBase
    {
        private readonly Context _context;

        public UsuariosController(Context context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<VisualizarUsuario> GetUsuario(Guid id)
        {
            UsuarioService service = new UsuarioService(_context);
            return await service.GetUsuario(id);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, AtualizarUsuario usuarioCommand)
        {
            try
            {
                UsuarioService service = new UsuarioService(_context);
                await service.PutUsuario(id, usuarioCommand);
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

        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostUsuario(CadastrarUsuario usuario)
        {
            try
            {
                UsuarioService service = new UsuarioService(_context);
                await service.PostUsuario(usuario);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(Guid id)
        {
            try
            {
                UsuarioService service = new UsuarioService(_context);
                await service.DeleteUsuario(id);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
