using Semerkand.Shared.DataInterfaces;
using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;

namespace Semerkand.Server.Managers
{
    public class KayitNedenManager : BaseManager<KayitNeden, KayitNedenDto>, IKayitNedenManager
    {
        public KayitNedenManager(IKayitNedenStore kayitNedenStore) : base(kayitNedenStore)
        {

        }

    }
}
