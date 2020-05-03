﻿using AutoMapper.Configuration;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Account;
using Semerkand.Shared.Dto.Sample;
using ApiLogItem = Semerkand.Shared.DataModels.ApiLogItem;
using Message = Semerkand.Shared.DataModels.Message;
using UserProfile = Semerkand.Shared.DataModels.UserProfile;

namespace Semerkand.Storage.Mapping
{
    public class MappingProfile : MapperConfigurationExpression
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();           
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
            CreateMap<ApiLogItem, ApiLogItemDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }
}
