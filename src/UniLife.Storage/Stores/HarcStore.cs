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
    public class HarcStore : IHarcStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;


        public HarcStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<HarcDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<HarcDto>(_db.Harcs).ToListAsync();
            //var asd=  _autoMapper.Map<List<HarcDto>>(await _db.Harcs.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<HarcDto>>(await _db.Harcs.ToListAsync());
            return asd;
        }

        public async Task<HarcDto> GetById(int id)
        {
            var harc = await _db.Harcs.SingleOrDefaultAsync(t => t.Id == id);

            if (harc == null)
                throw new InvalidDataException($"Unable to find Harc with ID: {id}");

            return _autoMapper.Map<HarcDto>(harc);
        }

        public async Task<Harc> Create(HarcDto harcDto)
        {
            var harc = _autoMapper.Map<HarcDto, Harc>(harcDto);
            await _db.Harcs.AddAsync(harc);
            await _db.SaveChangesAsync(CancellationToken.None);
            return harc;
        }

        public async Task<Harc> Update(HarcDto harcDto)
        {
            var harc = await _db.Harcs.SingleOrDefaultAsync(t => t.Id == harcDto.Id);
            if (harc == null)
                throw new InvalidDataException($"Unable to find Harc with ID: {harcDto.Id}");

            harc = _autoMapper.Map(harcDto, harc);
            _db.Harcs.Update(harc);
            await _db.SaveChangesAsync(CancellationToken.None);

            return harc;
        }

        public async Task DeleteById(int id)
        {
            var harc = _db.Harcs.SingleOrDefault(t => t.Id == id);

            if (harc == null)
                throw new InvalidDataException($"Unable to find Harc with ID: {id}");

            _db.Harcs.Remove(harc);
            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
