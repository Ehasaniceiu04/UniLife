using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.Dto.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IUniversiteStore
    {
        Task<List<UniversiteDto>> GetAll();

        Task<UniversiteDto> GetById(int id);

        Task<Universite> Create(UniversiteDto todoDto);

        Task<Universite> Update(UniversiteDto todoDto);

        Task DeleteById(int id);
    }
}