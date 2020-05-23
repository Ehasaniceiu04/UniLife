using AutoMapper;
using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Semerkand.Storage.Stores
{
    public class OgrenciStore : BaseStore<Ogrenci, OgrenciDto>, IOgrenciStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        public OgrenciStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }

    }
}
