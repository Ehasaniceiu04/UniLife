using AutoMapper;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.Storage.Stores
{
    public class PersonelTaskStore : BaseStore<PersonelTask, PersonelTaskDto>, IPersonelTaskStore
    {

        public PersonelTaskStore(IApplicationDbContext db, IMapper autoMapper) : base(db, autoMapper)
        {
        }

    }
}
