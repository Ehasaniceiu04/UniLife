using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class DerslikStore : BaseStore<Derslik, DerslikDto>, IDerslikStore
    {

        public DerslikStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
