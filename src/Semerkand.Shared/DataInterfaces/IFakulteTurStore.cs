using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using Semerkand.Shared.Dto.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IFakulteTurStore
    {
        Task<List<FakulteTurDto>> GetAll();

        Task<FakulteTurDto> GetById(int id);

        Task<FakulteTur> Create(FakulteTurDto fakulteTurDto);

        Task<FakulteTur> Update(FakulteTurDto fakulteTurDto);

        Task DeleteById(int id);
    }
}