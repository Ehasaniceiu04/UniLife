using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
namespace Semerkand.Storage.Stores
{
    public class DersStore : IDersStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public DersStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<DersDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<DersDto>(_db.Derss).ToListAsync();
            //var asd=  _autoMapper.Map<List<DersDto>>(await _db.Derss.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<DersDto>>(await _db.Derss.ToListAsync());
            return asd;
        }

        public async Task<DersDto> GetById(int id)
        {
            var ders = await _db.Derss.SingleOrDefaultAsync(t => t.Id == id);

            if (ders == null)
                throw new InvalidDataException($"Unable to find Ders with ID: {id}");

            return _autoMapper.Map<DersDto>(ders);
        }

        public async Task<Ders> Create(DersDto dersDto)
        {
            var ders = _autoMapper.Map<DersDto, Ders>(dersDto);
            await _db.Derss.AddAsync(ders);
            await _db.SaveChangesAsync(CancellationToken.None);
            return ders;
        }

        public async Task<Ders> Update(DersDto dersDto)
        {
            var ders = await _db.Derss.SingleOrDefaultAsync(t => t.Id == dersDto.Id);
            if (ders == null)
                throw new InvalidDataException($"Unable to find Ders with ID: {dersDto.Id}");

            ders = _autoMapper.Map(dersDto, ders);
            _db.Derss.Update(ders);
            await _db.SaveChangesAsync(CancellationToken.None);

            return ders;
        }

        public async Task DeleteById(int id)
        {
            var ders = _db.Derss.SingleOrDefault(t => t.Id == id);

            if (ders == null)
                throw new InvalidDataException($"Unable to find Ders with ID: {id}");

            _db.Derss.Remove(ders);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<DersDto>> GetDersByMufredatId(int mufredatId)
        {
            var derss = await _db.Derss.Where(t => t.MufredatId == mufredatId).ToListAsync();

            if (derss == null)
                throw new InvalidDataException($"Unable to find Ders with mufredatId: {mufredatId}");

            return _autoMapper.Map<List<DersDto>>(derss);
        }

        public async Task<List<DersDto>> GetAcilacakDers(DersAcDto dersAcDto)
        {
            var derss = from ders in _db.Derss
                        join m in _db.Mufredats on ders.MufredatId equals m.Id
                        where
                            (dersAcDto.MufredatSecilen.Contains(0) ? dersAcDto.MufredatSecenektekiler.Contains(ders.MufredatId) : dersAcDto.MufredatSecilen.Contains(ders.MufredatId))
                            && (m.Aktif == dersAcDto.IsActive) && (m.Aktif == dersAcDto.IsIntibak)//TODO : intibak ve aktif konusu konuslacak
                            && (dersAcDto.SinifSecilen.Contains(ders.Sinif))
                            && (ders.DonemTipId == dersAcDto.DonemTipSecilen)
                            && (string.IsNullOrWhiteSpace(dersAcDto.DersAd) ? true : dersAcDto.DersAd == ders.Ad)
                            && (string.IsNullOrWhiteSpace(dersAcDto.DersKod) ? true : dersAcDto.DersKod == ders.Kod)
                        select ders;

            return _autoMapper.Map<List<DersDto>>(await derss.ToListAsync()) ;


        }
    }
}
