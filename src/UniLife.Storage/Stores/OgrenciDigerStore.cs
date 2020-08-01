using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class OgrenciDigerStore : BaseStore<OgrenciDiger, OgrenciDigerDto>, IOgrenciDigerStore
    {

        public OgrenciDigerStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
