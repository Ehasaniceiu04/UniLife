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
    public class FakulteTurStore : IFakulteTurStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public FakulteTurStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }       

        public async Task<List<FakulteTurDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<FakulteTurDto>(_db.FakulteTurs).ToListAsync();
            var asd = _autoMapper.Map<List<FakulteTurDto>>(await _db.FakulteTurs.ToListAsync());
            return asd;
        }

        public async Task<FakulteTurDto> GetById(int id)
        {
            var fakulteTur = await _db.FakulteTurs.SingleOrDefaultAsync(t => t.Id == id);

            if (fakulteTur == null)
                throw new InvalidDataException($"Unable to find FakulteTur with ID: {id}");

            return _autoMapper.Map<FakulteTurDto>(fakulteTur);
        }

        public async Task<FakulteTur> Create(FakulteTurDto fakulteTurDto)
        {
            var fakulteTur = _autoMapper.Map<FakulteTurDto, FakulteTur>(fakulteTurDto);
            await _db.FakulteTurs.AddAsync(fakulteTur);
            await _db.SaveChangesAsync(CancellationToken.None);
            return fakulteTur;
        }

        public async Task<FakulteTur> Update(FakulteTurDto fakulteTurDto)
        {
            var fakulteTur = await _db.FakulteTurs.SingleOrDefaultAsync(t => t.Id == fakulteTurDto.Id);
            if (fakulteTur == null)
                throw new InvalidDataException($"Unable to find FakulteTur with ID: {fakulteTurDto.Id}");

            fakulteTur = _autoMapper.Map(fakulteTurDto, fakulteTur);
            _db.FakulteTurs.Update(fakulteTur);
            await _db.SaveChangesAsync(CancellationToken.None);

            return fakulteTur;
        }

        public async Task DeleteById(int id)
        {
            var fakulteTur = _db.FakulteTurs.SingleOrDefault(t => t.Id == id);

            if (fakulteTur == null)
                throw new InvalidDataException($"Unable to find FakulteTur with ID: {id}");

            _db.FakulteTurs.Remove(fakulteTur);
            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
