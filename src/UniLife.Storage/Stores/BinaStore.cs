using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class BinaStore : BaseStore<Bina, BinaDto>, IBinaStore
    {

        public BinaStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
