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
            return await _autoMapper.ProjectTo<FakulteDto>(_db.Fakultes).ToListAsync();
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
    }
}
