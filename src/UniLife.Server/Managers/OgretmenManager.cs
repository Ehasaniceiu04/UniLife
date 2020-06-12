using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class OgretmenManager : BaseManager<Ogretmen, OgretmenDto>, IOgretmenManager
    {
        public OgretmenManager(IOgretmenStore ogretmenStore) : base(ogretmenStore)
        {

        }

    }
}
