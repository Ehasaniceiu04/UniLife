using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage.Migrations;

namespace UniLife.Server.Managers
{
    public class OgrHarcManager : BaseManager<OgrHarc, OgrHarcDto>, IOgrHarcManager
    {
        public OgrHarcManager(IOgrHarcStore ogrHarcStore) : base(ogrHarcStore)
        {

        }

    }
}
