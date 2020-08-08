using IdentityServer4.Extensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniLife.Server.Managers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage;

namespace UniLife.Server.Controllers
{
    public class OgrencisController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IOgrenciManager _ogrenciManager;

        public OgrencisController(IApplicationDbContext applicationDbContext, IOgrenciManager ogrenciManager)
        {
            _applicationDbContext = applicationDbContext;
            _ogrenciManager = ogrenciManager;
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
        //                        group da by new { o.Id, o.OgrNo, o.Ad, o.Soyad }
        //                        into grp
        //                        select new OgrenciDersRaporDto
        //                        {
        //                            Id = grp.Key.Id,
        //                            OgrNo = grp.Key.OgrNo,
        //                            Ad = grp.Key.Ad,
        //                            Soyad = grp.Key.Soyad,
        //                            TopKredi = grp.Sum(t => t.Kredi)
        //                        };

        //    return filteredQuery;
        //}



        //[EnableQuery()]
        //[HttpGet]
        ////[Authorize(Permissions.Ogrenci.Create)]
        //[ODataRoute("GetTopAta(ProgramId={ProgramId},KayitNedenId={KayitNedenId},OgrenimDurumId={OgrenimDurumId},Sinif={Sinif},Cinsiyet={Cinsiyet})")]
        //public IEnumerable<Ogrenci> GetTopAta([FromODataUri] int ProgramId
        //                                    , [FromODataUri] int KayitNedenId
        //                                    , [FromODataUri] int OgrenimDurumId
        //                                    , [FromODataUri] int Sinif
        //                                    , [FromODataUri] int Cinsiyet
        //                                     )
        //{

        //    //var filteredQuery = from o in _applicationDbContext.Ogrencis.WhereIf(ProgramId != 0, x => x.ProgramId == ProgramId)
        //    //                                                            .WhereIf(KayitNedenId != 0, x => x.KayitNedenId == KayitNedenId)
        //    //                                                            .WhereIf(OgrenimDurumId != 0, x => x.OgrenimDurumId == OgrenimDurumId)
        //    //                                                            .WhereIf(Sinif != 0, x => x.Sinif == Sinif)
        //    //                                                            .WhereIf(Cinsiyet != 0, x => x.IsMale == (Cinsiyet == 2))
        //    //                    join a in _applicationDbContext.Akademisyens on o.DanismanId equals a.Id into akaLeft
        //    //                    from m in akaLeft.DefaultIfEmpty()
        //    //                    join p in _applicationDbContext.Programs on o.ProgramId equals p.Id
        //    //                    select o;

        //    var filteredQuery = from o in _applicationDbContext.Ogrencis.Where(x => ProgramId == 0 ? true : x.ProgramId == ProgramId)
        //                                                                .Where(x => KayitNedenId == 0 ? true : x.KayitNedenId == KayitNedenId)
        //                                                                .Where(x => OgrenimDurumId == 0 ? true : x.OgrenimDurumId == OgrenimDurumId)
        //                                                                .Where(x => Sinif == 0 ? true : x.Sinif == Sinif)
        //                                                                .Where(x => Cinsiyet == 0 ? true : x.IsMale == (Cinsiyet == 2))
        //                            //join p in _applicationDbContext.Programs on o.ProgramId equals p.Id
        //                            //join a in _applicationDbContext.Akademisyens on o.DanismanId equals a.Id into akaLeft
        //                            //from m in akaLeft.DefaultIfEmpty()

        //                        select o;


        //    return filteredQuery;
        //}

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        public IEnumerable<Ogrenci> Get()
        {
            return _applicationDbContext.Ogrencis;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [ODataRoute("GetForDanisman")]
        [Authorize] //[Authorize(Roles = "Administrator,Personel")]
        public IEnumerable<Ogrenci> GetForDanisman()
        {
            //sadece login olan akademsyenin öğrencileri.
            var aka =  _applicationDbContext.Akademisyens.FirstOrDefault(x => x.ApplicationUserId.ToString() == User.GetSubjectId());

            return _applicationDbContext.Ogrencis.Where(x=>x.DanismanId == aka.Id);
        }

        //[Microsoft.AspNet.OData.EnableQuery()]
        //[HttpGet]
        //public IEnumerable<Shared.Dto.Definitions.ResOgrTopAtaFilters> Get()
        //{
        //    var filteredQuery = from o in _applicationDbContext.Ogrencis
        //                        join a in _applicationDbContext.Akademisyens on o.DanismanId equals a.Id into akaLeft
        //                        from m in akaLeft.DefaultIfEmpty()
        //                        join p in _applicationDbContext.Programs on o.ProgramId equals p.Id
        //                        select new Shared.Dto.Definitions.ResOgrTopAtaFilters
        //                        {
        //                            OgrenciId = o.Id,
        //                            Ad =o.Ad,
        //                            Soyad = o.Soyad,
        //                            OgrenciNo = o.OgrNo
        //                        };
        //    return filteredQuery;
        //}

    }
}
