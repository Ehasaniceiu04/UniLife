using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.DataModels
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
