using Application.Commands;
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
    public class UsuariosController : ControllerBase
    {
        private readonly Context _context;
        private readonly UsuarioService _usuarioService;

        public UsuariosController(Context context)
        {
            _context = context;
            _usuarioService = new UsuarioService(_context);
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisualizarUsuario>>> GetAll()
        {

            return await _usuarioService.GetAll();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<VisualizarUsuario> GetUsuario(Guid id)
        {
            return await _usuarioService.Get(id);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, AtualizarUsuario usuarioCommand)
        {
            try
            {
                await _usuarioService.Update(id, usuarioCommand);
                return Created("", null);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
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
                await _usuarioService.Add(usuario);
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
                await _usuarioService.Delete(id);
                return Created("", null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
