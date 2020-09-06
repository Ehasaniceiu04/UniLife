using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class OgrBursBasariStore : BaseStore<OgrBursBasari, OgrBursBasariDto>, IOgrBursBasariStore
    {

        public OgrBursBasariStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
