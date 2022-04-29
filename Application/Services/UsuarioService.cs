using Application.Commands;
using Domain;
using Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Context _context;

        public UsuarioService(Context context)
        {
            _context = context;
            _usuarioRepository = new UsuarioRepository(_context);
        }
        public async Task PostUsuario(CadastrarUsuario usuarioCommand)
        {

            var usuario = new Usuario(usuarioCommand.Nome, usuarioCommand.Email, usuarioCommand.Cpf, usuarioCommand.DataNascimento);

            await _usuarioRepository.CreateUsuarioDB(usuario);

        }
        public async Task<VisualizarUsuario> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            var viewUsuario = new VisualizarUsuario(usuario.Nome, usuario.Email, usuario.Cpf, usuario.DataNascimento);
            if (usuario == null)
            {
                throw new InvalidOperationException("Usuário não encontrado!");
            }
            return viewUsuario;
        }
        public async Task PutUsuario(Guid id, AtualizarUsuario usuarioCommand)
        {
            if (!UsuarioExists(id))
                throw new ArgumentNullException("Usuário não foi encontrado");

            var usuario = await _context.Usuarios.FindAsync(id);
            usuario.DefinirNome(usuarioCommand.Nome);
            usuario.DefinirCpf(usuarioCommand.Cpf);
            usuario.DefinirDataNascimento(usuarioCommand.DataNascimento);

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
