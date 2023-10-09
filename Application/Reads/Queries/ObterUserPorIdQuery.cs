using Application.DTO;
using MediatR;

namespace Application.Reads.Queries
{
    public class ObterUserPorIdQuery : IRequest<UserDTO>
    {
        public string Id { get; set; }

        public ObterUserPorIdQuery(string id)
        {
            Id = id;
        }
    }
}
