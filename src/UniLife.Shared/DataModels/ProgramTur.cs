using UniLife.Shared.DataInterfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;

namespace UniLife.Shared.DataModels
{
    public class ProgramTur : Entity<int>, IAuditable, ISoftDelete
    {
        [Required]
        [MaxLength(100)]
        public string Ad { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
