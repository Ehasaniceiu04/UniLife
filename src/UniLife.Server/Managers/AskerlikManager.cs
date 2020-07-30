using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage.Migrations;

namespace UniLife.Server.Managers
{
    public class AskerlikManager : BaseManager<Askerlik, AskerlikDto>, IAskerlikManager
    {
        public AskerlikManager(IAskerlikStore askerlikStore) : base(askerlikStore)
        {

        }

    }
}
