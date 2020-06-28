using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class BinaManager : BaseManager<Bina, BinaDto>, IBinaManager
    {
        public BinaManager(IBinaStore binaStore) : base(binaStore)
        {

        }

    }
}
