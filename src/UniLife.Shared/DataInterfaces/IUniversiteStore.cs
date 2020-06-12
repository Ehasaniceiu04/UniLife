using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IUniversiteStore
    {
        Task<List<UniversiteDto>> GetAll();

        Task<UniversiteDto> GetById(int id);

        Task<Universite> Create(UniversiteDto universiteDto);

        Task<Universite> Update(UniversiteDto universiteDto);

        Task DeleteById(int id);
    }
}