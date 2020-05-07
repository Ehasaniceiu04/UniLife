using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.Dto.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IFakulteStore
    {
        Task<List<FakulteDto>> GetAll();

        Task<FakulteDto> GetById(int id);

        Task<Fakulte> Create(FakulteDto fakulteDto);

        Task<Fakulte> Update(FakulteDto fakulteDto);

        Task DeleteById(int id);
    }
}