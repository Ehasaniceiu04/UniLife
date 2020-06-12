using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.Server.Managers
{
    public class OgretmenManager : BaseManager<Ogretmen, OgretmenDto>, IOgretmenManager
    {
        public OgretmenManager(IOgretmenStore ogretmenStore) : base(ogretmenStore)
        {

        }

    }
}
