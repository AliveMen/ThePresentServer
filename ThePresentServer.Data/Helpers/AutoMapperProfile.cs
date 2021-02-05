using AutoMapper;
using ThePresentServer.Data.Entities;
using ThePresentServer.Data.Models.Users;

namespace ThePresentServer.Data.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<RegisterModel, UserEntity>();
            CreateMap<UpdateModel, UserEntity>();
        }
    }
}