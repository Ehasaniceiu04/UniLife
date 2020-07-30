using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class OsymStore : BaseStore<Osym, OsymDto>, IOsymStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public OsymStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

        public async Task<OsymDto> GetByAppId(Guid appId)
        {
            return _autoMapper.Map<OsymDto>(await _db.Osyms.Where(x => x.ApplicationUserId == appId).FirstOrDefaultAsync());
        }
    }
}
