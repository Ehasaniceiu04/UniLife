﻿using System.Threading.Tasks;
using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
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
            return new ApiResponse(Status200OK, "Retrieved DersDtos", await _dersStore.GetAll());

        }

        public async Task<ApiResponse> Get(int id)
        {
            return new ApiResponse(Status200OK, "Retrieved DersDto", await _dersStore.GetById(id));

        }

        public async Task<ApiResponse> Create(DersDto dersDto)
        {
            var ders = await _dersStore.Create(dersDto);
            return new ApiResponse(Status200OK, "Created Ders", ders);
        }

        public async Task<ApiResponse> Update(DersDto dersDto)
        {
            return new ApiResponse(Status200OK, "Updated DersDto", await _dersStore.Update(dersDto));

        }

        public async Task<ApiResponse> Delete(int id)
        {
            await _dersStore.DeleteById(id);
            return new ApiResponse(Status200OK, "Soft Delete DersDto");

        }

        public async Task<ApiResponse> GetDersByMufredatId(int mufredatId)
        {
            return new ApiResponse(Status200OK, "Retrieved DersDto", await _dersStore.GetDersByMufredatId(mufredatId));

        }

        public async Task<ApiResponse> GetAcilacakDersByFilterDto(DersFilterDto dersFilterDto)
        {
            var derss = await _dersStore.GetAcilacakDersByFilterDto(dersFilterDto);
            return new ApiResponse(Status200OK, "Selected DersDto", derss);
        }
    }
}
