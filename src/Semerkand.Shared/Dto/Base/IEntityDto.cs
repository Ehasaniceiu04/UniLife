using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.Dto
{
    public interface IEntityDto<T>
    {
        T Id { get; set; }
    }
}
