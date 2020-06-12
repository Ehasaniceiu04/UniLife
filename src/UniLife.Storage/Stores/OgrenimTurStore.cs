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

namespace UniLife.Storage.Stores
{
    public class OgrenimTurStore : IOgrenimTurStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public OgrenimTurStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }       

        public async Task<List<OgrenimTurDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<OgrenimTurDto>(_db.OgrenimTurs).ToListAsync();
            var asd = _autoMapper.Map<List<OgrenimTurDto>>(await _db.OgrenimTurs.ToListAsync());
            return asd;
        }

        public async Task<OgrenimTurDto> GetById(int id)
        {
            var ogrenimTur = await _db.OgrenimTurs.SingleOrDefaultAsync(t => t.Id == id);

            if (ogrenimTur == null)
                throw new InvalidDataException($"Unable to find OgrenimTur with ID: {id}");

            return _autoMapper.Map<OgrenimTurDto>(ogrenimTur);
        }

        public async Task<OgrenimTur> Create(OgrenimTurDto ogrenimTurDto)
        {
            var ogrenimTur = _autoMapper.Map<OgrenimTurDto, OgrenimTur>(ogrenimTurDto);
            await _db.OgrenimTurs.AddAsync(ogrenimTur);
            await _db.SaveChangesAsync(CancellationToken.None);
            return ogrenimTur;
        }

        public async Task<OgrenimTur> Update(OgrenimTurDto ogrenimTurDto)
        {
            var ogrenimTur = await _db.OgrenimTurs.SingleOrDefaultAsync(t => t.Id == ogrenimTurDto.Id);
            if (ogrenimTur == null)
                throw new InvalidDataException($"Unable to find OgrenimTur with ID: {ogrenimTurDto.Id}");

            ogrenimTur = _autoMapper.Map(ogrenimTurDto, ogrenimTur);
            _db.OgrenimTurs.Update(ogrenimTur);
            await _db.SaveChangesAsync(CancellationToken.None);

            return ogrenimTur;
        }

        public async Task DeleteById(int id)
        {
            var ogrenimTur = _db.OgrenimTurs.SingleOrDefault(t => t.Id == id);

            if (ogrenimTur == null)
                throw new InvalidDataException($"Unable to find OgrenimTur with ID: {id}");

            _db.OgrenimTurs.Remove(ogrenimTur);
            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
