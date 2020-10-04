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
            CreateMap<DersAcilan, DersAcilanDto>().ReverseMap()
                .ForMember(c => c.Mufredat, o => o.Ignore())
                .ForMember(c => c.Program, o => o.Ignore())
                .ForMember(c => c.Bolum, o => o.Ignore())
                .ForMember(c => c.Fakulte, o => o.Ignore())
                .ForMember(c => c.Akademisyen, o => o.Ignore());
            CreateMap<DersKayit, DersKayitDto>().ReverseMap();
            CreateMap<Mufredat, MufredatDto>().ReverseMap();
            CreateMap<OgrenimTur, OgrenimTurDto>().ReverseMap();
            CreateMap<OgrenimDurum, OgrenimDurumDto>().ReverseMap();
            CreateMap<FakulteTur, FakulteTurDto>().ReverseMap();
            CreateMap<Donem, DonemDto>().ReverseMap();
            CreateMap<DonemTip, DonemTipDto>().ReverseMap();
            CreateMap<Ogrenci, OgrenciDto>().ReverseMap();
            CreateMap<Akademisyen, AkademisyenDto>().ReverseMap();
            CreateMap<Personel, PersonelDto>().ReverseMap();
            CreateMap<KayitNeden, KayitNedenDto>().ReverseMap();
            CreateMap<Sinav, SinavDto>().ReverseMap()
                .ForMember(c => c.DersAcilan, o => o.Ignore());
            CreateMap<SinavTip, SinavTipDto>().ReverseMap();
            CreateMap<SinavTur, SinavTurDto>().ReverseMap();
            CreateMap<SinavKayit, SinavKayitDto>().ReverseMap();
            CreateMap<Bina, BinaDto>().ReverseMap();
            CreateMap<Derslik, DerslikDto>().ReverseMap();
            //.ForMember(c => c.DerslikRezervs, o => o.Ignore());
            CreateMap<DerslikRezerv, DerslikRezervDto>().ReverseMap();
                //.ForMember(c => c.ResourceData, o => o.Ignore())
                //.ForMember(c => c.DerslikId, o => o.Ignore());
            CreateMap<YabanciBasvuru, YabanciBasvuruDto>().ReverseMap();
            CreateMap<Nufus, NufusDto>().ReverseMap();
            CreateMap<Osym, OsymDto>().ReverseMap();
            CreateMap<Askerlik, AskerlikDto>().ReverseMap();
            CreateMap<OgrenciDiger, OgrenciDigerDto>().ReverseMap();
            CreateMap<OgrCeza, OgrCezaDto>().ReverseMap();
            CreateMap<OgrDondur, OgrDondurDto>().ReverseMap();
            CreateMap<OgrGecis, OgrGecisDto>().ReverseMap();
            CreateMap<OgrStaj, OgrStajDto>().ReverseMap();
            CreateMap<OgrTez, OgrTezDto>().ReverseMap();
            CreateMap<AkademikTakvim, AkademikTakvimDto>().ReverseMap();
            CreateMap<OgrHarc, OgrHarcDto>().ReverseMap();
            CreateMap<OgrBursBasari, OgrBursBasariDto>().ReverseMap();
            CreateMap<ProgramTur, ProgramTurDto>().ReverseMap();
            CreateMap<DersDil, DersDilDto>().ReverseMap();
            CreateMap<DersNeden, DersNedenDto>().ReverseMap();
            CreateMap<SinavKriter, SinavKriterDto>().ReverseMap();
            CreateMap<DersKanca, DersKancaDto>().ReverseMap();
            CreateMap<Kampus, KampusDto>().ReverseMap();
            CreateMap<PersonelTask, PersonelTaskDto>().ReverseMap();
        }
    }
}
