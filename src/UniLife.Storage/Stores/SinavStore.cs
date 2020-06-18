using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class SinavStore : BaseStore<Sinav, SinavDto>, ISinavStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public SinavStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<SinavDto>> GetSinavListByAcilanDersId(int dersId)
        {
            //var ogrenciDetail = await (from s in _db.Sinavs.Where(x => x.DersAcilanId == dersId)
            //                           join dk in _db.DersKayits on s.DersAcilanId equals dk.DersAcilanId into g1
            //                           from g2 in g1.DefaultIfEmpty()
            //                           group g2 by g2.DersAcilanId into grouped
            //                           select new SinavDto
            //                           {
            //                               Id = g2..Id,
            //                               Ad = s.Ad,
            //                               DersAcilanId = s.DersAcilanId,
            //                               SinavTipId = s.SinavTipId,
            //                               SinavTurId = s.SinavTurId,
            //                               SablonAd = s.SablonAd,
            //                               EtkiOran = s.EtkiOran,
            //                               IsKilit = s.IsKilit,
            //                               Tarih = s.Tarih,
            //                               TarihIlan = s.TarihIlan,
            //                               KisaAd = s.KisaAd,
            //                               OgrCount = grouped.Count()
            //                           }).ToListAsync();

            var ogrenciDetail2 = await (from s in _db.Sinavs.Where(x => x.DersAcilanId == dersId)
                                        let cCount =
                                        (
                                          from c in _db.DersKayits
                                          where s.DersAcilanId == c.DersAcilanId
                                          select c
                                        ).Count()
                                        select new SinavDto
                                        {
                                            Id = s.Id,
                                            Ad = s.Ad,
                                            DersAcilanId = s.DersAcilanId,
                                            SinavTipId = s.SinavTipId,
                                            SinavTurId = s.SinavTurId,
                                            SablonAd = s.SablonAd,
                                            EtkiOran = s.EtkiOran,
                                            IsKilit = s.IsKilit,
                                            Tarih = s.Tarih,
                                            TarihIlan = s.TarihIlan,
                                            KisaAd = s.KisaAd,
                                            OgrCount = cCount
                                        }).ToListAsync();


            //var ogrenciDto = await _autoMapper.ProjectTo<OgrenciDto>(_db.Ogrencis).FirstOrDefaultAsync(x => x.Id == id); //_db.Ogrencis.SingleOrDefaultAsync(t => t.Id == id);

            if (ogrenciDetail2 == null)
                throw new InvalidDataException($"Unable to find Sinav with ID: {dersId}");

            return ogrenciDetail2;
        }
    }
}
