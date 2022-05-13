using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Context _context;

        public UsuarioService(Context context)
        {
            _context = context;
            _usuarioRepository = new UsuarioRepository(_context);
        }
        public async Task<List<VisualizarUsuario>> GetAll()
        {
            var usuariosParaVisualizacao = new List<VisualizarUsuario>();
            var usuarios = await _context.Usuarios.ToListAsync();

            foreach (var usuario in usuarios)
                usuariosParaVisualizacao.Add(new VisualizarUsuario(usuario.Id, usuario.Nome, usuario.Email, usuario.Cpf, usuario.Ativo, usuario.DataNascimento));

            return usuariosParaVisualizacao;
        }
        public async Task Add(CadastrarUsuario usuarioCommand)
        {
            var usuario = new Usuario(usuarioCommand.Nome, usuarioCommand.Email, usuarioCommand.Cpf, usuarioCommand.DataNascimento);
            await _usuarioRepository.Create(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task<VisualizarUsuario> Get(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);
            var viewUsuario = new VisualizarUsuario(usuario.Id, usuario.Nome, usuario.Email, usuario.Cpf, usuario.Ativo, usuario.DataNascimento);

            return viewUsuario;
        }

        public async Task Update(Guid id, AtualizarUsuario usuarioCommand)
        {
            if (!UsuarioExists(id))
                throw new ArgumentNullException("Usuário não foi encontrado");

            var usuario = await _usuarioRepository.Get(id);

            await _usuarioRepository.Update(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);

            await _usuarioRepository.Delete(usuario);
            await _context.SaveChangesAsync();
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }


    }
}
