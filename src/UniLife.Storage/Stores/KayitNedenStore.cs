using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class KayitNedenStore : BaseStore<KayitNeden, KayitNedenDto>, IKayitNedenStore
    {

        public KayitNedenStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
