using UniLife.Server.Models;
using UniLife.Shared.Dto.Tenant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniLife.Shared.DataInterfaces
{
    public interface ITenantStore
    {
        List<TenantDto> GetAll();

        TenantDto GetById(long id);

        Task<Tenant> Create(TenantDto tenantDto);

        Task<Tenant> Update(TenantDto tenantDto);

        Task DeleteById(long id);
    }
}
