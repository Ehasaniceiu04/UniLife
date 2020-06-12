using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IDonemStore
    {
        Task<List<DonemDto>> GetAll();

        Task<DonemDto> GetById(int id);

        Task<Donem> Create(DonemDto donemDto);

        Task<Donem> Update(DonemDto donemDto);

        Task DeleteById(int id);
        Task<List<DonemDto>> Current();
    }
}
