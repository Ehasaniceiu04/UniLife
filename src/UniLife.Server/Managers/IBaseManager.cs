using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IBaseManager<T,TDto> where TDto : EntityDto<int>, new()
                                        where T : Entity<int>, new()
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(TDto tDto);
        Task<ApiResponse> Update(TDto tDto);
        Task<ApiResponse> Delete(int id);

        Task<ApiResponse> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<ApiResponse> First(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
