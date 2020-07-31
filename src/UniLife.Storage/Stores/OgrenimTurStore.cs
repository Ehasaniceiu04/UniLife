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
    public class OgrenimTurStore : BaseStore<OgrenimTur, OgrenimTurDto>, IOgrenimTurStore
    {

        public OgrenimTurStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }       

    }
}
