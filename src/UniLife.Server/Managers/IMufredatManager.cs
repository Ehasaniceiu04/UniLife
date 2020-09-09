using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IMufredatManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(MufredatDto mufredatDto);
        Task<ApiResponse> Update(MufredatDto mufredatDto);
        Task<ApiResponse> Delete(int id);

        Task<ApiResponse> Cokla(int id);
        Task<ApiResponse> GetMufredatByProgramIds(string[] programIds);
        Task<ApiResponse> GetMufredatState(int mufredatId);
        Task<ApiResponse> CreateDersAcilansByMufredatIds(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds);
        Task<ApiResponse> GetLastMufredatByProgramId(int programId);
        Task<ApiResponse> CoklaModified(MufredatDto mufredatDto);
    }
}
