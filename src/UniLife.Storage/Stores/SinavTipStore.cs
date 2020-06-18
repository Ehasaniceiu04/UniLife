using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class SinavTipStore : BaseStore<SinavTip, SinavTipDto>, ISinavTipStore
    {
        public SinavTipStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {

        }

    }
}
