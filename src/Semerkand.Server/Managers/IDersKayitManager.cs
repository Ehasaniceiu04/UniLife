using Semerkand.Server.Middleware.Wrappers;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Server.Managers
{
    public interface IDersKayitManager : IBaseManager<DersKayit, DersKayitDto>
    {
        Task<ApiResponse> OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos);
    }
}
