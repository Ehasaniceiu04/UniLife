using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.DataInterfaces;

namespace UniLife.Shared.DataModels
{
    public class DerslikRezerv : Entity<int>, IAuditable, ISoftDelete
    {
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Nullable<bool> IsAllDay { get; set; }
        public string CategoryColor { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }

        public int? DersAcilanId { get; set; }
        public virtual DersAcilan DersAcilan { get; set; }

        public int? SinavId { get; set; }
        public virtual Sinav Sinav{ get; set; }

        public bool IsSinav { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ResourceData")]
        public int DerslikId { get; set; }
        public virtual Derslik ResourceData { get; set; }

        public bool IsBlock { get; set; }
        public virtual string ElementType { get; set; }
        public virtual DateTime StartTimeValue { get; set; }
        public virtual DateTime EndTimeValue { get; set; }
        
    }
}
