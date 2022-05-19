using Domain.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(Context context) : base(context)
        {

        }
        public async Task<Usuario> Get(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
