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
namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly Context _context;

        public UsuarioService(Context context, IUsuarioRepository usuarioRepository)
        {
            _context = context;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<List<VisualizarUsuario>> GetAll()
        {
            var usuariosParaVisualizacao = new List<VisualizarUsuario>();
            var usuarios = await _context.Usuarios.ToListAsync();

            foreach (var usuario in usuarios)
                usuariosParaVisualizacao.Add(EntityToVO(usuario));

            return usuariosParaVisualizacao;
        }
        public async Task<VisualizarUsuario> Get(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);

            return usuario == null ? null : EntityToVO(usuario);
                
        }
        public async Task Add(CadastrarUsuario usuarioCommand)
        {
            var usuario = new Usuario(usuarioCommand.Nome, usuarioCommand.Email, usuarioCommand.Cpf, usuarioCommand.DataNascimento);
            await _usuarioRepository.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Guid id, AtualizarUsuario usuarioCommand)
        {
            if (!UsuarioExists(id))
                throw new ArgumentNullException("Usuário não foi encontrado");

            var usuario = await _usuarioRepository.Get(id);

            usuario.DefinirNome(usuarioCommand.Nome);
            usuario.DefinirCpf(usuarioCommand.Cpf);
            usuario.DefinirDataNascimento(usuarioCommand.DataNascimento);
            usuario.DefinirAtivo(usuarioCommand.Ativo);
            usuario.Validar();
            
            await _usuarioRepository.UpdateAsync(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);

            await _usuarioRepository.DeleteAsync(usuario);
            await _context.SaveChangesAsync();
        }
        private static VisualizarUsuario EntityToVO(Usuario usuario)
        {
            return new VisualizarUsuario(usuario.Id, usuario.Nome, usuario.Email, usuario.Cpf, usuario.Ativo, usuario.DataNascimento);
        }
        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }


    }
}
