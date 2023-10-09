using Application.Commands;
using Application.Commands.Usuario;
using Application.DTO;
using Application.Reads.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UserCRUD_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IMediator _handle;


        public UsuariosController(IMediator handle) : base()
        {
            _handle = handle;
        }

 
        // GET: api/Usuarios/5
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var result = await _handle.Send(new ObterTodosUsersQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var result = await _handle.Send(new ObterUserPorIdQuery(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostUsuario(CadastrarUsuarioCommand cadastrarUsuarioCommand)
        {
            var result = await _handle.Send(cadastrarUsuarioCommand);

            return Ok(result.Data);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete]
        public async Task<ActionResult> DeleteUsuario([FromForm] DeletarUsuarioCommand deletarUsuarioCommand)
        {
           await _handle.Send(deletarUsuarioCommand);
            return StatusCode(410, new { message = "User deleted succesfully!" });
        }

    }
}
