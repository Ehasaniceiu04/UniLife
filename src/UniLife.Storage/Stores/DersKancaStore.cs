using AutoMapper;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace UniLife.Storage.Stores
{
    public class DersKancaStore : BaseStore<DersKanca, DersKancaDto>, IDersKancaStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public DersKancaStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task YeniKanca(DersKancaDto dersKancaDto)
        {
            var silinecekKancas = await _db.DersKancas.Where(x => x.PasifMufredatDersId == dersKancaDto.PasifMufredatDersId).ToListAsync();

            _db.DersKancas.RemoveRange(silinecekKancas);

            _db.DersKancas.Add(new DersKanca { 
                AktifMufredatDersId = dersKancaDto.AktifMufredatDersId, 
                PasifMufredatDersId = dersKancaDto.PasifMufredatDersId,
                PasifMufredatAkts = dersKancaDto.PasifMufredatAkts,
                PasifMufredatKredi=dersKancaDto.PasifMufredatKredi,
                PasifMufredatDersAd =dersKancaDto.PasifMufredatDersAd,
                PasifMufredatDersKod = dersKancaDto.PasifMufredatDersKod,
                AktifMufredatAkts = dersKancaDto.AktifMufredatAkts,
                AktifMufredatKredi = dersKancaDto.AktifMufredatKredi,
                AktifMufredatDersAd = dersKancaDto.AktifMufredatDersAd,
                AktifMufredatDersKod = dersKancaDto.AktifMufredatDersKod
            });

            var kancalananDers = await _db.Derss.FirstOrDefaultAsync(x => x.Id == dersKancaDto.AktifMufredatDersId);

            (await _db.Derss.FirstOrDefaultAsync(x => x.Id == dersKancaDto.PasifMufredatDersId)).KancalananDersAd = kancalananDers.Ad;

            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
