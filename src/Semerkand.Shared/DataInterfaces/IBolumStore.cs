using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IBolumStore
    {
        Task<List<BolumDto>> GetAll();

        Task<BolumDto> GetById(int id);

        Task<Bolum> Create(BolumDto bolumDto);

        Task<Bolum> Update(BolumDto bolumDto);

        Task DeleteById(int id);
        Task<List<BolumDto>> GetDersByMufredatId(string[] fakulteIds);
    }
}
