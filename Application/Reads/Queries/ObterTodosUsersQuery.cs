using Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace Application.Reads.Queries
{
    public class ObterTodosUsersQuery : IRequest<List<UserDTO>>
    {
        public ObterTodosUsersQuery() { }
    }
}
