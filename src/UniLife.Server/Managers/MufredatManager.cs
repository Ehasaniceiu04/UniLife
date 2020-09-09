using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class MufredatManager : IMufredatManager
    {
        private readonly IMufredatStore _mufredatStore;
        private readonly ILogger<MufredatManager> _logger;

        public MufredatManager(IMufredatStore mufredatStore, ILogger<MufredatManager> logger)
        {
            _mufredatStore = mufredatStore;
            _logger = logger;
        }

        public async Task<ApiResponse> Get()
        {
            return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetAll());
        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved MufredatDto", await _mufredatStore.GetById(id));
        }

        public async Task<ApiResponse> Create(MufredatDto mufredatDto)
        {
            var universite = await _mufredatStore.Create(mufredatDto);
            return new ApiResponse(Status200OK, "Created MufredatDto", universite);
        }

        public async Task<ApiResponse> Update(MufredatDto mufredatDto)
        {
            return new ApiResponse(Status200OK, "Müfredat gÜncellendi.", await _mufredatStore.Update(mufredatDto));
        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _mufredatStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete MufredatDto");
        }

        public async Task<ApiResponse> Cokla(int id)
        {
            await _mufredatStore.Cokla(id);
            return new ApiResponse(Status200OK, "Cokla MufredatDto");
        }

        public async Task<ApiResponse> GetMufredatByProgramIds(string[] programIds)
        {
            return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetMufredatByProgramIds(programIds));

        }

        public async Task<ApiResponse> GetMufredatState(int mufredatId)
        {
            return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetMufredatState(mufredatId));
        }

        public async Task<ApiResponse> CreateDersAcilansByMufredatIds(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            await _mufredatStore.CreateDersAcilansByMufredatIds(reqEntityIdWithOtherEntitiesIds);
            return new ApiResponse(Status200OK, "Seçili müfredatların dersleri açıldı.");

        }

        public async Task<ApiResponse> GetLastMufredatByProgramId(int programId)
        {
            return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetLastMufredatByProgramId(programId));
        }

        public async Task<ApiResponse> CoklaModified(MufredatDto mufredatDto)
        {
            await _mufredatStore.CoklaModified(mufredatDto);
            return new ApiResponse(Status200OK, "CoklaModified MufredatDto");
        }
    }
}
