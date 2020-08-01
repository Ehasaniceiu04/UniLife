using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace UniLife.Shared.DataInterfaces
{
    public interface IBaseStore<T,TDto> where T : Entity<int>, new()
                                        where TDto : EntityDto<int>, new()
    {
        Task<List<TDto>> GetAll();

        Task<TDto> GetById(int id);

        Task<T> Create(TDto tDto);

        Task<T> Update(TDto tDto);

        Task DeleteById(int id);
        Task<IEnumerable<TDto>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<TDto> First(Expression<Func<T, bool>> predicate);
    }
}
