using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using SQLitePCL;
using System.Security.Cryptography.X509Certificates;

namespace UniLife.Storage.Stores
{
    public class DersStore : IDersStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public DersStore(IApplicationDbContext db, IMapper autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<DersDto>> GetAll()
        {
            //return await _autoMapper.ProjectTo<DersDto>(_db.Derss).ToListAsync();
            //var asd=  _autoMapper.Map<List<DersDto>>(await _db.Derss.Include("Universite").ToListAsync());
            var asd = _autoMapper.Map<List<DersDto>>(await _db.Derss.ToListAsync());
            return asd;
        }

        public async Task<DersDto> GetById(int id)
        {
            var ders = await _db.Derss.SingleOrDefaultAsync(t => t.Id == id);

            if (ders == null)
                throw new InvalidDataException($"Unable to find Ders with ID: {id}");

            return _autoMapper.Map<DersDto>(ders);
        }

        public async Task<Ders> Create(DersDto dersDto)
        {
            var ders = _autoMapper.Map<DersDto, Ders>(dersDto);
            await _db.Derss.AddAsync(ders);
            await _db.SaveChangesAsync(CancellationToken.None);
            return ders;
        }

        public async Task<Ders> Update(DersDto dersDto)
        {
            var ders = await _db.Derss.SingleOrDefaultAsync(t => t.Id == dersDto.Id);
            if (ders == null)
                throw new InvalidDataException($"Unable to find Ders with ID: {dersDto.Id}");

            ders = _autoMapper.Map(dersDto, ders);
            _db.Derss.Update(ders);
            await _db.SaveChangesAsync(CancellationToken.None);

            return ders;
        }

        public async Task DeleteById(int id)
        {
            var ders = _db.Derss.SingleOrDefault(t => t.Id == id);

            if (ders == null)
                throw new InvalidDataException($"Unable to find Ders with ID: {id}");

            _db.Derss.Remove(ders);
            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<DersDto>> GetDersByMufredatId(int mufredatId)
        {
            var derss = await _db.Derss.Where(t => t.MufredatId == mufredatId).ToListAsync();

            if (derss == null)
                throw new InvalidDataException($"Unable to find Ders with mufredatId: {mufredatId}");

            return _autoMapper.Map<List<DersDto>>(derss);
        }

        public async Task<List<DersDto>> GetAcilacakDersByFilterDto(DersFilterDto dersFilterDto)
        {
            var derss = from ders in _db.Derss
                        join m in _db.Mufredats on ders.MufredatId equals m.Id
                        where
                            (dersFilterDto.MufredatSecilen.Contains(55555) ? dersFilterDto.MufredatSecenektekiler.Contains(ders.MufredatId) : dersFilterDto.MufredatSecilen.Contains(ders.MufredatId))
                            && (m.Aktif == dersFilterDto.IsActive) && (m.Aktif == dersFilterDto.IsIntibak)//TODO : intibak ve aktif konusu konuslacak
                            && (dersFilterDto.SinifSecilen.Contains(ders.Sinif))
                            && (ders.DonemId == dersFilterDto.DonemSecilen)
                            && (string.IsNullOrWhiteSpace(dersFilterDto.DersAd) ? true : ders.Ad.Contains(dersFilterDto.DersAd))
                            && (string.IsNullOrWhiteSpace(dersFilterDto.DersKod) ? true : ders.Kod.Contains(dersFilterDto.DersKod))
                        select ders;

            return _autoMapper.Map<List<DersDto>>(await derss.ToListAsync()) ;


        }

        public async Task CreateDersAcilansByDersId(int dersId)
        {
            var dumpDersAcilan = await _db.DersAcilans.FirstOrDefaultAsync(x => x.DersId == dersId);

            var ders = await _db.Derss.FirstOrDefaultAsync(x => x.Id == dersId);

            DersAcilan dersAcilan = new DersAcilan
            {
                Ad = ders.Ad,
                AdEn = ders.AdEn,
                Akts = ders.Akts,
                BolumId = ders.BolumId,
                DersId = ders.Id,
                KisaAd = ders.KisaAd,
                Kod = ders.Kod + "-" + ders.Id,
                Kredi = ders.Kredi,
                LabSaat = ders.LabSaat,
                MufredatId = ders.MufredatId,
                OptikKod = ders.OptikKod,
                ProgramId = ders.ProgramId,
                SecmeliKodu = ders.SecmeliKodu,
                Sinif = ders.Sinif,
                DonemId = ders.DonemId,
                Durum = ders.Durum,
                FakulteId = ders.FakulteId,
                TeoSaat = ders.TeoSaat,
                UygSaat = ders.UygSaat,
                Zorunlu = ders.Zorunlu
            };

            ders.Durum = true;

            var mufredat = await _db.Mufredats.FirstOrDefaultAsync(x=>x.Id == ders.MufredatId);
            mufredat.Durum = 1;

            if (dumpDersAcilan!=null)
            {
                _db.DersAcilans.Remove(dumpDersAcilan);
            }

            _db.Mufredats.Update(mufredat);

            _db.DersAcilans.Add(dersAcilan);

            _db.Derss.Update(ders);

            await _db.SaveChangesAsync(CancellationToken.None);
        }

        //void deneme()
        //{
        //    _db.Context.UserRoles
        //}
    }
}
