using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DersKancaManager : BaseManager<DersKanca, DersKancaDto>, IDersKancaManager
    {
        private readonly IDersKancaStore _dersKancaStore;
        public DersKancaManager(IDersKancaStore dersKancaStore) : base(dersKancaStore)
        {
            _dersKancaStore = dersKancaStore;
        }

        public async Task<ApiResponse> YeniKanca(DersKancaDto dersKancaDto)
        {
            await _dersKancaStore.YeniKanca(dersKancaDto);
            return new ApiResponse(Status200OK, "Bağlı dersler güncellendi", null);
        }
    }
}
