using Application.Commands;
using Application.DTO;
using Application.Reads.Queries;
using AutoMapper;
using Domain.Entities;
using Infra.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handler
{
    public class ObterTodosUsersQueryHandler : IRequestHandler<ObterTodosUsersQuery, List<UserDTO>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public ObterTodosUsersQueryHandler(IMediator mediator, IUsuarioRepository repository, IMapper mapper)
        {
            _usuarioRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> Handle(ObterTodosUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _usuarioRepository.GetAllAsync();

            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}
