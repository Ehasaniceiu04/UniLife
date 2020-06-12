using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Sample;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
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