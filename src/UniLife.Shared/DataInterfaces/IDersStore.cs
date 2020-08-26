using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface IDersStore
    {
        Task<List<DersDto>> GetAll();

        Task<DersDto> GetById(int id);

        Task<Ders> Create(DersDto dersDto);

        Task<Ders> Update(DersDto dersDto);

        Task DeleteById(int id);
        Task<List<DersDto>> GetDersByMufredatId(int mufredatId);
        Task<List<DersDto>> GetAcilacakDersByFilterDto(DersFilterDto dersFilterDto);
        Task CreateDersAcilansByDersId(int dersId);
        Task<Ders> AddYerineDers(int dersId, int yerineDersId);
    }
}
