using AutoMapper;
using Core.Dtos.Ngrok;
using Core.Dtos.User;
using Core.Entities;
using Core.Exceptions;

namespace Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApiExceptionResponse,ApiException>().ReverseMap();
            CreateMap<User,UserModel>().ReverseMap();
            CreateMap<User,AddUserModel>().ReverseMap();
            CreateMap<Tunnel,NgrokModel>().ReverseMap();
        }
    }
}