using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Semerkand.Storage.Stores
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

        public async Task<OgrenciDto> GetOgrenciWithRelations(int id)
        {

            var ogrenciDetail = await (from o in _db.Ogrencis.Where(x => x.Id == id)
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
                                           OgrNo = o.OgrNo
                                       }).FirstOrDefaultAsync();


            //var ogrenciDto = await _autoMapper.ProjectTo<OgrenciDto>(_db.Ogrencis).FirstOrDefaultAsync(x => x.Id == id); //_db.Ogrencis.SingleOrDefaultAsync(t => t.Id == id);

            if (ogrenciDetail == null)
                throw new InvalidDataException($"Unable to find Ogrenci with ID: {id}");

            return ogrenciDetail;
        }
    }
}
