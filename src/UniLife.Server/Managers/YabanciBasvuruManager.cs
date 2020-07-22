using UniLife.Shared.DataInterfaces;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Server.Managers
{
    public class YabanciBasvuruManager : BaseManager<YabanciBasvuru, YabanciBasvuruDto>, IYabanciBasvuruManager
    {
        public YabanciBasvuruManager(IYabanciBasvuruStore yabanciBasvuruStore) : base(yabanciBasvuruStore)
        {

        }

    }
}
