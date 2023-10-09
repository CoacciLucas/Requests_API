using Abp.Domain.Entities;
using Application.DTO;
using Application.Reads.Queries;
using AutoMapper;
using Infra.Interfaces;
using MediatR;
using System.Data.Entity.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handler
{
    public class ObterUserPorIdQueryHandler : IRequestHandler<ObterUserPorIdQuery, UserDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public ObterUserPorIdQueryHandler(IMediator mediator, IUsuarioRepository repository, IMapper mapper)
        {
            _usuarioRepository = repository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(ObterUserPorIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _usuarioRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new ObjectNotFoundException("User not found");
            }
            return _mapper.Map<UserDTO>(user);
        }
    }
}
