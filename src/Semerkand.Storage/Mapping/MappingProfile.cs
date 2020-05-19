using AutoMapper.Configuration;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Account;
using Semerkand.Shared.Dto.Definitions;
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
            
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

            CreateMap<Universite, UniversiteDto>().ReverseMap();
            CreateMap<Fakulte, FakulteDto>().ReverseMap();
            CreateMap<Bolum, BolumDto>().ReverseMap();
            CreateMap<Program, ProgramDto>().ReverseMap();
            CreateMap<Harc, HarcDto>().ReverseMap();
            CreateMap<Mufredat, MufredatDto>().ReverseMap();
            CreateMap<Ders, DersDto>().ReverseMap();
            CreateMap<DersAcilan, DersAcilanDto>().ReverseMap();
            CreateMap<Mufredat, MufredatDto>().ReverseMap();
            CreateMap<OgrenimTur, OgrenimTurDto > ().ReverseMap();
            CreateMap<OgrenimDurum, OgrenimDurumDto>().ReverseMap();
            CreateMap<FakulteTur, FakulteTurDto>().ReverseMap();
            CreateMap<Donem , DonemDto>().ReverseMap();
            CreateMap<DonemTip, DonemTipDto>().ReverseMap();
            CreateMap<Ogrenci, OgrenciDto>().ReverseMap();
            CreateMap<KayitNeden, KayitNedenDto>().ReverseMap();
        }
    }
}
