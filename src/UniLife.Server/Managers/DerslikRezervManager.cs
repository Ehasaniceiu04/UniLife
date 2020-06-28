using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class DerslikRezervManager : BaseManager<DerslikRezerv, DerslikRezervDto>, IDerslikRezervManager
    {
        public DerslikRezervManager(IDerslikRezervStore derslikRezervStore) : base(derslikRezervStore)
        {

        }

    }
}
