using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IDersStore
    {
        Task<List<DersDto>> GetAll();

        Task<DersDto> GetById(int id);

        Task<Ders> Create(DersDto dersDto);

        Task<Ders> Update(DersDto dersDto);

        Task DeleteById(int id);
        Task<List<DersDto>> GetDersByMufredatId(int mufredatId);
    }
}
