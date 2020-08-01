using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class OgrenciDigerStore : BaseStore<OgrenciDiger, OgrenciDigerDto>, IOgrenciDigerStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public OgrenciDigerStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task PostSaveGecis(OgrenciDigerDto ogrenciDigerDto)
        {
            var existingOgrenciDiger = _db.OgrenciDigers.Where(x => x.OgrenciId == ogrenciDigerDto.OgrenciId).FirstOrDefault();
            if (existingOgrenciDiger != null)
            {
                existingOgrenciDiger.GelUniv = ogrenciDigerDto.GelUniv;
                existingOgrenciDiger.GelBolum = ogrenciDigerDto.GelBolum;
                existingOgrenciDiger.GelBirim = ogrenciDigerDto.GelBirim;
                _db.OgrenciDigers.Update(existingOgrenciDiger);
            }
            else
            {
                _db.OgrenciDigers.Add(_autoMapper.Map<OgrenciDigerDto, OgrenciDiger>(ogrenciDigerDto));
            }

            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task PostSaveKayitDond(OgrenciDigerDto ogrenciDigerDto)
        {
            var existingOgrenciDiger = _db.OgrenciDigers.Where(x => x.OgrenciId == ogrenciDigerDto.OgrenciId).FirstOrDefault();
            if (existingOgrenciDiger != null)
            {
                existingOgrenciDiger.DondTarih = ogrenciDigerDto.DondTarih;
                existingOgrenciDiger.IsDond = ogrenciDigerDto.IsDond;
                _db.OgrenciDigers.Update(existingOgrenciDiger);
            }
            else
            {
                _db.OgrenciDigers.Add(_autoMapper.Map<OgrenciDigerDto, OgrenciDiger>(ogrenciDigerDto));
            }

            await _db.SaveChangesAsync(CancellationToken.None);
        }
        public async Task PostSaveKayitCeza(OgrenciDigerDto ogrenciDigerDto)
        {
            var existingOgrenciDiger = _db.OgrenciDigers.Where(x => x.OgrenciId == ogrenciDigerDto.OgrenciId).FirstOrDefault();
            if (existingOgrenciDiger != null)
            {
                existingOgrenciDiger.CezaTarih = ogrenciDigerDto.CezaTarih;
                existingOgrenciDiger.CezaAd = ogrenciDigerDto.CezaAd;
                existingOgrenciDiger.CezaDesc = ogrenciDigerDto.CezaDesc;
                _db.OgrenciDigers.Update(existingOgrenciDiger);
            }
            else
            {
                _db.OgrenciDigers.Add(_autoMapper.Map<OgrenciDigerDto, OgrenciDiger>(ogrenciDigerDto));
            }

            await _db.SaveChangesAsync(CancellationToken.None);
        }
        public async Task PostSaveKayitStaj(OgrenciDigerDto ogrenciDigerDto)
        {
            var existingOgrenciDiger = _db.OgrenciDigers.Where(x => x.OgrenciId == ogrenciDigerDto.OgrenciId).FirstOrDefault();
            if (existingOgrenciDiger != null)
            {
                existingOgrenciDiger.StajTarihBas = ogrenciDigerDto.StajTarihBas;
                existingOgrenciDiger.StajTarihSon = ogrenciDigerDto.StajTarihSon;
                existingOgrenciDiger.StajSirket = ogrenciDigerDto.StajSirket;
                _db.OgrenciDigers.Update(existingOgrenciDiger);
            }
            else
            {
                _db.OgrenciDigers.Add(_autoMapper.Map<OgrenciDigerDto, OgrenciDiger>(ogrenciDigerDto));
            }

            await _db.SaveChangesAsync(CancellationToken.None);
        }

        public async Task PostSaveKayitTez(OgrenciDigerDto ogrenciDigerDto)
        {
            var existingOgrenciDiger = _db.OgrenciDigers.Where(x => x.OgrenciId == ogrenciDigerDto.OgrenciId).FirstOrDefault();
            if (existingOgrenciDiger != null)
            {
                existingOgrenciDiger.TezKonu = ogrenciDigerDto.TezKonu;
                existingOgrenciDiger.TezTarih = ogrenciDigerDto.TezTarih;
                _db.OgrenciDigers.Update(existingOgrenciDiger);
            }
            else
            {
                _db.OgrenciDigers.Add(_autoMapper.Map<OgrenciDigerDto, OgrenciDiger>(ogrenciDigerDto));
            }

            await _db.SaveChangesAsync(CancellationToken.None);
        }
    }
}
