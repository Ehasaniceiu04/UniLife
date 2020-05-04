using AutoMapper;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Sample;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.Storage.Stores
{
    public class UniversiteStore : IUniversiteStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public UniversiteStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }       

        public async Task<List<UniversiteDto>> GetAll()
        {
            return await _autoMapper.ProjectTo<UniversiteDto>(_db.Universites).ToListAsync();
        }

        public async Task<UniversiteDto> GetById(int id)
        {
            var universite = await _db.Universites.SingleOrDefaultAsync(t => t.Id == id);

            if (universite == null)
                throw new InvalidDataException($"Unable to find Universite with ID: {id}");

            return _autoMapper.Map<UniversiteDto>(universite);
        }

        public async Task<Universite> Create(UniversiteDto universiteDto)
        {
            var universite = _autoMapper.Map<UniversiteDto, Universite>(universiteDto);
            await _db.Universites.AddAsync(universite);
            await _db.SaveChangesAsync(CancellationToken.None);
            return universite;
        }

        public async Task<Universite> Update(UniversiteDto universiteDto)
        {
            var universite = await _db.Universites.SingleOrDefaultAsync(t => t.Id == universiteDto.Id);
            if (universite == null)
                throw new InvalidDataException($"Unable to find Universite with ID: {universiteDto.Id}");

            universite = _autoMapper.Map(universiteDto, universite);
            _db.Universites.Update(universite);
            await _db.SaveChangesAsync(CancellationToken.None);

            return universite;
        }

        public async Task DeleteById(int id)
        {
            var universite = _db.Universites.SingleOrDefault(t => t.Id == id);

            if (universite == null)
                throw new InvalidDataException($"Unable to find Universite with ID: {id}");

            _db.Universites.Remove(universite);
            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
