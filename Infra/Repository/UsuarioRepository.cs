using Amazon.DynamoDBv2.DataModel;
using Domain.Entities;
using Infra.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class UsuarioRepository : Repository<User>, IUsuarioRepository
    {
        private readonly DynamoDBContext _dynamoDBContext;

        public UsuarioRepository(Context context, DynamoDBContext dynamoDBContext) : base(context)
        {
            _dynamoDBContext = dynamoDBContext;
        }
        public async Task DeleteByIdAsync(string id)
        {
            await _dynamoDBContext.DeleteAsync(await GetByIdAsync(id));
        }
        public async Task SaveAsync(User item)
        {
            await _dynamoDBContext.SaveAsync(item);
        }
        public async Task<User> Get(string id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
        public async Task<List<User>> GetAllAsync()
        {
            var conditions = new List<ScanCondition>();
            var result = await _dynamoDBContext.ScanAsync<User>(conditions).GetRemainingAsync();

            return result;
        }
        public async Task<User> GetByIdAsync(string id)
        {
            return await _dynamoDBContext.LoadAsync<User>(id);
        }
    }
}
