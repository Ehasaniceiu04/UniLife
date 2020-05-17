using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public async Task Cokla(int id)
        {
            //using (var context = (_db as ApplicationDbContext).Database.BeginTransaction())
            //using (var context = _db.Database.BeginTransaction())
            using (var context = _db.Context.Database.BeginTransaction())
            {
                try
                {
                    var mufredat = _db.Mufredats.AsNoTracking().SingleOrDefault(t => t.Id == id);

                    var derss = await _db.Derss.AsNoTracking().Where(t => t.MufredatId == id).ToListAsync();

                    if (mufredat == null)
                        throw new InvalidDataException($"Unable to find Mufredat with ID: {id}");

                    mufredat.Id = 0;
                    _db.Mufredats.Add(mufredat);
                    await _db.SaveChangesAsync(CancellationToken.None);

                    derss.ForEach(x => { x.Id = 0; x.MufredatId = mufredat.Id; });
                    _db.Derss.AddRange(derss);
                    await _db.SaveChangesAsync(CancellationToken.None);

                    context.Commit();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            

        }

        public async Task<List<Mufredat>> GetMufredatByProgramIds(string[] programIds)
        {
            int[] myInts = Array.ConvertAll(programIds, int.Parse);
            List<Mufredat> mufredats;
            if (myInts.Contains(0))
            {
                mufredats = await _db.Mufredats.ToListAsync();
            }
            else
            {
                mufredats = await _db.Mufredats.Where(t => myInts.Contains(t.ProgramId)).ToListAsync();
            }
            

            if (mufredats == null)
                throw new InvalidDataException($"Unable to find Mufredats with IDs:...");

            return _autoMapper.Map<List<Mufredat>>(mufredats);
        }
    }
}
