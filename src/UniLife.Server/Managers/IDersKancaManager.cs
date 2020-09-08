using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public interface IDersKancaManager : IBaseManager<DersKanca, DersKancaDto>
    {
        Task<ApiResponse> YeniKanca(DersKancaDto dersKancaDto);
    }
}
