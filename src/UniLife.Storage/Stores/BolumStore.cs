using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace UniLife.Storage.Stores
{
    public class BolumStore : IBolumStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public BolumStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<BolumDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<BolumDto>(_db.Bolums).ToListAsync();
            //var asd=  _autoMapper.Map<List<BolumDto>>(await _db.Bolums.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<BolumDto>>(await _db.Bolums.ToListAsync());
            return asd;
        }

        public async Task<BolumDto> GetById(int id)
        {
            var bolum = await _db.Bolums.SingleOrDefaultAsync(t => t.Id == id);

            if (bolum == null)
                throw new InvalidDataException($"Unable to find Bolum with ID: {id}");

            return _autoMapper.Map<BolumDto>(bolum);
        }

        public async Task<Bolum> Create(BolumDto bolumDto)
        {
            var bolum = _autoMapper.Map<BolumDto, Bolum>(bolumDto);
            await _db.Bolums.AddAsync(bolum);
            await _db.SaveChangesAsync(CancellationToken.None);
            return bolum;
        }

        public async Task<Bolum> Update(BolumDto bolumDto)
        {
            var bolum = await _db.Bolums.SingleOrDefaultAsync(t => t.Id == bolumDto.Id);
            if (bolum == null)
                throw new InvalidDataException($"Unable to find Bolum with ID: {bolumDto.Id}");

            bolum = _autoMapper.Map(bolumDto, bolum);
            _db.Bolums.Update(bolum);
            await _db.SaveChangesAsync(CancellationToken.None);

            return bolum;
        }

        public async Task DeleteById(int id)
        {
            var bolum = _db.Bolums.SingleOrDefault(t => t.Id == id);

            if (bolum == null)
                throw new InvalidDataException($"Unable to find Bolum with ID: {id}");

            _db.Bolums.Remove(bolum);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<BolumDto>> GetBolumByFakulteId(string[] fakulteIds)
        {
            int[] myInts = Array.ConvertAll(fakulteIds, int.Parse);

            List<Bolum> bolums;
            if (myInts.Contains(55555))
            {
                bolums = await _db.Bolums.ToListAsync();
            }
            else
            {
                bolums = await _db.Bolums.Where(t => myInts.Contains(t.FakulteId)).ToListAsync();
            }            

            if (bolums == null)
                throw new InvalidDataException($"Unable to find Bolums with IDs:...");

            return _autoMapper.Map<List<BolumDto>>(bolums);
        }

        public IQueryable<Bolum> GetAllQueryable()
        {
            return _db.Bolums.AsQueryable();
        }
    }
}
