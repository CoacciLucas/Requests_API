using Application.Commands;
using Application.DTO;
using Application.Reads.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CadastrarUsuarioCommand, User>();

            CreateMap<ObterUserPorIdQuery, User>();

            CreateMap<User, UserDTO>();

        }
    }
}
