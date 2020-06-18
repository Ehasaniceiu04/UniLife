using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class SinavTipManager : BaseManager<SinavTip, SinavTipDto>, ISinavTipManager
    {
        public SinavTipManager(ISinavTipStore sinavTipStore) : base(sinavTipStore)
        {

        }

    }
}
