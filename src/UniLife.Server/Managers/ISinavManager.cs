using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public interface ISinavManager : IBaseManager<Sinav, SinavDto>
    {
        Task<ApiResponse> GetSinavListByAcilanDersId(int dersId);
        Task<ApiResponse> PostBulkCreate(SinavDto sinavDto);
    }
}
