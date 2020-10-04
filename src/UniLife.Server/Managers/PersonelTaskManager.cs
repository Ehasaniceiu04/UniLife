using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class PersonelTaskManager : BaseManager<PersonelTask, PersonelTaskDto>, IPersonelTaskManager
    {
        public PersonelTaskManager(IPersonelTaskStore personelTaskStore) : base(personelTaskStore)
        {

        }

    }
}
