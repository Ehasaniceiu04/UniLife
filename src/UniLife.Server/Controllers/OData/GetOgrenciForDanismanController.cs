using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UniLife.Shared.DataModels;
using UniLife.Storage;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.Dto.Definitions;
using IdentityServer4.Extensions;

namespace UniLife.Server.Controllers
{
    public class GetOgrenciForDanismanController : ControllerBase
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetOgrenciForDanismanController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Microsoft.AspNet.OData.EnableQuery()]
        [HttpGet]
        [Authorize(Roles = "Administrator,Personel,Akademisyen")]
        public IEnumerable<OgrenciOnay> Get()
        {
            //sadece login olan akademsyenin öğrencileri.
            var aka = _applicationDbContext.Akademisyens.FirstOrDefault(x => x.ApplicationUserId.ToString() == User.GetSubjectId());

            var aktifDonem = _applicationDbContext.Donems.FirstOrDefault(x => x.Durum == true);

            var asd= (from o in _applicationDbContext.Ogrencis.Where(x => x.DanismanId == aka.Id)
                    let cCount = o.DersKayits.Where(x => x.IsOnayli == true && x.DersAcilan.DonemId == aktifDonem.Id).Count()
                    select new OgrenciOnay
                    {
                        Id = o.Id,
                        Ad = o.Ad,
                        Soyad = o.Soyad,
                        TCKN = o.TCKN,
                        KayitTarih = o.KayitTarih,
                        OgrNo = o.OgrNo,
                        DersKayitOnayli = cCount > 0
                    });

            return asd;

        }

    }
}
