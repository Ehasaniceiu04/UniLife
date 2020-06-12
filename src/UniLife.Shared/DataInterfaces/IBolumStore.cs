using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IBolumStore
    {
        Task<List<BolumDto>> GetAll();

        Task<BolumDto> GetById(int id);

        Task<Bolum> Create(BolumDto bolumDto);

        Task<Bolum> Update(BolumDto bolumDto);

        Task DeleteById(int id);
        Task<List<BolumDto>> GetBolumByFakulteId(string[] fakulteIds);
        IQueryable<Bolum> GetAllQueryable();
    }
}
