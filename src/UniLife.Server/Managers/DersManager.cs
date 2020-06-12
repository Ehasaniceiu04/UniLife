using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class DersManager : IDersManager
    {
        private readonly IDersStore _dersStore;

        public DersManager(IDersStore dersStore)
        {
            _dersStore = dersStore;
        }

        public async Task<ApiResponse> Get()
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved DersDtos", await _dersStore.GetAll());
            }
            catch (Exception ex)
            {
                return new ApiResponse(Status400BadRequest, ex.Message);
            }
        }

        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved DersDto", await _dersStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve DersDto");
            }
        }

        public async Task<ApiResponse> Create(DersDto dersDto)
        {
            var ders = await _dersStore.Create(dersDto);
            return new ApiResponse(Status200OK, "Created Ders", ders);
        }

        public async Task<ApiResponse> Update(DersDto dersDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated DersDto", await _dersStore.Update(dersDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update DersDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _dersStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete DersDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update DersDto");
            }
        }

        public async Task<ApiResponse> GetDersByMufredatId(int mufredatId)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved DersDto", await _dersStore.GetDersByMufredatId(mufredatId));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve DersDto");
            }
        }

        public async Task<ApiResponse> GetAcilacakDersByFilterDto(DersFilterDto dersFilterDto)
        {
            var derss = await _dersStore.GetAcilacakDersByFilterDto(dersFilterDto);
            return new ApiResponse(Status200OK, "Selected DersDto", derss);
        }
    }
}
