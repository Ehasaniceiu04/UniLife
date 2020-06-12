using AutoMapper.Configuration;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Account;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Sample;
using ApiLogItem = UniLife.Shared.DataModels.ApiLogItem;
using Message = UniLife.Shared.DataModels.Message;
using UserProfile = UniLife.Shared.DataModels.UserProfile;

namespace UniLife.Storage.Mapping
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
            CreateMap<DersKayit, DersKayitDto>().ReverseMap();
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
