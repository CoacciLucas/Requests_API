using Application.Commands.UsuarioCmd;
using Domain.Entities;
using Infra;
using Infra.Repository;
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
            var usuario = await _usuarioRepository.GetUsuarioDb(id);
            var viewUsuario = new VisualizarUsuario(usuario.Nome, usuario.Email, usuario.Cpf, usuario.DataNascimento);

            return viewUsuario;
        }
        public async Task PutUsuario(Guid id, AtualizarUsuario usuarioCommand)
        {
            if (!UsuarioExists(id))
                throw new ArgumentNullException("Usuário não foi encontrado");

            var usuario = await _usuarioRepository.GetUsuarioDb(id);
            usuario.DefinirNome(usuarioCommand.Nome);
            usuario.DefinirCpf(usuarioCommand.Cpf);
            usuario.DefinirDataNascimento(usuarioCommand.DataNascimento);
            usuario.DefinirAtivo(usuarioCommand.Ativo);

            await _usuarioRepository.UpdateUsuarioDb(usuario);
        }
        public async Task DeleteUsuario(Guid id)
        {
            var usuario = await _usuarioRepository.GetUsuarioDb(id);

            await _usuarioRepository.DeleteUsuarioDb(usuario);
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
