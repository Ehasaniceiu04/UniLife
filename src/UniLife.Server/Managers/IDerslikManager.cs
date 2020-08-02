using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public interface IDerslikManager : IBaseManager<Derslik, DerslikDto>
    {
        Task<ApiResponse> GetDersliksAndDerslikRezsByMufredatId(int mufredatId);
    }
}
