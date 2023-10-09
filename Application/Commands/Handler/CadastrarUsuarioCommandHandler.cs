using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Infra.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handler
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public CadastrarUsuarioCommandHandler(IMediator mediator, IUsuarioRepository repository, IMapper mapper)
        {
            _usuarioRepository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = _mapper.Map<User>(request);

            await _usuarioRepository.SaveAsync(usuario);

            return new CommandResult(new { usuario.Id });
        }
    }
}
