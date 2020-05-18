using AutoMapper;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
namespace Semerkand.Storage.Stores
{
    public class DersAcilanStore : BaseStore<DersAcilan, DersAcilanDto>, IDersAcilanStore
    {
        public DersAcilanStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }
    }
}
