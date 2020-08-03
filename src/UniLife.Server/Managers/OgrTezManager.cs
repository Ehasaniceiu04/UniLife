using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage.Migrations;

namespace UniLife.Server.Managers
{
    public class OgrTezManager : BaseManager<OgrTez, OgrTezDto>, IOgrTezManager
    {
        public OgrTezManager(IOgrTezStore ogrTezStore) : base(ogrTezStore)
        {

        }

    }
}
