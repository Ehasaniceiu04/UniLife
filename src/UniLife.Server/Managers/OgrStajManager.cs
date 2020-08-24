using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class OgrStajManager : BaseManager<OgrStaj, OgrStajDto>, IOgrStajManager
    {
        public OgrStajManager(IOgrStajStore ogrStajStore) : base(ogrStajStore)
        {

        }

    }
}
