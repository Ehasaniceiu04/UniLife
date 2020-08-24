using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class OgrDondurManager : BaseManager<OgrDondur, OgrDondurDto>, IOgrDondurManager
    {
        public OgrDondurManager(IOgrDondurStore ogrDondurStore) : base(ogrDondurStore)
        {

        }

    }
}
