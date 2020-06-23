using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniLife.Storage.Stores
{
    public class OgrenciStore : BaseStore<Ogrenci, OgrenciDto>, IOgrenciStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public OgrenciStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<OgrenciDto>> GetOgrenciQuery(OgrenciDto ogrenci)
        {
            List<OgrenciDto> result = _autoMapper.Map<List<OgrenciDto>>(await _db.Ogrencis.AsQueryable().WhereIf(!string.IsNullOrEmpty(ogrenci.Soyad), x => x.Soyad.StartsWith(ogrenci.Soyad)).ToListAsync());


            //var ogrenciDto = await _autoMapper.ProjectTo<OgrenciDto>(_db.Ogrencis).FirstOrDefaultAsync(x => x.Id == id); //_db.Ogrencis.SingleOrDefaultAsync(t => t.Id == id);

            if (result == null)
                throw new InvalidDataException($"Unable to find Ogrenci with filters:");

            return result;
        }

        public async Task<OgrenciDto> GetOgrenciWithRelations(int id)
        {

            var ogrenciDetail = await (from o in _db.Ogrencis.Where(x => x.Id == id)
                                           //join ur in _db.Context.UserRoles on o.ApplicationUserId equals ur.UserId
                                           //join r in _db.Context.Roles on ur.RoleId equals r.Id
                                       join f in _db.Fakultes on o.FakulteId equals f.Id
                                       join b in _db.Bolums on o.BolumId equals b.Id
                                       join p in _db.Programs on o.ProgramId equals p.Id
                                       join m in _db.Mufredats on o.MufredatId equals m.Id
                                       select new OgrenciDto
                                       {
                                           Id = o.Id,
                                           Ad = o.Ad,
                                           Soyad = o.Soyad,
                                           FakulteAdi = f.Ad,
                                           BolumAdi = b.Ad,
                                           ProgramAdi = p.Ad,
                                           MufredatAdi = m.Ad,
                                           Email = o.Email,
                                           OgrNo = o.OgrNo,
                                           //Roles = r.Name
                                       }).FirstOrDefaultAsync();


            //var ogrenciDto = await _autoMapper.ProjectTo<OgrenciDto>(_db.Ogrencis).FirstOrDefaultAsync(x => x.Id == id); //_db.Ogrencis.SingleOrDefaultAsync(t => t.Id == id);

            if (ogrenciDetail == null)
                throw new InvalidDataException($"Unable to find Ogrenci with ID: {id}");

            return ogrenciDetail;
        }

        public async Task<List<OgrenciDto>> GetOgrenciListBySinavId(int sinavId)
        {
            var ogrenciList = await (from o in _db.Ogrencis
                                     join sk in _db.SinavKayits.Where(x => x.SinavId == sinavId) on o.Id equals sk.OgrenciId
                                     select new OgrenciDto { 
                                        Ad=o.Ad,
                                        Soyad = o.Soyad,
                                        OgrNo = o.OgrNo,
                                        TCKN = o.TCKN,
                                        SinavKayitId = sk.Id
                                     }).ToListAsync();


            //var ogrenciDto = await _autoMapper.ProjectTo<OgrenciDto>(_db.Ogrencis).FirstOrDefaultAsync(x => x.Id == id); //_db.Ogrencis.SingleOrDefaultAsync(t => t.Id == id);

            if (ogrenciList == null)
                throw new InvalidDataException($"GetOgrenciListBySinavId.. Belirtilen sinav idsi ile ilgili kayıt bulunamadı sinavId: {sinavId}");

            return _autoMapper.Map<List<OgrenciDto>>(ogrenciList);
        }


        public async Task<List<OgrenciDto>> GetOgrenciListByDersAcId(int dersAcId)
        {
            var ogrenciList = await (from o in _db.Ogrencis
                                     join dk in _db.DersKayits.Where(x => x.DersAcilanId == dersAcId) on o.Id equals dk.OgrenciId
                                     select new OgrenciDto
                                     {
                                         Ad = o.Ad,
                                         Soyad = o.Soyad,
                                         OgrNo = o.OgrNo,
                                         TCKN = o.TCKN,
                                         //SinavKayitId = sk.Id
                                     }).ToListAsync();


            //var ogrenciDto = await _autoMapper.ProjectTo<OgrenciDto>(_db.Ogrencis).FirstOrDefaultAsync(x => x.Id == id); //_db.Ogrencis.SingleOrDefaultAsync(t => t.Id == id);

            if (ogrenciList == null)
                throw new InvalidDataException($"GetOgrenciListByDersAcId.. Belirtilen dersAc idsi ile ilgili kayıt bulunamadı dersAcId: {dersAcId}");

            return _autoMapper.Map<List<OgrenciDto>>(ogrenciList);
        }

        


    }
}
