using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
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
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private readonly DynamoDBContext _dynamoDBContext;

        public UsuarioRepository(Context context, IAmazonDynamoDB dynamoDBClient, DynamoDBContext dynamoDBContext) : base(context)
        {
            _dynamoDBClient = dynamoDBClient;
            _dynamoDBContext = dynamoDBContext;
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
