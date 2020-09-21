using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;
using System.Linq;
using UniLife.Shared.Dto.Definitions;
using Microsoft.AspNetCore.Authorization;
using UniLife.Shared.AuthorizationDefinitions;

namespace UniLife.Server.Controllers
{
    public class DersAcilansExtController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DersAcilansExtController(IApplicationDbContext applicationDbContext)
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
                                 Ad = da.Ad,
                                 Sinif = da.Sinif,
                                 AdEn = da.AdEn,
                                 ADKayit = da.ADKayit,
                                 AkademisyenId = da.AkademisyenId,
                                 Akts = da.Akts,
                                 AltKont = da.AltKont,
                                 BolDisKont = da.BolDisKont,
                                 AltKota =da.AltKota,
                                 BolDisKota =da.BolDisKota,
                                 BolumId = da.BolumId,
                                 DersDilId = da.DersDilId,
                                 DersId=da.DersId,
                                 DersNedenId=da.DersNedenId,
                                 DonemId=da.DonemId,
                                 Durum=da.Durum,
                                 EskiMufBagliDersId=da.EskiMufBagliDersId,
                                 FakulteId=da.FakulteId,
                                 KisaAd=da.KisaAd,
                                 Kod=da.Kod,
                                 Kredi=da.Kredi,
                                 DersKayitCount = cCount,
                                 AkademisyenAd = a.Ad,
                                 LabSaat=da.LabSaat,
                                 MufredatId=da.MufredatId,
                                 ODTekrar=da.ODTekrar,
                                 OptikKod=da.OptikKod,
                                 ProgramId=da.ProgramId,
                                 SecmeliKodu=da.SecmeliKodu,
                                 Sube=da.Sube,
                                 TeoSaat=da.TeoSaat,
                                 TopKont=da.TopKont,
                                 UygSaat=da.UygSaat,
                                 Zorunlu=da.Zorunlu
                             };
            return daForSinav;

        }

    }
}
