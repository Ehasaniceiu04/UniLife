using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniLife.Vendor;

namespace UniLife.Storage.Stores
{
    public class YokStore : IYokStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public YokStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }


        public async Task<bool> AskerlikErtelemeTalepGonder()
        {
            YokRest yokRest = new YokRest();
            YokSoap yokSoap = new YokSoap();
            try
            {
                var asd=await yokRest.AskerlikErtelemeTalepGonder();

                var zxc =await yokSoap.EgitimBilgisiOgrenci();

            }
            catch (System.Exception ex)
            {
                throw;
            }
            
            return false;
        }
    }
}
