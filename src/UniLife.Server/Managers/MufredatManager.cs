﻿using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.Extensions.Logging;

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
            try
            {
                return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetAll());
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
                return new ApiResponse(Status200OK, "Retrieved MufredatDto", await _mufredatStore.GetById(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve MufredatDto");
            }
        }

        public async Task<ApiResponse> Create(MufredatDto mufredatDto)
        {
            var universite = await _mufredatStore.Create(mufredatDto);
            return new ApiResponse(Status200OK, "Created MufredatDto", universite);
        }

        public async Task<ApiResponse> Update(MufredatDto mufredatDto)
        {
            try
            {
                return new ApiResponse(Status200OK, "Updated MufredatDto", await _mufredatStore.Update(mufredatDto));
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update MufredatDto");
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await _mufredatStore.DeleteById(id);
                return new ApiResponse(Status200OK, "Soft Delete MufredatDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to update MufredatDto");
            }
        }

        public async Task<ApiResponse> Cokla(int id)
        {
            try
            {
                await _mufredatStore.Cokla(id);
                return new ApiResponse(Status200OK, "Cokla MufredatDto");
            }
            catch (InvalidDataException dataException)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Cokla MufredatDto");
            }
        }

        public async Task<ApiResponse> GetMufredatByProgramIds(string[] programIds)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetMufredatByProgramIds(programIds));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve MufredatDtos");
            }
        }

        public async Task<ApiResponse> GetMufredatState(int mufredatId)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved MufredatDtos", await _mufredatStore.GetMufredatState(mufredatId));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to GetMufredatState");
            }
        }

        public async Task<ApiResponse> CreateDersAcilansByMufredatIds(IntEnumarableDto intEnumarableDto)
        {
                await _mufredatStore.CreateDersAcilansByMufredatIds(intEnumarableDto);
                return new ApiResponse(Status200OK, "Seçili müfredatların dersleri açıldı.");

        }
    }
}
