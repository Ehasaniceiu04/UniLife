using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using UniLife.Shared.AuthorizationDefinitions;

namespace UniLife.Server.Controllers
{
    public class DersAcilansForSinavsController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersAcilansForSinavsController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Permissions.DersAcilan.Read)]
        public IEnumerable<DersAcilanForSinav> Get()
        {
            var daForSinav = from da in _applicationDbContext.DersAcilans
                             join a in _applicationDbContext.Akademisyens on da.AkademisyenId equals a.Id into AkademisyensLeft
                             from a in AkademisyensLeft.DefaultIfEmpty()
                             let cCount = da.DersKayits.Count()
                             select new DersAcilanForSinav
                             {
                                 Id = da.Id,
                                 Kod = da.Kod,
                                 Ad = da.Ad,
                                 Sinif = da.Sinif,
                                 Zorunlu = da.Zorunlu,
                                 AkademisyenAd = a.Ad,
                                 DersKayitCount = cCount,
                                 ProgramId = da.ProgramId,
                                 FakulteId = da.FakulteId,
                                 BolumId =da.BolumId,
                                 DonemId = da.DonemId,
                                 IsYillik = da.IsYillik
                             };
            return daForSinav;

        }

    }
}
