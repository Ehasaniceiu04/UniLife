using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Storage.Migrations;

namespace UniLife.Server.Managers
{
    public class OgrenciDigerManager : BaseManager<OgrenciDiger, OgrenciDigerDto>, IOgrenciDigerManager
    {
        public OgrenciDigerManager(IOgrenciDigerStore ogrenciDigerStore) : base(ogrenciDigerStore)
        {

        }

    }
}
