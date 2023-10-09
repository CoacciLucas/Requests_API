using Application.Commands;
using Application.Commands.Usuario;
using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Infra.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handler
{
    public class DeletarUsuarioCommandHandler : IRequestHandler<DeletarUsuarioCommand, CommandResult>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public DeletarUsuarioCommandHandler(IMediator mediator, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {

            await _usuarioRepository.DeleteByIdAsync(request.Id);

            return new CommandResult(410);
        }
    }
}
