using UniLife.Server.Middleware.Wrappers;
using System;
using System.Threading.Tasks;
using UniLife.Shared.DataModels;
using System.Linq.Expressions;
using System.Threading;

namespace UniLife.Server.Managers
{
    public interface IDbLogManager
    {
        Task<ApiResponse> GetAsync(int pageSize, int page, Expression<Func<DbLog, bool>> predicate = null, CancellationToken cancellationToken = default);


        Task<ApiResponse> GetDeltaMetaAsync(int deltaIndex, Expression<Func<DbLog, bool>> predicate = null, CancellationToken cancellationToken = default);
    }
}
