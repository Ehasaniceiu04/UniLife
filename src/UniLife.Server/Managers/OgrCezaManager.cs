using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage.Migrations;

namespace UniLife.Server.Managers
{
    public class OgrCezaManager : BaseManager<OgrCeza, OgrCezaDto>, IOgrCezaManager
    {
        public OgrCezaManager(IOgrCezaStore ogrCezaStore) : base(ogrCezaStore)
        {

        }

    }
}
