using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class YabanciBasvuruStore : BaseStore<YabanciBasvuru, YabanciBasvuruDto>, IYabanciBasvuruStore
    {

        public YabanciBasvuruStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
