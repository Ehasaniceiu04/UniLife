using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Sample;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;
using System.Security.Cryptography.X509Certificates;
using UniLife.Shared.Helpers;
using System;
using UniLife.Shared;

namespace UniLife.Storage.Stores
{
    public class SinavKayitStore : ISinavKayitStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public SinavKayitStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<SinavKayitDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<SinavKayitDto>(_db.SinavKayits).ToListAsync();
            var asd = _autoMapper.Map<List<SinavKayitDto>>(await _db.SinavKayits.ToListAsync());
            return asd;
        }

        public async Task<SinavKayitDto> GetById(int id)
        {
            var sinavKayit = await _db.SinavKayits.SingleOrDefaultAsync(t => t.Id == id);

            if (sinavKayit == null)
                throw new InvalidDataException($"Unable to find SinavKayit with ID: {id}");

            return _autoMapper.Map<SinavKayitDto>(sinavKayit);
        }

        public async Task<SinavKayit> Create(SinavKayitDto sinavKayitDto)
        {
            if (sinavKayitDto.MazeretSinavId.HasValue)
            {
                var mazeretiSinavKayit = await _db.SinavKayits.FirstOrDefaultAsync(x => x.SinavId == sinavKayitDto.MazeretSinavId && x.OgrenciId == sinavKayitDto.OgrenciId);

                sinavKayitDto.MazeretiSinavKayitId = mazeretiSinavKayit.Id;
            }
            var sinavKayit = _autoMapper.Map<SinavKayitDto, SinavKayit>(sinavKayitDto);
            await _db.SinavKayits.AddAsync(sinavKayit);
            await _db.SaveChangesAsync(CancellationToken.None);
            return sinavKayit;

        }

        public async Task<SinavKayit> Update(SinavKayitDto sinavKayitDto)
        {
            var sinavKayit = await _db.SinavKayits.SingleOrDefaultAsync(t => t.Id == sinavKayitDto.Id);
            if (sinavKayit == null)
                throw new InvalidDataException($"Unable to find SinavKayit with ID: {sinavKayitDto.Id}");

            sinavKayit = _autoMapper.Map(sinavKayitDto, sinavKayit);
            _db.SinavKayits.Update(sinavKayit);
            await _db.SaveChangesAsync(CancellationToken.None);

            return sinavKayit;
        }

        public async Task DeleteById(int id)
        {
            var sinavKayit = _db.SinavKayits.SingleOrDefault(t => t.Id == id);

            if (sinavKayit == null)
                throw new InvalidDataException($"Unable to find SinavKayit with ID: {id}");

            _db.SinavKayits.Remove(sinavKayit);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<OgrenciNotlarDto>> GetOgrenciNotlar(int ogrenciId)
        {
            var ogrenciNotlar = from sk in _db.SinavKayits.Where(x => x.OgrenciId == ogrenciId)
                                join s in _db.Sinavs on sk.SinavId equals s.Id
                                join da in _db.DersAcilans on s.DersAcilanId equals da.Id
                                join d in _db.Donems on da.DonemId equals d.Id
                                join st in _db.SinavTips on s.SinavTipId equals st.Id
                                select new OgrenciNotlarDto
                                {
                                    SinavKayitId = sk.Id,
                                    SinavId = sk.SinavId,
                                    SinavTip = st.Ad,
                                    DersKisaAd = da.KisaAd,
                                    DersAd = da.Ad,
                                    DersId = da.Id,
                                    OgrenciId = sk.OgrenciId,
                                    OgrNot = sk.OgrNot,
                                    Donem = d.Ad,
                                    Sinif = da.Sinif
                                };



            return await ogrenciNotlar.ToListAsync();
        }

        public async Task<List<SinavOgrNotlarDto>> GetSinavKayitOgrenciNotlar(int sinavId, int dersAcilanId)
        {
            var derseKayitliOgrlerinTumSinavlari = await (from s in _db.Sinavs.Where(x => x.DersAcilanId == dersAcilanId)
                                                          join st in _db.SinavTips on s.SinavTipId equals st.Id
                                                          join sk in _db.SinavKayits on s.Id equals sk.SinavId
                                                          select new OgrDigerSinavlar
                                                          {
                                                              OgrenciId = sk.OgrenciId,
                                                              SinavTipAd = st.Ad,
                                                              Not = sk.OgrNot
                                                          }).AsNoTracking().ToListAsync();


            var ogrenciNotlar = from sk in _db.SinavKayits.Where(x => x.SinavId == sinavId)
                                join o in _db.Ogrencis on sk.OgrenciId equals o.Id
                                select new SinavOgrNotlarDto
                                {
                                    SinavKayitId = sk.Id,
                                    SinavId = sk.SinavId,
                                    OgrenciId = sk.OgrenciId,
                                    OgrenciAd = o.Ad + " " + o.Soyad,
                                    OgrenciNo = o.OgrNo,
                                    OgrNot = sk.OgrNot,
                                    Katilim = sk.Katilim,
                                    OgrDigerSinavlarText = GetDigerSinavsByText(derseKayitliOgrlerinTumSinavlari, o)
                                };



            return await ogrenciNotlar.AsNoTracking().ToListAsync();
        }

        private static string GetDigerSinavsByText(List<OgrDigerSinavlar> derseKayitliOgrlerinTumSinavlari, Ogrenci o)
        {
            return String.Join(" ", derseKayitliOgrlerinTumSinavlari.Where(y => y.OgrenciId == o.Id).OrderBy(x => x.SinavTipAd).Select(x => x.SinavTipAd + ":" + x.Not));
        }

        public async Task<List<KeyValueDto>> GetOgrenciSinavsByDers(int ogrenciId, int dersAcilanId)
        {
            var ogrenciNotlar = from sk in _db.SinavKayits.Where(x => x.OgrenciId == ogrenciId)
                                join s in _db.Sinavs.Where(x => x.DersAcilanId == dersAcilanId) on sk.SinavId equals s.Id
                                select new KeyValueDto
                                {
                                    Ad = s.Ad,
                                    DoubleValue = sk.OgrNot
                                };



            return await ogrenciNotlar.ToListAsync();
        }

        public async Task<List<SinavSinavKayitDto>> GetSinavKayitsByOgrenciDers(int ogrenciId, int dersAcilanId)
        {
            var ogrenciNotlar = from sk in _db.SinavKayits.Where(x => x.OgrenciId == ogrenciId)
                                join s in _db.Sinavs.Where(x => x.DersAcilanId == dersAcilanId) on sk.SinavId equals s.Id
                                select new SinavSinavKayitDto
                                {
                                    SinavKayitId = sk.Id,
                                    SinavId = s.Id,
                                    SinavAd = s.Ad,
                                    EtkiOran = s.EtkiOran,
                                    OgrNot = sk.OgrNot
                                };



            return await ogrenciNotlar.ToListAsync();
        }

        public async Task UpdateSinavKayit(int sinavkayitId, double orgNot)
        {
            var sk = await _db.SinavKayits.FirstOrDefaultAsync(x => x.Id == sinavkayitId);
            sk.OgrNot = orgNot;
            _db.SinavKayits.Update(sk);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<bool> PutOgrenciSinavKayitNot(OgrenciNotlarDto ogrenciNotlarDto)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {
                var existSinavKayit = await _db.SinavKayits.FirstOrDefaultAsync(x => x.Id == ogrenciNotlarDto.SinavKayitId);

                existSinavKayit.OgrNot = ogrenciNotlarDto.OgrNot;

                _db.SinavKayits.Update(existSinavKayit);

                await _db.SaveChangesAsync(CancellationToken.None);

                var dersSinavList = await (from da in _db.DersAcilans.Where(x => x.Id == ogrenciNotlarDto.DersId)
                                           join dk in _db.DersKayits.Where(x => x.OgrenciId == ogrenciNotlarDto.OgrenciId) on da.Id equals dk.DersAcilanId
                                           join s in _db.Sinavs on da.Id equals s.DersAcilanId
                                           join sk in _db.SinavKayits.Where(x => x.OgrenciId == ogrenciNotlarDto.OgrenciId) on s.Id equals sk.SinavId
                                           select new DersNotHesaplamaDto
                                           {
                                               DersAcilanId = da.Id,
                                               DersKayitId = dk.Id,
                                               OgrNot = sk.OgrNot,
                                               SEtkiOran = s.EtkiOran,
                                               MazeretiSinavId = s.MazeretiSinavId,
                                               MazeretiSinavKayitId = sk.MazeretiSinavKayitId
                                           }).ToListAsync();

                double Ortlama = 0;


                foreach (var dersSinavi in dersSinavList)
                {
                    if (dersSinavList.Any(x => x.MazeretiSinavKayitId == dersSinavi.MazeretiSinavKayitId && x.MazeretiSinavKayitId.HasValue))
                    {

                    }
                    else
                    {
                        Ortlama += dersSinavi.OgrNot * (dersSinavi.SEtkiOran / 100);
                    }
                }
                HesapKitap hesapKitap = new HesapKitap();

                DersNotHarfDto dersNotHarfDto = hesapKitap.MutlakOrtalamaHarflendir(Ortlama);


                var dersKayitExist = await _db.DersKayits.FirstOrDefaultAsync(x => x.DersAcilanId == ogrenciNotlarDto.DersId && x.OgrenciId == ogrenciNotlarDto.OgrenciId);
                dersKayitExist.HarfNot = dersNotHarfDto.harf;
                dersKayitExist.Carpan = dersNotHarfDto.carpan;
                dersKayitExist.Ort = Ortlama;
                _db.DersKayits.Update(dersKayitExist);


                if (await _db.SaveChangesAsync(CancellationToken.None) > 0)
                {
                    context.Commit();
                    return true;
                }
                else
                    throw new DomainException($"Dikkat! Not ve ortlama değişikliği gerçekleştirilemedi ogrenci:{ogrenciNotlarDto.OgrenciId} ders:{ogrenciNotlarDto.SinavId}");

            }
        }

        public async Task<bool> PutAkaOgrenciSinavKayitNot(SinavOgrNotlarDto sinavOgrNotlarDto)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {
                var existSinavKayit = await _db.SinavKayits.FirstOrDefaultAsync(x => x.Id == sinavOgrNotlarDto.SinavKayitId);

                existSinavKayit.OgrNot = sinavOgrNotlarDto.OgrNot;

                _db.SinavKayits.Update(existSinavKayit);

                await _db.SaveChangesAsync(CancellationToken.None);

                var dersSinavList = await (from da in _db.DersAcilans.Where(x => x.Id == sinavOgrNotlarDto.DersId)
                                           join dk in _db.DersKayits.Where(x => x.OgrenciId == sinavOgrNotlarDto.OgrenciId) on da.Id equals dk.DersAcilanId
                                           join s in _db.Sinavs on da.Id equals s.DersAcilanId
                                           join sk in _db.SinavKayits.Where(x => x.OgrenciId == sinavOgrNotlarDto.OgrenciId) on s.Id equals sk.SinavId
                                           select new DersNotHesaplamaDto
                                           {
                                               DersAcilanId = da.Id,
                                               DersKayitId = dk.Id,
                                               OgrNot = sk.OgrNot,
                                               SEtkiOran = s.EtkiOran,
                                               MazeretiSinavId = s.MazeretiSinavId,
                                               MazeretiSinavKayitId = sk.MazeretiSinavKayitId
                                           }).ToListAsync();

                double Ortlama = 0;


                foreach (var dersSinavi in dersSinavList)
                {
                    if (dersSinavList.Any(x => x.MazeretiSinavKayitId == dersSinavi.MazeretiSinavKayitId && x.MazeretiSinavKayitId.HasValue))
                    {

                    }
                    else
                    {
                        Ortlama += dersSinavi.OgrNot * (dersSinavi.SEtkiOran / 100);
                    }
                }
                HesapKitap hesapKitap = new HesapKitap();

                DersNotHarfDto dersNotHarfDto = hesapKitap.MutlakOrtalamaHarflendir(Ortlama);


                var dersKayitExist = await _db.DersKayits.FirstOrDefaultAsync(x => x.DersAcilanId == sinavOgrNotlarDto.DersId && x.OgrenciId == sinavOgrNotlarDto.OgrenciId);
                dersKayitExist.HarfNot = dersNotHarfDto.harf;
                dersKayitExist.Carpan = dersNotHarfDto.carpan;
                dersKayitExist.Ort = Ortlama;
                _db.DersKayits.Update(dersKayitExist);


                if (await _db.SaveChangesAsync(CancellationToken.None) > 0)
                {
                    context.Commit();
                    return true;
                }
                else
                    throw new DomainException($"Dikkat! Not ve ortlama değişikliği gerçekleştirilemedi ogrenci:{sinavOgrNotlarDto.OgrenciId} ders:{sinavOgrNotlarDto.SinavId}");

            }
        }

        public async Task UpdateOgrNotsBatch(List<SinavKayitNotBatch> sinavKayitNotBatches)
        {
            var existSinavKayits = _db.SinavKayits.Where(x => sinavKayitNotBatches.Select(y => y.SinavKayitId).Contains(x.Id));

            foreach (var item in existSinavKayits)
            {
                var enteredSinavKayit = sinavKayitNotBatches.FirstOrDefault(x => x.SinavKayitId == item.Id);
                item.OgrNot = enteredSinavKayit.OgrNot;
                item.Katilim = enteredSinavKayit.Katilim;
            }

            await _db.SaveChangesAsync(CancellationToken.None);
        }


    }
}
