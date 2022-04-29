using Domain;
using System.Threading.Tasks;

namespace Infra
{
    public class UsuarioRepository
    {
        protected readonly Context _context;

        public UsuarioRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateUsuarioDB (Usuario usuario)
        {
            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task GetUsuarioDB(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }

    }
}
