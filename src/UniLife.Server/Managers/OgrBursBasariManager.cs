using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class OgrBursBasariManager : BaseManager<OgrBursBasari, OgrBursBasariDto>, IOgrBursBasariManager
    {
        public OgrBursBasariManager(IOgrBursBasariStore ogrBursBasariStore) : base(ogrBursBasariStore)
        {

        }
    }
}
