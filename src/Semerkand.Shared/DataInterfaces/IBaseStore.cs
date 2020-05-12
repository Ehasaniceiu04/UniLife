using Semerkand.Shared.DataModels;
using Semerkand.Shared.Dto;
using Semerkand.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataInterfaces
{
    public interface IBaseStore<T,TDto> where T : Entity<int>, new()
                                        where TDto : EntityDto<int>, new()
    {
        Task<List<TDto>> GetAll();

        Task<TDto> GetById(int id);

        Task<T> Create(TDto tDto);

        Task<T> Update(TDto tDto);

        Task DeleteById(int id);
    }
}
