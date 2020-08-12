using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class AkademikTakvimManager : BaseManager<AkademikTakvim, AkademikTakvimDto>, IAkademikTakvimManager
    {
        private readonly IAkademikTakvimStore _akademikTakvimStore;
        private readonly ILogger<ExternalAuthManager> _logger;
        public AkademikTakvimManager(IAkademikTakvimStore akademikTakvimStore,
            ILogger<ExternalAuthManager> logger) : base(akademikTakvimStore)
        {
            _akademikTakvimStore = akademikTakvimStore;
            _logger = logger;
        }

        public async Task<ApiResponse> GetAkaTakByFakIdDonId(int fakulteId, int donemId)
        {
            var sonuc = await _akademikTakvimStore.GetAkaTakByFakIdDonId(fakulteId, donemId);
            return new ApiResponse(Status200OK, "GetAkaTakByFakIdDonId fetched", sonuc);
        }

        public async Task<ApiResponse> PostChangeAllFakultesTakvim(AkademikTakvimDto akademikTakvimDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated SetDatesToAllFaksForKod", await _akademikTakvimStore.PostChangeAllFakultesTakvim(akademikTakvimDto));
            }
            catch (DomainException ex)
            {
                _logger.LogError("Takvim toplu güncellemde hata oluştu: {0}, {1}", ex.Description, ex.Message);
                return new ApiResponse(Status400BadRequest, $"Takvim toplu güncellemde hata oluştu: {ex.Description} ");
            }
            

        }
    }
}
