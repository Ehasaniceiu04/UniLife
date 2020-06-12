using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class KayitNedenManager : BaseManager<KayitNeden, KayitNedenDto>, IKayitNedenManager
    {
        public KayitNedenManager(IKayitNedenStore kayitNedenStore) : base(kayitNedenStore)
        {

        }

    }
}
