using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class DerslikStore : BaseStore<Derslik, DerslikDto>, IDerslikStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public DerslikStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<DersliksAndDerslikRezervsDto> GetDersliksAndDerslikRezsByMufredatId(int mufredatId)
        {
            var mufredatAcilanDersler = await _db.DersAcilans.Where(x => x.MufredatId == mufredatId).Select(x => x.Id).ToListAsync();

            var derslikRezervs = await (from dr in _db.DerslikRezervs.Where(x => mufredatAcilanDersler.Contains(x.DersAcilanId))
                                                                 select dr).ToListAsync();

            var dersliks = await (from d in _db.Dersliks.Where(x => derslikRezervs.Select(y=>y.DerslikId).Contains(x.Id))
                                        select d).ToListAsync();



            //var ogrenciDersliks = from da in _db.DersAcilans.Where(x => x.MufredatId == mufredatId)
            //                      .Include(x => x.DerslikRezervs)
            //                      .ThenInclude(y => y.ResourceData)
            //                      select da;

            derslikRezervs.ForEach(x => x.ResourceData = null);
            dersliks.ForEach(x => x.DerslikRezervs = null);

            DersliksAndDerslikRezervsDto dersliksAndDerslikRezervsDto = new DersliksAndDerslikRezervsDto();
            dersliksAndDerslikRezervsDto.DerslikRezervs = _autoMapper.Map<List<DerslikRezervDto>>(derslikRezervs);
            dersliksAndDerslikRezervsDto.Dersliks = _autoMapper.Map<List<DerslikDto>>(dersliks);

            return dersliksAndDerslikRezervsDto;
            //return await ogrenciDersliks.ToListAsync();
        }
    }
}
