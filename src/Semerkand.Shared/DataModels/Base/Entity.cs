using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semerkand.Shared.DataModels
{
    public class Entity<T>: IEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
