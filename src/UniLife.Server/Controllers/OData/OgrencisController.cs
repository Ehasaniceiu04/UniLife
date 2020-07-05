using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniLife.Shared.DataModels;
using UniLife.Storage;
using UniLife.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace UniLife.Server.Controllers
{
    public class OgrencisController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OgrencisController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [EnableQuery()]
        [HttpGet]
        //[Authorize(Permissions.Ogrenci.Create)]
        [ODataRoute("GetTopAta(ProgramId={ProgramId},KayitNedenId={KayitNedenId},OgrenimDurumId={OgrenimDurumId},Sinif={Sinif},Cinsiyet={Cinsiyet})")]
        public IEnumerable<Ogrenci> GetTopAta([FromODataUri] int ProgramId
                                            , [FromODataUri] int KayitNedenId
                                            , [FromODataUri] int OgrenimDurumId
                                            , [FromODataUri] int Sinif
                                            , [FromODataUri] int Cinsiyet
                                             )
        {

            //var filteredQuery = from o in _applicationDbContext.Ogrencis.WhereIf(ProgramId != 0, x => x.ProgramId == ProgramId)
            //                                                            .WhereIf(KayitNedenId != 0, x => x.KayitNedenId == KayitNedenId)
            //                                                            .WhereIf(OgrenimDurumId != 0, x => x.OgrenimDurumId == OgrenimDurumId)
            //                                                            .WhereIf(Sinif != 0, x => x.Sinif == Sinif)
            //                                                            .WhereIf(Cinsiyet != 0, x => x.IsMale == (Cinsiyet == 2))
            //                    join a in _applicationDbContext.Akademisyens on o.DanismanId equals a.Id into akaLeft
            //                    from m in akaLeft.DefaultIfEmpty()
            //                    join p in _applicationDbContext.Programs on o.ProgramId equals p.Id
            //                    select o;

            var filteredQuery = from o in _applicationDbContext.Ogrencis.Where(x => ProgramId == 0 ? true : x.ProgramId == ProgramId)
                                                                        .Where(x => KayitNedenId == 0 ? true : x.KayitNedenId == KayitNedenId)
                                                                        .Where(x => OgrenimDurumId == 0 ? true : x.OgrenimDurumId == OgrenimDurumId)
                                                                        .Where(x => Sinif == 0 ? true : x.Sinif == Sinif)
                                                                        .Where(x => Cinsiyet == 0? true : x.IsMale == (Cinsiyet == 2))
                                //join p in _applicationDbContext.Programs on o.ProgramId equals p.Id
                                //join a in _applicationDbContext.Akademisyens on o.DanismanId equals a.Id into akaLeft
                                //from m in akaLeft.DefaultIfEmpty()
                                
                                select o;


            return filteredQuery;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        public IEnumerable<Ogrenci> Get()
        {
            return _applicationDbContext.Ogrencis;
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
