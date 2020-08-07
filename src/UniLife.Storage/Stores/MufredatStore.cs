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
            var mufredat = await _db.Mufredats.SingleOrDefaultAsync(t => t.Id == mufredatDto.Id);
            if (mufredat == null)
                throw new InvalidDataException($"Müfredat bulunamadı: {mufredatDto.Id}");

            mufredat = _autoMapper.Map(mufredatDto, mufredat);
            _db.Mufredats.Update(mufredat);
            await _db.SaveChangesAsync(CancellationToken.None);

            return mufredat;
        }

        public async Task DeleteById(int id)
        {
            var mufredat = _db.Mufredats.SingleOrDefault(t => t.Id == id);

            if (mufredat == null)
                throw new InvalidDataException($"Unable to find Mufredat with ID: {id}");

            _db.Mufredats.Remove(mufredat);
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
                                    ProgramId =p.Id,
                                    ProgramAd =p.Ad,
                                    BolumId =b.Id,
                                    BolumAd =b.Ad,
                                    FakulteId=f.Id,
                                    FakulteAd =f.Ad
                                };
            return await mufredatState.FirstOrDefaultAsync();
        }

        public async Task CreateDersAcilansByMufredatIds(ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds)
        {
            var mufredatDerss = await _db.Derss.Where(x => reqEntityIdWithOtherEntitiesIds.EntityId == x.DonemId && reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.MufredatId)).ToListAsync();


            //RAW delete hard yapıyor.. tehlikeli.
            //string queryIncludeIds = "";
            //foreach (var item in mufredatDerss.Select(x=>x.Id))
            //{
            //    queryIncludeIds = queryIncludeIds + item.ToString() + ",";
            //}
            //queryIncludeIds = queryIncludeIds.TrimEnd(',');

            //var rawBulkDeleteQuery = $"delete from public.'DersAcilans' where 'DersId' in ({queryIncludeIds})";

            //int numberOfRowAffected = await _db.Database.ExecuteSqlCommandAsync(rawBulkDeleteQuery.Replace('\'', '"'));

            var dumpDersAcilans = _db.DersAcilans.Where(x => reqEntityIdWithOtherEntitiesIds.EntityId == x.DonemId && reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.MufredatId)).ToList(); //bunlar yeniden geldiği için silinecek.


            List<DersAcilan> dersAcilans = new List<DersAcilan>();
            foreach (var i in mufredatDerss)
            {
                DersAcilan dersAcilan = new DersAcilan
                {
                    Ad = i.Ad,
                    AdEn = i.AdEn,
                    Akts = i.Akts,
                    BolumId = i.BolumId,
                    DersId = i.Id,
                    KisaAd = i.KisaAd,
                    Kod = i.Kod,
                    Kredi = i.Kredi,
                    LabSaat = i.LabSaat,
                    MufredatId = i.MufredatId,
                    OptikKod = i.OptikKod,
                    ProgramId = i.ProgramId,
                    SecmeliKodu = i.SecmeliKodu,
                    Sinif = i.Sinif,
                    DonemId = i.DonemId,
                    Durum = i.Durum,
                    FakulteId = i.FakulteId,
                    TeoSaat = i.TeoSaat,
                    UygSaat = i.UygSaat,
                    Zorunlu = i.Zorunlu
                };
                dersAcilans.Add(dersAcilan);

                i.Durum = true; // Dersleri açınca durumlarını aktif ediyoruz. Bir başka ifadeyle, "Ders"ler "DersAcilan"a aktarıldığında "Ders" aktif oluyor.
            }

            _db.DersAcilans.RemoveRange(dumpDersAcilans); //aynı dersacilan varsa sil
            _db.Derss.UpdateRange(mufredatDerss); // açilan dersler in durumunu 1 yap.


            var aktifMufredats = await _db.Mufredats.Where(x=> reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Contains(x.Id)).ToListAsync();
            aktifMufredats.ForEach(x => { x.Durum = 1;});  //açılan derslerin bağlı oldugu müfredatların durumu 1 yap.

            await _db.DersAcilans.AddRangeAsync(dersAcilans); //açılan dersleri ekle


            _db.Mufredats.UpdateRange(aktifMufredats); //1 leri kaydet.

            await _db.SaveChangesAsync(CancellationToken.None);

                
        }
    }
}
