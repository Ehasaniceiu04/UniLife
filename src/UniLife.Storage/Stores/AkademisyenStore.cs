using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UniLife.Storage.Stores
{
    public class AkademisyenStore : BaseStore<Akademisyen, AkademisyenDto>, IAkademisyenStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public AkademisyenStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }


        public async Task<AkademisyenDto> GetAkademisyenState(Guid userId)
        {
            var akaQuery = from aka in _db.Akademisyens
                           where aka.ApplicationUserId == userId
                           select aka;

            return _autoMapper.Map<AkademisyenDto>(await akaQuery.FirstOrDefaultAsync());
        }
    }
}
