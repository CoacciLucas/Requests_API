using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var user = await _usuarioRepository.GetAllAsync();
            var users = _mapper.Map<List<UserDTO>>(user);

            return users;
        }
        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user = await _usuarioRepository.GetByIdAsync(id);
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }
    }
}
