using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class SinavKriterManager : BaseManager<SinavKriter, SinavKriterDto>, ISinavKriterManager
    {
        public SinavKriterManager(ISinavKriterStore sinavKriterStore) : base(sinavKriterStore)
        {

        }

    }
}
