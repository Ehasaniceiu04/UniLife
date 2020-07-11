using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class OgrenciDersRaporsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OgrenciDersRaporsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        
        //[EnableQuery()]
        //[HttpGet]
        ////[Authorize(Permissions.Ogrenci.Create)]
        //[ODataRoute("GetOgrenciDers(TopKredi={TopKredi})")]
        //public IEnumerable<OgrenciDersRaporDto> GetOgrenciDers([FromODataUri] bool TopKredi)
        //{

        //    var filteredQuery = from o in _applicationDbContext.Ogrencis
        //                        join dk in _applicationDbContext.DersKayits on o.Id equals dk.OgrenciId
        //                        join da in _applicationDbContext.DersAcilans on dk.DersAcilanId equals da.Id
        //                        group da by new { o.Id, o.OgrNo, o.Ad, o.Soyad}
        //                        into grp
        //                        select new OgrenciDersRaporDto
        //                        {
        //                            OgrenciId = grp.Key.Id,
        //                            OgrNo= grp.Key.OgrNo,
        //                            Ad = grp.Key.Ad,
        //                            Soyad = grp.Key.Soyad,
        //                            TopKredi = grp.Sum(t => t.Kredi)
        //                        };

        //    return filteredQuery;
        //}



        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        public IEnumerable<OgrenciDersRaporDto> Get() 
        {
            //Get(int programId,int bolumId,int fakulteId) yapılırsa  http://localhost:53414/odata/OgrenciDersRapors?programId=1 şeklinde de querye giydirilebilir.

            var filteredQuery = from o in _applicationDbContext.Ogrencis
                                join dk in _applicationDbContext.DersKayits on o.Id equals dk.OgrenciId
                                join da in _applicationDbContext.DersAcilans on dk.DersAcilanId equals da.Id
                                group da by new { o.Id, o.OgrNo, o.Ad, o.Soyad, o.ProgramId, o.BolumId, o.Sinif, o.FakulteId }
                                into grp
                                select new OgrenciDersRaporDto
                                {
                                    Id = grp.Key.Id,
                                    OgrNo = grp.Key.OgrNo,
                                    Ad = grp.Key.Ad,
                                    Soyad = grp.Key.Soyad,
                                    ProgramId = grp.Key.ProgramId,
                                    BolumId = grp.Key.BolumId,
                                    FakulteId = grp.Key.FakulteId,
                                    Sinif = grp.Key.Sinif,
                                    TopKredi = grp.Sum(t => t.Kredi)
                                };

            return filteredQuery;
        }


    }
}
