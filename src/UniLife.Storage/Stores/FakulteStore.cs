using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UniLife.Storage.Stores
{
    public class FakulteStore : IFakulteStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public FakulteStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<FakulteDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<FakulteDto>(_db.Fakultes).ToListAsync();
            //var asd=  _autoMapper.Map<List<FakulteDto>>(await _db.Fakultes.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<FakulteDto>>(await _db.Fakultes.ToListAsync());
            return asd;
        }

        public async Task<FakulteDto> GetById(int id)
        {
            var fakulte = await _db.Fakultes.SingleOrDefaultAsync(t => t.Id == id);

            if (fakulte == null)
                throw new InvalidDataException($"Unable to find Fakulte with ID: {id}");

            return _autoMapper.Map<FakulteDto>(fakulte);
        }

        public async Task<Fakulte> Create(FakulteDto fakulteDto)
        {
            var fakulte = _autoMapper.Map<FakulteDto, Fakulte>(fakulteDto);
            await _db.Fakultes.AddAsync(fakulte);
            await _db.SaveChangesAsync(CancellationToken.None);
            return fakulte;
        }

        public async Task<Fakulte> Update(FakulteDto fakulteDto)
        {
            var fakulte = await _db.Fakultes.SingleOrDefaultAsync(t => t.Id == fakulteDto.Id);
            if (fakulte == null)
                throw new InvalidDataException($"Unable to find Fakulte with ID: {fakulteDto.Id}");

            fakulte = _autoMapper.Map(fakulteDto, fakulte);
            _db.Fakultes.Update(fakulte);
            await _db.SaveChangesAsync(CancellationToken.None);

            return fakulte;
        }

        public async Task DeleteById(int id)
        {
            var fakulte = _db.Fakultes.SingleOrDefault(t => t.Id == id);

            if (fakulte == null)
                throw new InvalidDataException($"Unable to find Fakulte with ID: {id}");

            _db.Fakultes.Remove(fakulte);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<ChartData>> GetOgrCountOfFakultesGYear()
        {
            var result = await (from f in _db.Fakultes
                                join o in _db.Ogrencis on f.Id equals o.FakulteId
                                group o by new { f.Ad, o.KayitTarih.Year }
                                into grp
                                select new ChartData
                                {
                                    FakulteAd = grp.Key.Ad,
                                    Yil = grp.Key.Year.ToString(),
                                    Miktar = grp.Count()
                                }).ToListAsync();

            return result;

        }
    }
}
