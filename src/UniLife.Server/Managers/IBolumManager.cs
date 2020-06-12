using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared.Dto.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Server.Managers
{
    public interface IBolumManager
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Create(BolumDto bolumDto);
        Task<ApiResponse> Update(BolumDto bolumDto);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse> GetBolumByFakulteId(string[] fakulteIds);

        IQueryable<UniLife.Shared.DataModels.Bolum> GetAllQueryable();
    }
}
