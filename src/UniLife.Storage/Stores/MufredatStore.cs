using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using UniLife.Shared;

namespace UniLife.Storage.Stores
{
    public class MufredatStore : IMufredatStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public MufredatStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<MufredatDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<MufredatDto>(_db.Mufredats).ToListAsync();
            //var asd=  _autoMapper.Map<List<MufredatDto>>(await _db.Mufredats.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<MufredatDto>>(await _db.Mufredats.ToListAsync());
            return asd;
        }

        public async Task<MufredatDto> GetById(int id)
        {
            var mufredat = await _db.Mufredats.SingleOrDefaultAsync(t => t.Id == id);

            if (mufredat == null)
                throw new InvalidDataException($"Unable to find Mufredat with ID: {id}");

            return _autoMapper.Map<MufredatDto>(mufredat);
        }

        public async Task<Mufredat> Create(MufredatDto mufredatDto)
        {
            var mufredat = _autoMapper.Map<MufredatDto, Mufredat>(mufredatDto);
            await _db.Mufredats.AddAsync(mufredat);
            await _db.SaveChangesAsync(CancellationToken.None);
            return mufredat;
        }

        public async Task<Mufredat> Update(MufredatDto mufredatDto)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {

                var mufredat = await _db.Mufredats.SingleOrDefaultAsync(t => t.Id == mufredatDto.Id);
                if (mufredat == null)
                    throw new InvalidDataException($"Müfredat bulunamadı: {mufredatDto.Id}");



                mufredat = _autoMapper.Map(mufredatDto, mufredat);
                _db.Mufredats.Update(mufredat);
                await _db.SaveChangesAsync(CancellationToken.None);

                var aktifMufredats = await _db.Mufredats.Where(t => t.Aktif == true && mufredatDto.ProgramId == t.ProgramId).ToListAsync();
                if (aktifMufredats.Count > 1)
                {
                    throw new DomainException($"Dikkat! Bir program içinde birden fazla aktif müfredat olamaz");
                }

                context.Commit();
                return mufredat;
            }
        }

        public async Task DeleteById(int id)
        {
            var mufredat = _db.Mufredats.SingleOrDefault(t => t.Id == id);

            if (mufredat == null)
                throw new InvalidDataException($"Unable to find Mufredat with ID: {id}");

            _db.Mufredats.Remove(mufredat);

            _db.Derss.RemoveRange(_db.Derss.Where(x => x.MufredatId == mufredat.Id));


            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task Cokla(int id)
        {
            //using (var context = (_db as ApplicationDbContext).Database.BeginTransaction())
            //using (var context = _db.Database.BeginTransaction())
            using (var context = _db.Context.Database.BeginTransaction())
            {
                try
                {
                    var mufredat = _db.Mufredats.AsNoTracking().SingleOrDefault(t => t.Id == id);

                    var derss = await _db.Derss.AsNoTracking().Where(t => t.MufredatId == id).ToListAsync();

                    if (mufredat == null)
                        throw new InvalidDataException($"Unable to find Mufredat with ID: {id}");

                    mufredat.Id = 0;
                    _db.Mufredats.Add(mufredat);
                    await _db.SaveChangesAsync(CancellationToken.None);

                    derss.ForEach(x => { x.Id = 0; x.MufredatId = mufredat.Id; });
                    _db.Derss.AddRange(derss);
                    await _db.SaveChangesAsync(CancellationToken.None);

                    context.Commit();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }


        }

        public async Task<List<Mufredat>> GetMufredatByProgramIds(string[] programIds)
        {
            int[] myInts = Array.ConvertAll(programIds, int.Parse);
            List<Mufredat> mufredats;
            if (myInts.Contains(55555))
            {
                mufredats = await _db.Mufredats.ToListAsync();
            }
            else
            {
                mufredats = await _db.Mufredats.Where(t => myInts.Contains(t.ProgramId)).ToListAsync();
            }

            if (mufredats == null)
                throw new InvalidDataException($"Unable to find Mufredats with IDs:...");

            return _autoMapper.Map<List<Mufredat>>(mufredats);
        }

        public async Task<MufredatStateDto> GetMufredatState(int mufredatId)
        {
            var mufredatState = from m in _db.Mufredats.Where(x => x.Id == mufredatId)
                                join p in _db.Programs on m.ProgramId equals p.Id
                                join b in _db.Bolums on p.BolumId equals b.Id
                                join f in _db.Fakultes on b.FakulteId equals f.Id
                                select new MufredatStateDto
                                {
                                    MufredatId = m.Id,
                                    MufredatAd = m.Ad,
                                    ProgramId = p.Id,
                                    ProgramAd = p.Ad,
                                    BolumId = b.Id,
                                    BolumAd = b.Ad,
                                    FakulteId = f.Id,
                                    FakulteAd = f.Ad
                                };
            return await mufredatState.FirstOrDefaultAsync();
        }

        public async Task CreateDersAcilansByMufredatIds(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            var mufredatDerss = await _db.Derss.Where(x => reqEntityIdWithOtherEntitiesIds.EntityId == x.DonemTipId && reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.MufredatId) && x.Durum == true).ToListAsync();

            #region RAWDEL
            //RAW delete hard yapıyor.. tehlikeli.
            //string queryIncludeIds = "";
            //foreach (var item in mufredatDerss.Select(x=>x.Id))
            //{
            //    queryIncludeIds = queryIncludeIds + item.ToString() + ",";
            //}
            //queryIncludeIds = queryIncludeIds.TrimEnd(',');

            //var rawBulkDeleteQuery = $"delete from public.'DersAcilans' where 'DersId' in ({queryIncludeIds})";

            //int numberOfRowAffected = await _db.Database.ExecuteSqlCommandAsync(rawBulkDeleteQuery.Replace('\'', '"'));
            #endregion RAWDEL

            




            var aktifDonem = await _db.Donems.FirstOrDefaultAsync(x => x.Durum == true);

            var dumpDersAcilans = _db.DersAcilans.Where(x => aktifDonem.Id == x.DonemId && reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.MufredatId)).ToList(); //bunlar yeniden geldiği için silinecek.
            List<DersAcilan> dersAcilans = new List<DersAcilan>();



            //foreach (var i in mufredatDerss)
            //{
            //    DersAcilan dersAcilan = new DersAcilan
            //    {
            //        Ad = i.Ad,
            //        AdEn = i.AdEn,
            //        Akts = i.Akts,
            //        BolumId = i.BolumId,
            //        DersId = i.Id,
            //        KisaAd = i.KisaAd,
            //        Kod = i.Kod,
            //        Kredi = i.Kredi,
            //        LabSaat = i.LabSaat,
            //        MufredatId = i.MufredatId,
            //        OptikKod = i.OptikKod,
            //        ProgramId = i.ProgramId,
            //        SecmeliKodu = i.SecmeliKodu,
            //        Sinif = i.Sinif,
            //        DersNedenId = i.DersNedenId,
            //        DersDilId = i.DersDilId,
            //        DonemId = aktifDonem.Id, // Todo : burada sadece aktif döneme açmak yerine başka dönemlere açma seçeneği istenebilir.
            //        //Durum = i.Durum, //ders açılandan bu alanın henuz bir anlamı yok.
            //        FakulteId = i.FakulteId,
            //        TeoSaat = i.TeoSaat,
            //        UygSaat = i.UygSaat,
            //        Zorunlu = i.Zorunlu,
            //        //EskiMufBagliDersId = i.EskiMufBagliDersId
            //    };
            //    dersAcilans.Add(dersAcilan);

            //    //i.AktifDonemdeAcik = true; // "Ders"ler "DersAcilan"a aktarıldığında "Ders" yeşilleniyor oluyor. Bir sonki dönem bu sıfırlanır.
            //}




            var OpeningMufredatList = await _db.Mufredats.Where(x => reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.Id))
                .Include(y => y.Derss.Where(z => reqEntityIdWithOtherEntitiesIds.EntityId == z.DonemTipId && z.Durum == true))
                .ToListAsync();

            foreach (var mufredat in OpeningMufredatList)
            {
                if (mufredat.Aktif)
                {
                    foreach (var ders in mufredat.Derss)
                    {
                        DersAcilan dersAcilan = new DersAcilan
                        {
                            Ad = ders.Ad,
                            AdEn = ders.AdEn,
                            Akts = ders.Akts,
                            BolumId = ders.BolumId,
                            DersId = ders.Id,
                            KisaAd = ders.KisaAd,
                            Kod = ders.Kod,
                            Kredi = ders.Kredi,
                            LabSaat = ders.LabSaat,
                            MufredatId = ders.MufredatId,
                            OptikKod = ders.OptikKod,
                            ProgramId = ders.ProgramId,
                            SecmeliKodu = ders.SecmeliKodu,
                            Sinif = ders.Sinif,
                            DersNedenId = ders.DersNedenId,
                            DersDilId = ders.DersDilId,
                            DonemId = aktifDonem.Id, // Todo : burada sadece aktif döneme açmak yerine başka dönemlere açma seçeneği istenebilir.
                                                     //Durum = ders.Durum, //ders açılandan bu alanın henuz bir anlamı yok.
                            FakulteId = ders.FakulteId,
                            TeoSaat = ders.TeoSaat,
                            UygSaat = ders.UygSaat,
                            Zorunlu = ders.Zorunlu,
                            //EskiMufBagliDersId = ders.EskiMufBagliDersId
                        };
                        dersAcilans.Add(dersAcilan);
                    }
                }
                else
                {
                    //Pasif mufredatın derslerini açma

                    var beraberSeçilenAktifMufredat = OpeningMufredatList.FirstOrDefault(x => x.Aktif = true && x.ProgramId == mufredat.ProgramId);

                    var acilmisAktifMufredatDersKodlari = _db.DersAcilans.Where(x => x.DonemId == aktifDonem.Id && x.ProgramId == mufredat.ProgramId).Select(x => x.Kod);

                    foreach (var ders in mufredat.Derss)
                    {
                        if (!beraberSeçilenAktifMufredat.Derss.Any(x => x.Kod == ders.Kod) && !acilmisAktifMufredatDersKodlari.Any(x => x == ders.Kod))
                        {
                            DersAcilan dersAcilan = new DersAcilan
                            {
                                Ad = ders.Ad,
                                AdEn = ders.AdEn,
                                Akts = ders.Akts,
                                BolumId = ders.BolumId,
                                DersId = ders.Id,
                                KisaAd = ders.KisaAd,
                                Kod = ders.Kod,
                                Kredi = ders.Kredi,
                                LabSaat = ders.LabSaat,
                                MufredatId = ders.MufredatId,
                                OptikKod = ders.OptikKod,
                                ProgramId = ders.ProgramId,
                                SecmeliKodu = ders.SecmeliKodu,
                                Sinif = ders.Sinif,
                                DersNedenId = ders.DersNedenId,
                                DersDilId = ders.DersDilId,
                                DonemId = aktifDonem.Id, // Todo : burada sadece aktif döneme açmak yerine başka dönemlere açma seçeneği istenebilir.
                                                         //Durum = ders.Durum, //ders açılandan bu alanın henuz bir anlamı yok.
                                FakulteId = ders.FakulteId,
                                TeoSaat = ders.TeoSaat,
                                UygSaat = ders.UygSaat,
                                Zorunlu = ders.Zorunlu,
                                //EskiMufBagliDersId = ders.EskiMufBagliDersId
                            };
                            dersAcilans.Add(dersAcilan);
                        }
                    }
                }
            }





            _db.DersAcilans.RemoveRange(dumpDersAcilans); //aynı dersacilan varsa sil
            //_db.Derss.UpdateRange(mufredatDerss); // açilan dersler in durumunu 1 yap.


            //var aktifMufredats = await _db.Mufredats.Where(x=> reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.Id)).ToListAsync();
            //aktifMufredats.ForEach(x => { x.Durum = 1;});  //açılan derslerin bağlı oldugu müfredatların durumu 1 yap.

            await _db.DersAcilans.AddRangeAsync(dersAcilans); //açılan dersleri ekle


            //_db.Mufredats.UpdateRange(aktifMufredats); //1 leri kaydet.

            await _db.SaveChangesAsync(CancellationToken.None);






            

        }

        public async Task<MufredatDto> GetLastMufredatByProgramId(int programId)
        {
            ////Eğer son eklene müfredatsa bu
            //var aktifMufredat= await _db.Mufredats.Where(x => x.ProgramId == programId).OrderByDescending(y => y.Yil).FirstOrDefaultAsync();

            var aktifMufredat = await _db.Mufredats.FirstOrDefaultAsync(x => x.ProgramId == programId && x.Durum == 1);

            return _autoMapper.Map<Mufredat, MufredatDto>(aktifMufredat);
        }

        public async Task CoklaModified(MufredatDto mufredatDto)
        {
            using (var context = _db.Context.Database.BeginTransaction())
            {
                try
                {
                    var program = await _db.Programs.FirstOrDefaultAsync(x => x.Id == mufredatDto.ProgramId);
                    var derss = await _db.Derss.AsNoTracking().Where(t => t.MufredatId == mufredatDto.Id).ToListAsync();

                    mufredatDto.Id = 0;
                    var temizMuf = _autoMapper.Map<Mufredat>(mufredatDto);
                    _db.Mufredats.Add(temizMuf);
                    await _db.SaveChangesAsync(CancellationToken.None);
                    //derss.ForEach(x => x.MufredatId = mufredatDto.Id);
                    derss.ForEach(x =>
                    {
                        x.Id = 0;
                        x.MufredatId = temizMuf.Id;
                        x.ProgramId = program.Id;
                        x.BolumId = program.BolumId;
                        x.FakulteId = program.FakulteId;
                    });
                    _db.Derss.AddRange(derss);
                    await _db.SaveChangesAsync(CancellationToken.None);

                    context.Commit();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }
    }
}
