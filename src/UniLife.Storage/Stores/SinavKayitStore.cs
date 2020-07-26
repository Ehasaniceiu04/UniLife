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
    public class SinavKayitStore : ISinavKayitStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public SinavKayitStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<SinavKayitDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<SinavKayitDto>(_db.SinavKayits).ToListAsync();
            var asd = _autoMapper.Map<List<SinavKayitDto>>(await _db.SinavKayits.ToListAsync());
            return asd;
        }

        public async Task<SinavKayitDto> GetById(int id)
        {
            var sinavKayit = await _db.SinavKayits.SingleOrDefaultAsync(t => t.Id == id);

            if (sinavKayit == null)
                throw new InvalidDataException($"Unable to find SinavKayit with ID: {id}");

            return _autoMapper.Map<SinavKayitDto>(sinavKayit);
        }

        public async Task<SinavKayit> Create(SinavKayitDto sinavKayitDto)
        {
            var sinavKayit = _autoMapper.Map<SinavKayitDto, SinavKayit>(sinavKayitDto);
            await _db.SinavKayits.AddAsync(sinavKayit);
            await _db.SaveChangesAsync(CancellationToken.None);
            return sinavKayit;
        }

        public async Task<SinavKayit> Update(SinavKayitDto sinavKayitDto)
        {
            var sinavKayit = await _db.SinavKayits.SingleOrDefaultAsync(t => t.Id == sinavKayitDto.Id);
            if (sinavKayit == null)
                throw new InvalidDataException($"Unable to find SinavKayit with ID: {sinavKayitDto.Id}");

            sinavKayit = _autoMapper.Map(sinavKayitDto, sinavKayit);
            _db.SinavKayits.Update(sinavKayit);
            await _db.SaveChangesAsync(CancellationToken.None);

            return sinavKayit;
        }

        public async Task DeleteById(int id)
        {
            var sinavKayit = _db.SinavKayits.SingleOrDefault(t => t.Id == id);

            if (sinavKayit == null)
                throw new InvalidDataException($"Unable to find SinavKayit with ID: {id}");

            _db.SinavKayits.Remove(sinavKayit);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<OgrenciNotlarDto>> GetOgrenciNotlar(int ogrenciId)
        {
            var ogrenciNotlar = from sk in _db.SinavKayits.Where(x => x.OgrenciId == ogrenciId)
                                join s in _db.Sinavs on sk.SinavId equals s.Id
                                join da in _db.DersAcilans on s.DersAcilanId equals da.Id
                                join d in _db.Donems on da.DonemId equals d.Id
                                join st in _db.SinavTips on s.SinavTipId equals st.Id
                                select new OgrenciNotlarDto
                                {
                                    SinavKayitId = sk.Id,
                                    SinavId = sk.SinavId,
                                    SinavTip = st.Ad,
                                    DersKisaAd = da.KisaAd,
                                    DersAd = da.Ad,
                                    OgrenciId = sk.OgrenciId,
                                    OgrNot = sk.OgrNot,
                                    Donem =d.Ad,
                                    Sinif = da.Sinif
                                };



            return await ogrenciNotlar.ToListAsync();
        }
    }
}
