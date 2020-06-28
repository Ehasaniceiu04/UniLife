using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class DerslikManager : BaseManager<Derslik, DerslikDto>, IDerslikManager
    {
        public DerslikManager(IDerslikStore derslikStore) : base(derslikStore)
        {

        }

    }
}
