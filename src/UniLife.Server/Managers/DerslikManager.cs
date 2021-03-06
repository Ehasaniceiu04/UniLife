using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DerslikManager : BaseManager<Derslik, DerslikDto>, IDerslikManager
    {
        private readonly IDerslikStore _derslikStore;
        public DerslikManager(IDerslikStore derslikStore) : base(derslikStore)
        {
            _derslikStore = derslikStore;
        }

        public async Task<ApiResponse> GetDersliksAndDerslikRezsByMufredatId(int mufredatId, int ogrenciId, bool isSinav)
        {
            var dersliks = await _derslikStore.GetDersliksAndDerslikRezsByMufredatId(mufredatId, ogrenciId, isSinav);
            return new ApiResponse(Status200OK, "GetDersliksAndDerslikRezsByMufredatId fetched", dersliks);
        }

        public async Task<ApiResponse> GetDersliksAndDerslikRezsByAkaId(int akaId, bool isSinav)
        {
            var dersliks = await _derslikStore.GetDersliksAndDerslikRezsByAkaId(akaId, isSinav);
            return new ApiResponse(Status200OK, "GetDersliksAndDerslikRezsByAkaId fetched", dersliks);
        }
    }
}
