using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class BolumManager : IBolumManager
    {
        private readonly IBolumStore _bolumStore;

        public BolumManager(IBolumStore bolumStore)
        {
            _bolumStore = bolumStore;
        }

        public async Task<ApiResponse> Get()
        {
                return new ApiResponse(Status200OK, "Retrieved BolumDtos", await _bolumStore.GetAll());
        }

        public async Task<ApiResponse> Get(int id)
        {
                return new ApiResponse(Status200OK, "Retrieved BolumDto", await _bolumStore.GetById(id));
           
        }

        public async Task<ApiResponse> Create(BolumDto bolumDto)
        {
            var universite = await _bolumStore.Create(bolumDto);
            return new ApiResponse(Status200OK, "Created BolumDto", universite);
        }

        public async Task<ApiResponse> Update(BolumDto bolumDto)
        {
                return new ApiResponse(Status200OK, "Updated BolumDto", await _bolumStore.Update(bolumDto));
           
        }

        public async Task<ApiResponse> Delete(int id)
        {
                await _bolumStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete BolumDto");
            
        }

        public async Task<ApiResponse> GetBolumByFakulteId(string[] fakulteIds)
        {
                return new ApiResponse(Status200OK, "Retrieved BolumDtos", await _bolumStore.GetBolumByFakulteId(fakulteIds));
           
        }

        public System.Linq.IQueryable<UniLife.Shared.DataModels.Bolum> GetAllQueryable()
        {
                return _bolumStore.GetAllQueryable();
           
        }
    }
}
