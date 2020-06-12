using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class OgretmenStore : BaseStore<Ogretmen, OgretmenDto>, IOgretmenStore
    {
        //private readonly IApplicationDbContext _db;
        //private readonly IMapper _autoMapper;

        public OgretmenStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            //_db = db;
            //_autoMapper = autoMapper;
        }

    }
}
