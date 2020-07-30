using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class NufusStore : BaseStore<Nufus, NufusDto>, INufusStore
    {

        public NufusStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
