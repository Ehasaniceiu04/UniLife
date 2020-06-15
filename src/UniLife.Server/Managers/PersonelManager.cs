using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class PersonelManager : BaseManager<Personel, PersonelDto>, IPersonelManager
    {
        public PersonelManager(IPersonelStore personelStore) : base(personelStore)
        {

        }

    }
}
