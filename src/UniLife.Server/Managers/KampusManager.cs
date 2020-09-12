using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class KampusManager : BaseManager<Kampus, KampusDto>, IKampusManager
    {
        public KampusManager(IKampusStore kampusStore) : base(kampusStore)
        {

        }
    }
}
