using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Semerkand.Storage.Stores
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
                throw new InvalidDataException($"Unable to find Mufredat with ID: {mufredatDto.Id}");

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
    }
}
