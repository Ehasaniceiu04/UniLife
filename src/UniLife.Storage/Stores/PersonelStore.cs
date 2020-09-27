using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class PersonelStore : BaseStore<Personel, PersonelDto>, IPersonelStore
    {
        private readonly IApplicationDbContext _db;
        private readonly IMapper _autoMapper;

        public PersonelStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
            _db = db;
            _autoMapper = autoMapper;
        }
        public async Task<long> GetLastPersNo()
        {
            try
            {
                return await _db.Akademisyens.MaxAsync(x => x.AkaNo);
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
