using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class AkademisyenStore : BaseStore<Akademisyen, AkademisyenDto>, IAkademisyenStore
    {
        //private readonly IApplicationDbContext _db;
        //private readonly IMapper _autoMapper;

        public AkademisyenStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            //_db = db;
            //_autoMapper = autoMapper;
        }

    }
}
