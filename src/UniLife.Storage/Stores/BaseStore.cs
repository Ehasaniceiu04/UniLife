using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace UniLife.Storage.Stores
{
    public class BaseStore<T,TDto> : IBaseStore<T, TDto> where T: Entity<int>, new() 
                                                         where TDto : EntityDto<int>, new()
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public BaseStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<TDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<BolumDto>(_db.Bolums).ToListAsync();
            //var asd=  _autoMapper.Map<List<BolumDto>>(await _db.Bolums.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<TDto>>(await _db.Context.Set<T>().ToListAsync());
            return asd;
        }

        public async Task<TDto> GetById(int id)
        {
            var t = await _db.Context.Set<T>().SingleOrDefaultAsync(t => t.Id == id);

            if (t == null)
                throw new InvalidDataException($"Unable to find T with ID: {id}");

            return _autoMapper.Map<TDto>(t);
        }

        public async Task<T> Create(TDto tDto)
        {
            var t = _autoMapper.Map<TDto, T>(tDto);
            await _db.Context.Set<T>().AddAsync(t);
            _db.Context.Entry<T>(t).State = EntityState.Added;
            await _db.SaveChangesAsync(CancellationToken.None);
            return t;
        }

        public async Task<T> Update(TDto tDto)
        {
            var t = await _db.Context.Set<T>().SingleOrDefaultAsync(t => t.Id == tDto.Id);
            if (t == null)
                throw new InvalidDataException($"Unable to find T with ID: {tDto.Id}");

            t = _autoMapper.Map(tDto, t);
            _db.Context.Set<T>().Update(t);
            _db.Context.Entry<T>(t).State= EntityState.Modified;
            await _db.SaveChangesAsync(CancellationToken.None);

            return t;
        }

        public async Task DeleteById(int id)
        {
            var t = _db.Context.Set<T>().SingleOrDefault(t => t.Id == id);

            if (t == null)
                throw new InvalidDataException($"Unable to find T with ID: {id}");

            _db.Context.Set<T>().Remove(t);
            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
