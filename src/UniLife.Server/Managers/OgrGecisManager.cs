using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class OgrGecisManager : BaseManager<OgrGecis, OgrGecisDto>, IOgrGecisManager
    {
        public OgrGecisManager(IOgrGecisStore ogrGecisStore) : base(ogrGecisStore)
        {

        }

    }
}
