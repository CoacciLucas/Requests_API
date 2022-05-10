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
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task<Usuario> Get(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task Update(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await Task.FromResult(_context.Set<Usuario>().Update(usuario));
        }

        public async Task Delete(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Deleted;
            await Task.FromResult(_context.Set<Usuario>().Remove(usuario));
        }

    }
}
