﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class DerslikRezervDto : EntityDto<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
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

        public int DerslikId { get; set; }
        public DerslikDto ResourceData { get; set; } // foreign key olarka aslı modelde farklı.

        public bool IsBlock { get; set; }
        public virtual string ElementType { get; set; }
        public virtual DateTime StartTimeValue { get; set; }
        public virtual DateTime EndTimeValue { get; set; }

    }
}
