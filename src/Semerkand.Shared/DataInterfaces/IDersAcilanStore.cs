using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IDersAcilanStore : IBaseStore<DersAcilan, DersAcilanDto>
    {
        //BUlk insering bilgilerini dönebiliriz List<DersAcilan> 
        Task<bool> CreateDersAcilanByDers(DersAcDto dersAcDto);
    }
}
