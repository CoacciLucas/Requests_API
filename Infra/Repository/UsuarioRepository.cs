using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class UsuarioRepository
    {
        protected readonly Context _context;

        public UsuarioRepository(Context context)
        {
            _context = context;
        }
        public async Task Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> Get(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuario não encontrado!");

            return usuario;
        }

        public async Task Update(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

    }
}
