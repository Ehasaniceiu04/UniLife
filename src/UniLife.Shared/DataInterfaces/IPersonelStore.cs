using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.Shared.DataInterfaces
{
    public interface IPersonelStore : IBaseStore<Personel, PersonelDto>
    {
        Task<long> GetLastPersNo();
    }
}
