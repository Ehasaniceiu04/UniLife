﻿using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.IO;

namespace UniLife.Storage.Stores
{
    public class DersKayitStore : BaseStore<DersKayit, DersKayitDto>, IDersKayitStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public DersKayitStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task DeleteByOgrId_DersId(int ogrenciId, int dersId)
        {
            var dersKayit = _db.DersKayits.SingleOrDefault(t => t.OgrenciId == ogrenciId && t.DersAcilanId == dersId);

            if (dersKayit == null)
            {
                //throw new InvalidDataException($"Unable to find Todo with ogrenciId: {ogrenciId} dersId : {dersId}");
            }
            else
            {
                _db.DersKayits.Remove(dersKayit);
                await _db.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<List<OgrenciDersKayitDto>> GetOgrenciDersKayitsByDers(int dersAcilanId)
        {
            var ogrenciDersKayits = from dk in _db.DersKayits.Where(x => x.DersAcilanId == dersAcilanId)
                                    join o in _db.Ogrencis on dk.OgrenciId equals o.Id
                                    select new OgrenciDersKayitDto
                                    {
                                        OgrId = o.Id,
                                        OgrNo = o.OgrNo,
                                        OgrAd = o.Ad,
                                        OgrSoy = o.Soyad,
                                        AlTip = dk.AlTip,
                                        HBN = dk.HBN,
                                        TSkor = dk.TSkor,
                                        HarfN = dk.HarfNot,
                                        GecDurum= dk.GecDurum?"Geçti":"Kaldı"
                                    };
            return await ogrenciDersKayits.ToListAsync();
        }

        public async Task HedefKaynakOgrAktar(HedefKaynakDto hedefKaynakDto)
        {
            var resultKaynak = hedefKaynakDto.KaynakIdList.Where(x => !hedefKaynakDto.HedefIdList.Contains(x));

            var silinecekler = await _db.DersKayits.Where(x => x.DersAcilanId == hedefKaynakDto.KaynakId && hedefKaynakDto.KaynakIdList.Contains(x.OgrenciId)).ToListAsync();

            _db.DersKayits.RemoveRange(silinecekler);

            List<DersKayit> dersKayits = new List<DersKayit>();
            foreach (var item in resultKaynak)
            {
                dersKayits.Add(new DersKayit
                {
                    DersAcilanId = hedefKaynakDto.HedefId,
                    OgrenciId = item,
                    DersYerineSecilenId = hedefKaynakDto.HedefId,
                    
                });
            }
            _db.DersKayits.AddRange(dersKayits);
            await _db.SaveChangesAsync(CancellationToken.None);
        }


        public async Task HedefKaynakOgrDersKayit(HedefKaynakDto hedefKaynakDto)
        {
            var existDersKayits =await _db.DersKayits.Where(x => hedefKaynakDto.KaynakIdList.Contains(x.OgrenciId) && hedefKaynakDto.HedefIdList.Contains(x.DersAcilanId)).ToListAsync();

            List<DersKayit> dersKayits = new List<DersKayit>();
            foreach (var ogrenciId in hedefKaynakDto.KaynakIdList)
            {
                foreach (var dersAcId in hedefKaynakDto.HedefIdList)
                {
                    if (!existDersKayits.Any(x=>x.DersAcilanId== dersAcId && x.OgrenciId == ogrenciId))
                    {
                        dersKayits.Add(new DersKayit
                        {
                            DersAcilanId = dersAcId,
                            OgrenciId = ogrenciId,
                            DersYerineSecilenId = dersAcId
                        });
                    }
                }
            }

            _db.DersKayits.AddRange(dersKayits);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task OgrenciKayitToDerss(IEnumerable<DersKayitDto> dersKayitDtos)
        {
            var silinecekler = from k in _db.DersKayits.Where(x => (x.OgrenciId == dersKayitDtos.FirstOrDefault().OgrenciId) && dersKayitDtos.Select(x => x.DersAcilanId).Contains(x.DersAcilanId))
                               select k;
            _db.DersKayits.RemoveRange(silinecekler.ToList());

            var dersKayits = _autoMapper.Map<IEnumerable<DersKayitDto>, IEnumerable<DersKayit>>(dersKayitDtos);
            _db.DersKayits.AddRange(dersKayits);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> PutUpdateOgrencisDersKayits(PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto)
        {

            var dersKayits = _db.DersKayits.Where(x => x.DersAcilanId == putUpdateOgrencisDersKayitsDto.SelectedDersAcilanId
                                                    && putUpdateOgrencisDersKayitsDto.OgrenciIds.Contains(x.OgrenciId));

            await dersKayits.ForEachAsync(x => x.DersAcilanId = putUpdateOgrencisDersKayitsDto.PointedDersAcilanId);

            return await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> PutUpdateOgrencisDersKayitsDeleteExSubes(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {
                var dersKayits = _db.DersKayits.Where(x => reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.DersAcilanId));
                await dersKayits.ForEachAsync(x => x.DersAcilanId = reqEntityIdWithOtherEntitiesIds.EntityId);

                var updateOgr = await _db.SaveChangesAsync(CancellationToken.None);

                var fazlalikSubes = _db.DersAcilans.Where(x => reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.Id)).ToArray();
                _db.DersAcilans.RemoveRange(fazlalikSubes);

                await _db.SaveChangesAsync(CancellationToken.None);

                context.Commit();
                return updateOgr;
            }
        }
    }
}
