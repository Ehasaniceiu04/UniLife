using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class DerslikRezervStore : BaseStore<DerslikRezerv, DerslikRezervDto>, IDerslikRezervStore
    {

        public DerslikRezervStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
