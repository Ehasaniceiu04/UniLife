﻿using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace UniLife.Server.Managers
{
    public class OgrenciManager : BaseManager<Ogrenci, OgrenciDto>, IOgrenciManager
    {

        private readonly IOgrenciStore _ogrenciStore;
        public OgrenciManager(IOgrenciStore ogrenciStore) : base(ogrenciStore)
        {
            _ogrenciStore = ogrenciStore;
            
        }

        public async Task<ApiResponse> GetOgrenciQuery(OgrenciDto ogrenci)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved OgrenciDto", await _ogrenciStore.GetOgrenciQuery(ogrenci));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve OgrenciDto");
            }
        }

        public async Task<ApiResponse> GetOgrenciWithRelations(int id)
        {
            try
            {
                return new ApiResponse(Status200OK, "Retrieved OgrenciDto", await _ogrenciStore.GetOgrenciWithRelations(id));
            }
            catch (Exception e)
            {
                return new ApiResponse(Status400BadRequest, "Failed to Retrieve OgrenciDto");
            }
        }
    }
}