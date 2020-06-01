using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IDersAcilanManager : IBaseManager<DersAcilan, DersAcilanDto>
    {
        Task<ApiResponse> CreateDersAcilanByDers(DersAcDto dersAcDto);
        Task<ApiResponse> GetAcilanDersByFilterDto(DersAcilanFilterDto dersAcilanFilterDto);
        Task<ApiResponse> GetAcilanDersByMufredatId(int mufredatId);
    }
}
