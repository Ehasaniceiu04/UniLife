using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniLife.Shared;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class AkademikTakvimStore : BaseStore<AkademikTakvim, AkademikTakvimDto>, IAkademikTakvimStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public AkademikTakvimStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<List<AkademikTakvimDto>> GetAkaTakByFakIdDonId(int fakulteId, int donemId)
        {
            List<AkademikTakvim> spesFakResult = new List<AkademikTakvim>();
            spesFakResult = await _db.AkademikTakvims.Where(x => x.DonemId == donemId && x.FakulteId == fakulteId).ToListAsync();

            if (spesFakResult.Count < 1)
            {
                spesFakResult = await _db.AkademikTakvims.Where(x => x.DonemId == donemId && x.FakulteId == null).ToListAsync();
            }

            var sonuc = from bir in _db.AkademikTakvims.Where(x => x.DonemId == donemId && x.FakulteId == null)
                        join iki in _db.AkademikTakvims.Where(x => x.DonemId == donemId && x.FakulteId == fakulteId) on bir.Kod equals iki.Kod into ps
                        from iki in ps.DefaultIfEmpty()
                        select new AkademikTakvimDto
                        {
                            Id = (iki == null || iki.Id == 0) ? bir.Id : iki.Id,
                            Ad = bir.Ad,
                            Kod = bir.Kod,
                            BasTarih = (iki == null || iki.BasTarih == null) ? bir.BasTarih : iki.BasTarih,
                            BitTarih = (iki == null || iki.BitTarih == null) ? bir.BitTarih : iki.BitTarih,
                            DonemId = (iki == null || iki.DonemId == 0) ? bir.DonemId : iki.DonemId,
                            FakulteId = (iki == null || iki.FakulteId == null) ? iki.FakulteId : bir.FakulteId
                        };

            return await sonuc.ToListAsync();
        }

        public async Task<int> PostChangeAllFakultesTakvim(AkademikTakvimDto akademikTakvimDto)
        {
             var withKodes= await _db.AkademikTakvims.Where(x => x.Kod == akademikTakvimDto.Kod 
                                                            && x.DonemId == akademikTakvimDto.DonemId
                                                            /*&& x.FakulteId == null*/).ToListAsync();

            //if (withKodes.Count>1)
            //{
            //    throw new DomainException("Takvimde hata oluştu");
            //}

            AkademikTakvim general = withKodes.FirstOrDefault(x => x.FakulteId == null);
            List<AkademikTakvim> others  = withKodes.Where(x => x.FakulteId != null).ToList();

            general.BasTarih = akademikTakvimDto.BasTarih;
            general.BitTarih = akademikTakvimDto.BitTarih;

            _db.AkademikTakvims.RemoveRange(others);

            return await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
