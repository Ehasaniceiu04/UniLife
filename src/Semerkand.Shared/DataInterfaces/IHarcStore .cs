using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IHarcStore
    {
        Task<List<HarcDto>> GetAll();

        Task<HarcDto> GetById(int id);

        Task<Harc> Create(HarcDto harcDto);

        Task<Harc> Update(HarcDto harcDto);

        Task DeleteById(int id);
    }
}
