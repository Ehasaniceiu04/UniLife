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
using System.Linq.Expressions;
using System;

namespace UniLife.Storage.Stores
{
    public class DonemStore : IDonemStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public DonemStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<DonemDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<DonemDto>(_db.Donems).ToListAsync();
            //var asd=  _autoMapper.Map<List<DonemDto>>(await _db.Donems.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<DonemDto>>(await _db.Donems.ToListAsync());
            return asd;
        }

        public async Task<DonemDto> GetById(int id)
        {
            var donem = await _db.Donems.SingleOrDefaultAsync(t => t.Id == id);

            if (donem == null)
                throw new InvalidDataException($"Unable to find Donem with ID: {id}");

            return _autoMapper.Map<DonemDto>(donem);
        }

        public async Task<Donem> Create(DonemDto donemDto)
        {
            var donem = _autoMapper.Map<DonemDto, Donem>(donemDto);
            await _db.Donems.AddAsync(donem);
            await _db.SaveChangesAsync(CancellationToken.None);
            return donem;
        }

        public async Task<Donem> Update(DonemDto donemDto)
        {
            var donem = await _db.Donems.SingleOrDefaultAsync(t => t.Id == donemDto.Id);
            if (donem == null)
                throw new InvalidDataException($"Unable to find Donem with ID: {donemDto.Id}");

            donem = _autoMapper.Map(donemDto, donem);
            _db.Donems.Update(donem);

            await _db.Ogrencis.ForEachAsync(x => x.DnmSnfGecBilgi = "");

            await _db.SaveChangesAsync(CancellationToken.None);

            return donem;
        }

        public async Task DeleteById(int id)
        {
            var donem = _db.Donems.SingleOrDefault(t => t.Id == id);

            if (donem == null)
                throw new InvalidDataException($"Unable to find Donem with ID: {id}");

            _db.Donems.Remove(donem);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<DonemDto>> Current()
        {
            //Bu en son yılı alıyor.
            //var asd = _autoMapper.Map<List<DonemDto>>((await _db.Donems.Where(e => e.Yil == _db.Donems.Max(e2 => (int)e2.Yil)).ToListAsync()));

            var asd = _autoMapper.Map<List<DonemDto>>((await _db.Donems.Where(e => e.Yil == _db.Donems.FirstOrDefault(e2 => e2.Durum==true).Yil).ToListAsync()));
            return asd;
        }

        public async Task<IEnumerable<DonemDto>> GetWhere(Expression<Func<Donem, bool>> predicate)
        {
            var result = await _db.Context.Set<Donem>().Where(predicate).ToListAsync();
            return _autoMapper.Map<IEnumerable<DonemDto>>(result);
        }

        public async Task<Donem> CreateNewDonemWithTakvim(DonemDto donemDto)
        {
            var donem = _autoMapper.Map<DonemDto, Donem>(donemDto);
            var baseAkaTakvim = await _db.AkademikTakvims.Where(x => x.DonemId == 1 && x.FakulteId == null).ToListAsync();

            using (var context = _db.Context.Database.BeginTransaction())
            {
                
                await _db.Donems.AddAsync(donem);
                await _db.SaveChangesAsync(CancellationToken.None);

                baseAkaTakvim.ForEach(x => { x.BasTarih = null; x.BitTarih = null; x.Id=0; x.DonemId = donem.Id; });
                await _db.AkademikTakvims.AddRangeAsync(baseAkaTakvim);
                await _db.SaveChangesAsync(CancellationToken.None);
                context.Commit();
            }
            return donem;
        }
    }
}
