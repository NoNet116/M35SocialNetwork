﻿using App.Data.Models;
using AutoMapper;
using M35SocialNetwork.ViewModels.Account;

namespace M35SocialNetwork.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
        .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.FirstName));

            /*CreateMap<LoginViewModel, User>()
        .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email));
            CreateMap<UserEditViewModel, User>().ReverseMap();
            CreateMap<UserWithFriendExt, User>().ReverseMap();*/
        }
    }
}
