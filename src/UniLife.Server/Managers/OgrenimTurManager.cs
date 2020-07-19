using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class OgrenimTurManager : BaseManager<OgrenimTur, OgrenimTurDto>, IOgrenimTurManager
    {
        public OgrenimTurManager(IOgrenimTurStore ogrenimTurStore) : base(ogrenimTurStore)
        {

        }
    }
}
