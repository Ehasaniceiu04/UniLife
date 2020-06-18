using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class SinavTurStore : BaseStore<SinavTur, SinavTurDto>, ISinavTurStore
    {
        public SinavTurStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }
    }
}
