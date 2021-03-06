using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Shared.Dto.Definitions
{
    public class PersonelTaskDto : EntityDto<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        //public string StartTimeDay {
        //    get 
        //    {
        //        switch (StartTime.DayOfWeek)
        //        {
        //            case DayOfWeek.Monday:
        //                return "Pazartesi";
        //            case DayOfWeek.Tuesday:
        //                return "Salı";
        //            case DayOfWeek.Wednesday:
        //                return "Çarşamba";
        //            case DayOfWeek.Thursday:
        //                return "Perşembe";
        //            case DayOfWeek.Friday:
        //                return "Cuma";
        //            case DayOfWeek.Saturday:
        //                return "Cumartesi";
        //            case DayOfWeek.Sunday:
        //                return "Pazar";
        //            default:
        //                return "Gün yok";
        //        }
        //    }
        //}

        //public string StartTimeTime 
        //{
        //    get
        //    {
        //        return StartTime.ToString("HH:mm");
        //    }
        //}
        public DateTime EndTime { get; set; }
        //public string EndTimeTime
        //{
        //    get
        //    {
        //        return EndTime.ToString("HH:mm");
        //    }
        //}
        public Nullable<bool> IsAllDay { get; set; }
        public string CategoryColor { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
    }
}
