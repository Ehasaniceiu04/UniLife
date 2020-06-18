using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class SinavTurManager : BaseManager<SinavTur, SinavTurDto>, ISinavTurManager
    {
        public SinavTurManager(ISinavTurStore sinavTurStore) : base(sinavTurStore)
        {

        }

    }
}
