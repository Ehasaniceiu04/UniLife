using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Semerkand.Storage.Stores
{
    public class ProgramStore : IProgramStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public ProgramStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<ProgramDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<ProgramDto>(_db.Programs).ToListAsync();
            //var asd=  _autoMapper.Map<List<ProgramDto>>(await _db.Programs.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<ProgramDto>>(await _db.Programs.ToListAsync());
            return asd;
        }

        public async Task<ProgramDto> GetById(int id)
        {
            var program = await _db.Programs.SingleOrDefaultAsync(t => t.Id == id);

            if (program == null)
                throw new InvalidDataException($"Unable to find Program with ID: {id}");

            return _autoMapper.Map<ProgramDto>(program);
        }

        public async Task<Program> Create(ProgramDto programDto)
        {
            var program = _autoMapper.Map<ProgramDto, Program>(programDto);
            await _db.Programs.AddAsync(program);
            await _db.SaveChangesAsync(CancellationToken.None);
            return program;
        }

        public async Task<Program> Update(ProgramDto programDto)
        {
            var program = await _db.Programs.SingleOrDefaultAsync(t => t.Id == programDto.Id);
            if (program == null)
                throw new InvalidDataException($"Unable to find Program with ID: {programDto.Id}");

            program = _autoMapper.Map(programDto, program);
            _db.Programs.Update(program);
            await _db.SaveChangesAsync(CancellationToken.None);

            return program;
        }

        public async Task DeleteById(int id)
        {
            var program = _db.Programs.SingleOrDefault(t => t.Id == id);

            if (program == null)
                throw new InvalidDataException($"Unable to find Program with ID: {id}");

            _db.Programs.Remove(program);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<Program>> GetProgramByBolumIds(string[] bolumIds)
        {
            int[] myInts = Array.ConvertAll(bolumIds, int.Parse);

            List<Program> programs;
            if (myInts.Contains(55555))
            {
                programs = await _db.Programs.ToListAsync();
            }
            else
            {
                programs = await _db.Programs.Where(t => myInts.Contains(t.BolumId)).ToListAsync();
            }

            if (programs == null)
                throw new InvalidDataException($"Unable to find Program with IDs:...");

            return _autoMapper.Map<List<Program>>(programs);
        }
    }
}
