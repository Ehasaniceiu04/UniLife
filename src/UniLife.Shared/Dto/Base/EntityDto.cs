using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto
{
    public class EntityDto<T> : IEntityDto<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}
