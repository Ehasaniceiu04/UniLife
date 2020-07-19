using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class OgrenimDurumManager : BaseManager<OgrenimDurum, OgrenimDurumDto>, IOgrenimDurumManager
    {
        public OgrenimDurumManager(IOgrenimDurumStore ogrenimDurumStore) : base(ogrenimDurumStore)
        {

        }
    }
}
